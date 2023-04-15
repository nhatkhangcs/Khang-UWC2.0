using System;
using UnityEngine;
using UnityEngine.UI;

public class AssigningMCPListItemView : MCPDataListItemView
{
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;

    private void Start()
    {
        moveUpButton.onClick.AddListener(() =>
        {
            ListViewManager.Instance.AssigningMCPListView.MoveItemUp(Data);
        });
        moveDownButton.onClick.AddListener(() =>
            ListViewManager.Instance.AssigningMCPListView.MoveItemDown(Data));
    }
}