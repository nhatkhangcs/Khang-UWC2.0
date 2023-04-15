using System;
using UnityEngine;
using UnityEngine.UI;

public class SidebarButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image buttonIcon;
    [SerializeField] private ViewType viewType;
    
    private void Awake()
    {
        ApplicationManager.Instance.AddInitWork(Init, ApplicationManager.InitState.UI);
    }

    private void Init()
    {
        button.onClick.AddListener(() => PrimarySidebar.Instance.OnViewChanged(viewType));
        PrimarySidebar.Instance.ViewChanged += ViewChangedHandler;
        ApplicationManager.Instance.CompleteWork(ApplicationManager.InitState.UI);
    }

    private void OnDestroy()
    {
        PrimarySidebar.Instance.ViewChanged -= ViewChangedHandler;
    }

    private void ViewChangedHandler(ViewType changeTo)
    {
        if (viewType == changeTo)
        {
            buttonIcon.color = buttonIcon.color.SetAlpha(1f);
        }
        else
        {
            buttonIcon.color = buttonIcon.color.SetAlpha(VisualManager.Instance.InactiveViewButtonAlpha);
        }
    }
}