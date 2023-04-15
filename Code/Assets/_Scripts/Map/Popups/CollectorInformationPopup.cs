using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectorInformationPopup : MapPopup<StaffData>
{
    [SerializeField] private Image profilePicture;
    [SerializeField] private TMP_Text staffName;
    [SerializeField] private TMP_Text drivingVehicle;

    [SerializeField] private Button assignTaskButton;
    [SerializeField] private Button sendMessageButton;
    [SerializeField] private Button detailedInformationButton;

    [SerializeField] private TaskDataListView taskDataListView;

    private RectTransform rectTransform;

    private void Awake()
    {
        ApplicationManager.Instance.AddInitWork(Init, ApplicationManager.InitState.UI);
    }

    private void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        ApplicationManager.Instance.CompleteWork(ApplicationManager.InitState.UI);
    }

    public override void Show(StaffData data, Vector2d position)
    {
        gameObject.SetActive(true);

        staffName.text = data.Name;
        drivingVehicle.text = "is driving " + "[vehicle type]" + " " + "[vehicle plate]";
        coordinate = position;

        transform.SetSiblingIndex(transform.parent.childCount - 1);

        taskDataListView.ShowTodayTasksOf(data);

        MapUpdatedHandler();
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }
}