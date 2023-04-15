using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapbox.Utils;
using UnityEngine;


public class MapManager : PersistentSingleton<MapManager>
{
    [SerializeField] private Transform mapTransform;
    [SerializeField] private MapWrapper mapWrapper;

    public CollectorInformationPopup CollectorInformationPopup;
    public MCPInformationPopup MCPInformationPopup;

    public string ViewingCollectorID;
    public RoutePolyline RoutePolyline;

    protected override void Awake()
    {
        base.Awake();
        ApplicationManager.Instance.AddInitWork(Init, ApplicationManager.InitState.Map);
    }

    private void Init()
    {
        foreach (var staffData in DatabaseManager.Instance.AllStaffs)
        {
            if (staffData.Role == Role.Janitor)
            {
                var janitor = Instantiate(ResourceManager.Instance.JanitorMapEntity, mapTransform)
                    .GetComponent<JanitorMapEntity>();

                janitor.transform.SetParent(mapTransform);
                janitor.Init(staffData);
            }

            if (staffData.Role == Role.Collector)
            {
                var collector = Instantiate(ResourceManager.Instance.CollectorMapEntity, mapTransform)
                    .GetComponent<CollectorMapEntity>();

                collector.transform.SetParent(mapTransform);
                collector.Init(staffData);
            }
        }

        foreach (var mcpData in DatabaseManager.Instance.AllMCPs)
        {
            var mcp = Instantiate(ResourceManager.Instance.MCPMapEntity, mapTransform)
                .GetComponent<MCPMapEntity>();

            mcp.transform.SetParent(mapTransform);
            mcp.Init(mcpData);
        }

        HideRoutePolyline();
        StartCoroutine(UpdateRoutePolyline_CO());

        ApplicationManager.Instance.CompleteWork(ApplicationManager.InitState.Map);
    }

    public Vector2d WorldToGeoPosition(Vector2 coordinate)
    {
        return mapWrapper.WorldToGeoPosition(coordinate);
    }

    public Vector2 GeoToWorldPosition(Vector2d coordinate)
    {
        return mapWrapper.GeoToWorldPosition(coordinate);
    }

    public void GetRoute(List<Vector2d> route, Action<bool, List<Vector2d>> callback)
    {
        mapWrapper.GetRoute(route, callback);
    }

    public void ShowRoutePolyline(List<Coordinate> coordinates)
    {
        RoutePolyline.gameObject.SetActive(true);
        RoutePolyline.UpdateCoordinates(coordinates);
    }

    public void HideRoutePolyline()
    {
        RoutePolyline.gameObject.SetActive(false);
    }

    private IEnumerator UpdateRoutePolyline_CO()
    {
        while (true)
        {
            if (ViewingCollectorID != String.Empty)
            {
                BackendCommunicator.Instance.MapAPICommunicator.GetCollectorPosition(ViewingCollectorID,
                    (isSucceeded, routeTraversedData) =>
                    {
                        if (isSucceeded)
                        {
                            routeTraversedData.Route[0] = routeTraversedData.CurrentPos;
                            ShowRoutePolyline(routeTraversedData.Route);
                        }
                    });
                yield return new WaitForSeconds(4f);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}