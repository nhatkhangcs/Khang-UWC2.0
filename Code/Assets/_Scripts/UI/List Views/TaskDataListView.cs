using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDataListView : DataListView<TaskData>
{
    [SerializeField] private RectTransform headingToBoxRectTransform;
    [SerializeField] private Image upperFade;
    [SerializeField] private Image lowerFade;

    private List<TaskData> taskData;

    public void ShowTodayTasksOf(StaffData staffData)
    {
        RemoveAllItem();

        BackendCommunicator.Instance.TaskDatabaseCommunicator.GetAllTaskOfEmployeeByDate(staffData.ID,
            DateTime.Today, (isSucceeded, list) =>
            {
                if (isSucceeded)
                {
                    Init(list);
                }
            });
    }

    public void Init(List<TaskData> taskData)
    {
        base.Init();

        VERTICAL_SPACING = 0f;

        this.taskData = taskData;

        foreach (var data in taskData)
        {
            AddDataItem(data);
        }
    }

    protected override void Init()
    {
        prefab = ResourceManager.Instance.TaskDataListItemView;
    }
}