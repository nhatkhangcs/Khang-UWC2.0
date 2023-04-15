using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class AssigningMCPListView : DataListView<MCPData>
{
    [SerializeField] private Button assignNowButton;
    [SerializeField] private Button scheduleButton;
    [SerializeField] private Calendar calendar;

    private Vector3 initialAssignButtonAnchorPos;
    private Vector3 initialScheduleButtonAnchorPos;

    protected override void Awake()
    {
        base.Awake();

        initialAssignButtonAnchorPos = assignNowButton.GetComponent<RectTransform>().anchoredPosition;
        initialScheduleButtonAnchorPos = scheduleButton.GetComponent<RectTransform>().anchoredPosition;
    }

    protected override void Init()
    {
        base.Init();

        prefab = ResourceManager.Instance.AssigningMcpListItemView;

        AnimateHide();


        calendar.AssignAction((date) =>
        {
            BackendCommunicator.Instance.VehicleDatabaseCommunicator.GetAllVehicle((isSucceeded, list) =>
            {
                if (!isSucceeded) return;

                var randomVehicle = list[Random.Range(0, list.Count - 1)];

                foreach (var itemView in itemViews)
                {
                    if (itemView is DataListItemView<MCPData> dataListItemView)
                    {
                        AddTaskPayload taskData = new AddTaskPayload()
                        {
                            EmployeeID = StaffInformationPanel.Instance.Data.ID,
                            MCPID = dataListItemView.Data.ID,
                            VehicleID = randomVehicle.ID,
                            Timestamp = date.AddHours(-7).ToString("yy/MM/dd hh:mm:ss"),
                            CheckedIn = 0,
                            CheckedOut = 0,
                        };
                        BackendCommunicator.Instance.TaskDatabaseCommunicator.AddTask(taskData,
                            addSuccessfully =>
                            {
                                if (addSuccessfully) Debug.Log("added task");
                            });
                    }
                    else throw new Exception();
                }

                AnimateHide();

                MCPMapEntity.GroupingSelect = false;
            });
        });

        scheduleButton.onClick.RemoveAllListeners();
        scheduleButton.onClick.AddListener(() =>
        {
            calendar.gameObject.SetActive(true);
            assignNowButton.transform.position = Vector3.down * 1000;
            scheduleButton.transform.position = Vector3.down * 1000;
        });


        assignNowButton.onClick.RemoveAllListeners();
        assignNowButton.onClick.AddListener(() =>
        {
            BackendCommunicator.Instance.MapAPICommunicator.GetCollectorPosition(
                StaffInformationPanel.Instance.Data.ID, (isSucceeded, routeTraversedData) =>
                {
                    if (!isSucceeded) return;

                    CollectorRouteData collectorRouteData = new();
                    List<Vector2d> route = new List<Vector2d>();
                    route.Add(new Vector2d(routeTraversedData.CurrentPos.Latitude,
                        routeTraversedData.CurrentPos.Longitude));

                    collectorRouteData.Route = new List<Coordinate>();
                    foreach (var itemView in itemViews)
                    {
                        if (itemView is DataListItemView<MCPData> dataListItemView)
                        {
                            route.Add(new Vector2d(dataListItemView.Data.Latitude,
                                dataListItemView.Data.Longitude));
                        }
                        else throw new Exception();
                    }

                    MapManager.Instance.GetRoute(route, (getRouteSucceeded, waypoints) =>
                    {
                        if (!getRouteSucceeded)
                        {
                            return;
                        }

                        foreach (var waypoint in waypoints)
                        {
                            collectorRouteData.Route.Add(new Coordinate(waypoint.x, waypoint.y));
                        }

                        collectorRouteData.CollectorId = StaffInformationPanel.Instance.Data.ID;
                        BackendCommunicator.Instance.MapAPICommunicator.SetCollectorWaypoints(
                            collectorRouteData, isSucceeded =>
                            {
                                if (isSucceeded)
                                {
                                    NotificationManager.Instance.EnqueueNotification(
                                        new NotificationData(NotificationType.Info,
                                            LanguageTranslation.GetText(
                                                LanguageTranslation.TextType.MCP_Assign_Succeed,
                                                LanguageTranslation.ReturnTextOption.Sentence_case) +
                                            "."));
                                }
                                else
                                {
                                    NotificationManager.Instance.EnqueueNotification(
                                        new NotificationData(NotificationType.Warning,
                                            LanguageTranslation.GetText(
                                                LanguageTranslation.TextType.MCP_Assign_Fail,
                                                LanguageTranslation.ReturnTextOption.Sentence_case) +
                                            "."));
                                }

                                AnimateHide();
                            });
                    });
                });
        });
    }

    public override Task AnimateHide()
    {
        RemoveAllItem();

        assignNowButton.GetComponent<RectTransform>().anchoredPosition = initialAssignButtonAnchorPos;
        scheduleButton.GetComponent<RectTransform>().anchoredPosition = initialScheduleButtonAnchorPos;
        calendar.gameObject.SetActive(false);

        StartCoroutine(DeferredCall_CO());
        return base.AnimateHide();
    }

    private IEnumerator DeferredCall_CO()
    {
        yield return null;
        yield return new WaitForSeconds(1f);
        MCPMapEntity.GroupingSelect = false;
    }
}