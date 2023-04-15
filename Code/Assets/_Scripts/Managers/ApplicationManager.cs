using System;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : PersistentSingleton<ApplicationManager>
{
    // Initialize states. The program will start with the works tagged with the first state, then gradually move to the next state.
    public enum InitState
    {
        Data,
        Map,
        UI,
    }

    // Similar to InitState, this enum is used to tag terminate/cleanup work.
    public enum TerminateState
    {
        Map,
        UI,
    }

    private Dictionary<InitState, int> workCountByInitState = new()
    {
        {InitState.Data, 0}, {InitState.Map, 0}, {InitState.UI, 0},
    };

    // Event used for important classes to subscribe their initialize works to.
    public event Action<InitState> InitEventFlow;

    // Similar to InitEventFlow.
    public event Action<TerminateState> TerminateEventFlow;

    [SerializeField] private GameObject loadPanelGO;

    private void Start()
    {
        MapWrapper.Instance.abstractMap.OnInitialized += Init;
    }

    private void Init()
    {
        InitEventFlow?.Invoke(InitState.Data);
    }

    private void Terminate()
    {
    }

    public void CompleteWork(InitState state)
    {
        workCountByInitState[state]--;
        Debug.Log(state + ": " + workCountByInitState[state]);
        if (workCountByInitState[state] > 0) return;

        switch (state)
        {
            case InitState.Data:
                InitEventFlow?.Invoke(InitState.Map);
                Debug.Log("Completed Data");
                break;
            case InitState.Map:
                InitEventFlow?.Invoke(InitState.UI);
                Debug.Log("Completed Map");
                loadPanelGO.GetComponent<IHideAnimatable>().AnimateHide();
                break;
            case InitState.UI:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public void AddInitWork(Action initWork, InitState state)
    {
        workCountByInitState[state]++;

        InitEventFlow += (s) =>
        {
            if (s == state) initWork?.Invoke();
        };
    }

    public void AddTerminateWork(Action terminateWork, TerminateState state)
    {
        TerminateEventFlow += (s) =>
        {
            if (s == state) terminateWork?.Invoke();
        };
    }
}