using System.Collections;
using TMPro;
using UnityEngine;

public class MessageDataListItemView : DataListItemView<MessageData>
{
    private Vector2 sizeDelta;

    public override void SetData(MessageData data)
    {
        base.SetData(data);

        PrimaryText = data.Content;
        SecondaryText = data.Timestamp.ToString("hh:mm:ss dd/MM/yy");

        if (data.SenderID == AccountManager.Instance.AccountID)
        {
            image.color = VisualManager.Instance.PrimaryColor;
            primaryText_TMP.color = VisualManager.Instance.WhiteTextColor;
            primaryText_TMP.alignment = TextAlignmentOptions.TopRight;
            secondaryText_TMP.alignment = TextAlignmentOptions.TopRight;
            rectTransform.anchorMin = Vector2.one;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.pivot = new Vector2(1, 1);
        }
        else
        {
            image.color = VisualManager.Instance.SubtleGreyColor;
        }

        UpdateView();
    }

    protected override void UpdateView()
    {
        primaryText_TMP.text = PrimaryText;
        secondaryText_TMP.text = SecondaryText;
        
        primaryText_TMP.ForceMeshUpdate();
        secondaryText_TMP.ForceMeshUpdate();
        
        var width = Mathf.Clamp(primaryText_TMP.preferredWidth, 0f, 800f);
        var textSizeDelta = primaryText_TMP.rectTransform.sizeDelta;

        rectTransform.sizeDelta = new Vector2(width - textSizeDelta.x, 0);

        primaryText_TMP.ForceMeshUpdate();

        var height = primaryText_TMP.preferredHeight;

        rectTransform.sizeDelta = new Vector2(width - textSizeDelta.x, height + 20 - textSizeDelta.y);
    }
}