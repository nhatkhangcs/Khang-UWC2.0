using System;
using UnityEngine;


public abstract class MapEntity : MonoBehaviour
{
    protected virtual void Awake()
    {
        ApplicationManager.Instance.AddInitWork(Init, ApplicationManager.InitState.UI);
        ApplicationManager.Instance.AddTerminateWork(Terminate, ApplicationManager.TerminateState.UI);
    }

    protected virtual void Init()
    {
        MapWrapper.Instance.MapUpdated += MapUpdatedHandler;
        ApplicationManager.Instance.CompleteWork(ApplicationManager.InitState.UI);
    }

    protected virtual void Terminate()
    {
        MapWrapper.Instance.MapUpdated -= MapUpdatedHandler;
    }

    protected abstract void MapUpdatedHandler();
}