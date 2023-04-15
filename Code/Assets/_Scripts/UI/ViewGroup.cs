using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ViewGroup : MonoBehaviour
{
    [SerializeField] private ViewType viewType;

    [SerializeField] private List<GameObject> showGameObjects;
    [SerializeField] private List<GameObject> hideGameObjects;
    private List<IShowAnimatable> showAnimatables = new();
    private List<IHideAnimatable> hideAnimatables = new();

    private void Awake()
    {
        ApplicationManager.Instance.AddInitWork(Init, ApplicationManager.InitState.UI);
        ApplicationManager.Instance.AddTerminateWork(Terminate, ApplicationManager.TerminateState.UI);
    }

    private void Init()
    {
        PrimarySidebar.Instance.ViewChanged += ViewChangedHandler;

        foreach (var showGameObject in showGameObjects)
        {
            showAnimatables.Add(showGameObject.GetComponent<IShowAnimatable>());
        }

        foreach (var hideGameObject in hideGameObjects)
        {
            hideAnimatables.Add(hideGameObject.GetComponent<IHideAnimatable>());
        }
        
        AnimateHide();
        ApplicationManager.Instance.CompleteWork(ApplicationManager.InitState.UI);
    }

    private void Terminate()
    {
        PrimarySidebar.Instance.ViewChanged -= ViewChangedHandler;
    }

    private void ViewChangedHandler(ViewType changeTo)
    {
        if (viewType == changeTo)
        {
            PrimarySidebar.Instance.ShowAnimation = AnimateShow();
        }
        else if (viewType == PrimarySidebar.Instance.CurrentViewType)
        {
            PrimarySidebar.Instance.HideAnimation = AnimateHide();
        }
    }

    public Task AnimateShow()
    {
        List<Task> showTasks = new();

        foreach (var animatable in showAnimatables)
        {
            showTasks.Add(animatable.AnimateShow());
        }

        return Task.WhenAll(showTasks);
    }

    public Task AnimateHide()
    {
        List<Task> hideTasks = new();

        foreach (var animatable in hideAnimatables)
        {
            hideTasks.Add(animatable.AnimateHide());
        }

        return Task.WhenAll(hideTasks);
    }
}