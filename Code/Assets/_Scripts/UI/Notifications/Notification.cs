using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour, IShowHideAnimatable
{
    [SerializeField] private RectTransform notificationRectTransform;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text notificationType;
    [SerializeField] private TMP_Text contentText;

    public void SetContent(NotificationData data)
    {
        contentText.text = data.Content;
        contentText.ForceMeshUpdate();
        notificationRectTransform.sizeDelta = new Vector2(
            Mathf.Max(175 + contentText.preferredWidth, 350), notificationRectTransform.sizeDelta.y);
    }

    public Task AnimateShow()
    {
        notificationRectTransform.anchoredPosition = new Vector2(0, 200f);
        return notificationRectTransform.DOAnchorPosY(-25f, 0.3f)
            .SetEase(Ease.OutCubic)
            .AsyncWaitForCompletion();
    }

    public Task AnimateHide()
    {
        return notificationRectTransform.DOAnchorPosY(200f, 0.3f)
            .SetEase(Ease.OutCubic)
            .AsyncWaitForCompletion();
    }
}

public enum NotificationType
{
    Info,
    Warning,
    Error,
}