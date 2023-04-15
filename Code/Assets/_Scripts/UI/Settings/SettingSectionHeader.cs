using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingSectionHeader : ListItemView, ITMPDeferrable
{
    private const float LINE_MARGIN_LEFT = 10f;

    [SerializeField] private TMP_Text header;
    [SerializeField] private RectTransform lineRectTransform;

    private float totalWidth;

    public void Init(string headerText, float totalWidth)
    {
        header.text = headerText;
        header.ForceMeshUpdate();
        this.totalWidth = totalWidth;
    }

    public void ExecuteDeferredWork()
    {
        var newWidth = totalWidth - header.textBounds.size.x - LINE_MARGIN_LEFT;
        lineRectTransform.sizeDelta = new Vector2(newWidth, lineRectTransform.sizeDelta.y);
    }
}