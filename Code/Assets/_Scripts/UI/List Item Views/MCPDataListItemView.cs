using UnityEngine;

public class MCPDataListItemView : DataListItemView<MCPData>
{
    public override void SetData(MCPData data)
    {
        base.SetData(data);

        PrimaryText = data.Address;
        SecondaryText = Mathf.CeilToInt(data.StatusPercentage) + "%";

        ChangeIconColor(data.StatusPercentage / 100f);
        UpdateView();

        button.onClick.AddListener(() => MCPInformationPanel.Instance.Show(data));
        data.ValueChanged += ValueChangedHandler;
    }

    private void ChangeIconColor(float capacity)
    {
        if (capacity < SystemConstants.MCP.AlmostFullThreshold)
        {
            image.color = VisualManager.Instance.MCPNotFullColor;
        }
        else if (capacity < SystemConstants.MCP.FullyLoadedThreshold)
        {
            image.color = VisualManager.Instance.MCPAlmostFullColor;
        }
        else
        {
            image.color = VisualManager.Instance.MCPFullyLoadedColor;
        }
    }

    protected override void UpdateView()
    {
        primaryText_TMP.text = PrimaryText;
        secondaryText_TMP.text = SecondaryText;
    }

    public void ValueChangedHandler()
    {
        var percentage = Data.StatusPercentage;

        ChangeIconColor(percentage / 100f);
        SecondaryText = percentage.ToString("F0") + "%";
        
        UpdateView();
    }
}