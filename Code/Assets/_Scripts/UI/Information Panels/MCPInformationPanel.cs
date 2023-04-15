using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MCPInformationPanel : InformationPanel<MCPData>
{
    [SerializeField] private TMP_Text address;
    [SerializeField] private Image capacityBar;
    [SerializeField] private Image capacityTextBackground;
    [SerializeField] private TMP_Text status;
    [SerializeField] private TMP_Text capacityPercentage;

    protected override void SetData(MCPData data)
    {
        base.SetData(data);

        address.text = data.Address;
        capacityBar.transform.localScale = new Vector3(data.StatusPercentage / 100f, 1, 1);
        status.text = VisualManager.Instance.GetMCPStatusText(data.StatusPercentage / 100f);
        capacityPercentage.text = Mathf.CeilToInt(data.StatusPercentage) + "%";

        var mcpColor = VisualManager.Instance.GetMCPColor(data.StatusPercentage / 100f);
        capacityBar.color = capacityTextBackground.color = mcpColor;
    }
}