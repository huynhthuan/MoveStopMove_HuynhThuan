using UnityEngine;
using UnityEditor;

public class SaveScreenshotToFile
{
    [MenuItem("Custom/Render Camera to file")]
    public static void RenderCameraToFile()
    {
        Camera camera = Selection.activeGameObject.GetComponent<Camera>();

        RenderTexture rt = new RenderTexture(500, 500, 24, RenderTextureFormat.BGRA32, RenderTextureReadWrite.sRGB);
        RenderTexture oldRT = camera.targetTexture;
        camera.targetTexture = rt;
        camera.Render();
        camera.targetTexture = oldRT;

        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        RenderTexture.active = null;

        byte[] bytes = tex.EncodeToPNG();
        string path = "Assets/SaveImage/screenshot-" + Random.value + ".png";
        System.IO.File.WriteAllBytes(path, bytes);
        AssetDatabase.ImportAsset(path);
        Debug.Log("Saved to " + path);
    }

    [MenuItem("Custom/Render Camera to file", true)]
    public static bool RenderCameraToFileValidation()
    {
        return Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Camera>() != null;
    }
}