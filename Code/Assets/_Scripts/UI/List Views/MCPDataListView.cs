using UnityEngine;

public class MCPDataListView : DataListView<MCPData>
{
    protected override void Init()
    {
        base.Init();

        prefab = ResourceManager.Instance.MCPDataListItemView;

        var allMCPs = DatabaseManager.Instance.AllMCPs;
        foreach (var mcpData in allMCPs)
        {
            AddDataItem(mcpData);
        }
    }

    public void Choose(string mcpId)
    {
        foreach (var itemView in itemViews)
        {
            if (itemView is MCPDataListItemView mcpDataListItemView)
            {
                if (mcpDataListItemView.Data.ID == mcpId) mcpDataListItemView.Choose();
            }
        }
    }
}