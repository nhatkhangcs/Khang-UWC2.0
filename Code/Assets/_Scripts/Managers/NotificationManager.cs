using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class NotificationManager : PersistentSingleton<NotificationManager>
{
    private const float NOTIFICATION_SHOW_TIME = 5f;

    private Queue<NotificationData> notificationQueue = new();
    private WaitForSeconds waitForSeconds;

    private Notification currentNotification;

    private void Start()
    {
        waitForSeconds = new WaitForSeconds(NOTIFICATION_SHOW_TIME);
        StartCoroutine(ContinuouslyShowNotification_CO());
    }

    public void EnqueueNotification(NotificationData notificationData)
    {
        notificationQueue.Enqueue(notificationData);
    }

    private IEnumerator ContinuouslyShowNotification_CO()
    {
        while (true)
        {
            if (notificationQueue.Count == 0)
            {
                yield return null;
                continue;
            }

            var notification = ResourceManager.Instance.Notification;
            currentNotification = Instantiate(notification, transform).GetComponent<Notification>();
            currentNotification.SetContent(notificationQueue.Dequeue());
            yield return currentNotification.AnimateShow();
            yield return waitForSeconds;
            yield return currentNotification.AnimateHide();
        }
    }
}