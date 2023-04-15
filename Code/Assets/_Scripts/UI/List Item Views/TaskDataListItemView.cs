using UnityEngine;
using UnityEngine.UI;

public class TaskDataListItemView : DataListItemView<TaskData>
{
    [SerializeField] private Button dragHandle;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button editButton;
    public override void SetData(TaskData data)
    {
        base.SetData(data);

        PrimaryText = DatabaseManager.Instance.GetMCPDataByID(data.MCPID).Address;
        UpdateView();
    }
    
    protected override void UpdateView()
    {
        primaryText_TMP.text = PrimaryText;
    }
}