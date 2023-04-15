using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using MapboxDirectionRequestResult;
using UnityEngine.EventSystems;

public class MapWrapper : Singleton<MapWrapper>, IBeginDragHandler, IDragHandler, IScrollHandler
{
    public event Action MapUpdated;

    [SerializeField] public AbstractMap abstractMap;

    private Vector2 prevMousePos;
    private Vector2d start;
    private bool firstPointChosen = false;

    protected override void Awake()
    {
        base.Awake();
        ApplicationManager.Instance.AddInitWork(Init, ApplicationManager.InitState.Map);
        ApplicationManager.Instance.AddTerminateWork(Terminate, ApplicationManager.TerminateState.Map);
    }

    private void Init()
    {
        abstractMap.OnUpdated += OnMapUpdated;
        ApplicationManager.Instance.CompleteWork(ApplicationManager.InitState.Map);
    }

    private void Terminate()
    {
        abstractMap.OnUpdated -= OnMapUpdated;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var currentMousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseDelta = prevMousePos - currentMousePos;

        if (mouseDelta == prevMousePos)
        {
            prevMousePos = currentMousePos;
            return;
        }

        abstractMap.UpdateMap(abstractMap.WorldToGeoPosition(mouseDelta));
        prevMousePos = currentMousePos;
    }

    public void OnScroll(PointerEventData eventData)
    {
        var newZoomValue = Mathf.Clamp(abstractMap.Zoom + Input.mouseScrollDelta.y / 5f, 12, 20);
        abstractMap.UpdateMap(newZoomValue);
    }

    public void GetRoute(List<Vector2d> route, Action<bool, List<Vector2d>> callback)
    {
        StartCoroutine(ReadRequestResult(route, callback));
    }

    private string BuildRequestURI(List<Vector2d> route)
    {
        string head = "https://api.mapbox.com/directions/v5/mapbox/driving/";
        string tail = "?geometries=geojson&access_token=" + SystemConstants.Map.MapboxAccessToken;

        string uri = head;
        for (int i = 0; i < route.Count - 1; i++)
        {
            uri += route[i] + ";";
        }

        uri += route[^1] + tail;

        return uri;
    }

    private IEnumerator ReadRequestResult(List<Vector2d> route, Action<bool, List<Vector2d>> callback)
    {
        var request = UnityWebRequest.Get(BuildRequestURI(route));
        Debug.Log(request.uri);

        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);

        var result = JsonConvert.DeserializeObject<Result>(request.downloadHandler.text);
        var rawCoordinates = result.routes[0].geometry.coordinates;

        List<Vector2d> coordinateList = new();
        foreach (var coordinate in rawCoordinates)
        {
            coordinateList.Add(new Vector2d(coordinate[1], coordinate[0]));
        }

        if (request.result == UnityWebRequest.Result.Success) callback?.Invoke(true, coordinateList);
        else callback?.Invoke(false, null);
    }

    public void OnMapUpdated()
    {
        MapUpdated?.Invoke();
    }

    public Vector2d WorldToGeoPosition(Vector2 coordinate)
    {
        return abstractMap.WorldToGeoPosition(coordinate);
    }

    public Vector2 GeoToWorldPosition(Vector2d coordinate)
    {
        return abstractMap.GeoToWorldPosition(coordinate, false);
    }
}