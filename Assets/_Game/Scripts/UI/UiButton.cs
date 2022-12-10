using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButton : MonoBehaviour
{
    [SerializeField]
    internal Vector2 viewPosition;
    [SerializeField]
    internal Vector2 hidePosition;
    [SerializeField]
    private bool isEnableAnim = true;
    internal RectTransform R_TF;
    private bool isShow = false;
    private float time = 0;
    private float duration = 1.3f;
    private void Start()
    {
        R_TF = transform.GetComponent<RectTransform>();
    }

    public void Show()
    {
        time = 0;
        isShow = true;

    }
    public void Hide()
    {
        time = 0;
        isShow = false;
    }

    public void OnTap()
    {
        AudioManager.Ins.PlayAudio(AudioType.TAP);
    }


    private void Update()
    {
        if (isEnableAnim)
        {
            if (time < duration)
            {
                if (isShow)
                {
                    R_TF.anchoredPosition = Vector2.Lerp(hidePosition, viewPosition, time / duration);

                }
                else
                {
                    R_TF.anchoredPosition = Vector2.Lerp(viewPosition, hidePosition, time / duration);
                }
                time += Time.deltaTime;
            }
        }
    }
}