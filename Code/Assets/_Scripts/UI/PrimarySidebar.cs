using System;
using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PrimarySidebar : Singleton<PrimarySidebar>
{
    // @formatter:off
    
    public event Action<ViewType> ViewChanged;
    
    [SerializeField] private RectTransform sidebarRectTransform;
    private Vector2 initialSidebarSizeDelta;
    
    [Header("Buttons")] 
    [SerializeField] private SidebarButton mapOverviewButton;
    [SerializeField] private SidebarButton staffOverviewButton;
    [SerializeField] private SidebarButton mcpsOverviewButton;
    [SerializeField] private SidebarButton vehiclesOverviewButton;
    [SerializeField] private SidebarButton messageOverviewButton;
    [SerializeField] private SidebarButton settingsButton;

    [Header("Views")] 
    [SerializeField] private ViewGroup mapViewGroup;
    [SerializeField] private ViewGroup staffViewGroup;
    [SerializeField] private ViewGroup mcpsViewGroup;
    [SerializeField] private ViewGroup vehiclesViewGroup;
    [SerializeField] private ViewGroup messageViewGroup;
    [SerializeField] private ViewGroup settingsViewGroup;

    [HideInInspector] public ViewType CurrentViewType = ViewType.None;
    private GameObject currentViewObject;

    public Task ShowAnimation;
    public Task HideAnimation;

    // @formatter:on

    private void Start()
    {
        initialSidebarSizeDelta = sidebarRectTransform.sizeDelta;
        ViewChanged += changeTo =>
        {
            if (changeTo == ViewType.MapOverview) ShrinkSidebar();
            else ExtendSidebar();
        };
        
        StartCoroutine(NextFrameCall_CO());
    }

    public async void OnViewChanged(ViewType changeTo)
    {
        ViewChanged?.Invoke(changeTo);
        if (HideAnimation != null) await HideAnimation;
        if (ShowAnimation != null) await ShowAnimation;
        CurrentViewType = changeTo;
    }

    private IEnumerator NextFrameCall_CO()
    {
        yield return null;
        OnViewChanged(ViewType.MapOverview);
    }

    private void ExtendSidebar()
    {
        sidebarRectTransform.DOKill();
        sidebarRectTransform.DOSizeDelta(new Vector2(initialSidebarSizeDelta.x, 200), 0.15f)
            .SetEase(Ease.OutCubic);
    }

    private void ShrinkSidebar()
    {
        sidebarRectTransform.DOKill();
        sidebarRectTransform.DOSizeDelta(initialSidebarSizeDelta, 0.15f).SetEase(Ease.OutCubic);
    }
}

public enum ViewType
{
    MapOverview,
    StaffsOverview,
    MCPsOverview,
    VehiclesOverview,
    MessagesOverview,
    SettingsOverview,
    None,
}