using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSwitchButton : MonoBehaviour
{
    [SerializeField]
    internal GameObject imageEnable;

    [SerializeField]
    internal GameObject imageDisable;

    [SerializeField]
    internal RectTransform switcher;


    [SerializeField]
    internal bool isSwitcher;

    [SerializeField]
    internal Vector2 startSwitch;

    [SerializeField]
    internal Vector2 endSwitch;

    public bool isEnable;

    internal float time = 0;
    private float duration = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        time = 0;
    }

    public virtual void OnClick()
    {
        time = 0;
    }

    public virtual void BeforeUpdate()
    {

    }

    private void Update()
    {
        BeforeUpdate();

        imageDisable.SetActive(!isEnable);
        imageEnable.SetActive(isEnable);

        if (isSwitcher)
        {

            if (time < duration)
            {

                if (isEnable)
                {
                    Debug.Log("Slide enable");
                    switcher.anchoredPosition = Vector2.Lerp(switcher.anchoredPosition, endSwitch, time / duration);
                }
                else
                {
                    Debug.Log("Slide disnable");
                    switcher.anchoredPosition = Vector2.Lerp(switcher.anchoredPosition, startSwitch, time / duration);
                }

                time += Time.deltaTime;
            }

        }
    }
}
