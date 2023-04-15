using System;
using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageDataListView : DataListView<MessageData>
{
    [SerializeField] private Chatbox chatbox;
    [SerializeField] private Image profilePicture;
    [SerializeField] private TMP_Text accountName;
    [SerializeField] private TMP_Text status;

    private StaffData staffData;

    protected override void Init()
    {
        base.Init();

        VERTICAL_SPACING = 20f;
        prefab = ResourceManager.Instance.MessageDataListItemView;

        ListViewManager.Instance.InboxListView.InboxChosen += InboxChosenHandler;
        PrimarySidebar.Instance.ViewChanged += (viewType) => staffData = null;
        StartCoroutine(RefreshInbox_CO());
    }

    IEnumerator RefreshInbox_CO()
    {
        while (true)
        {
            if (staffData != null)
                InboxChosenHandler(staffData);

            yield return new WaitForSeconds(5f);
        }
    }

    private void InboxChosenHandler(StaffData staffData)
    {
        RemoveAllItemViews();

        this.staffData = staffData;
        BackendCommunicator.Instance.MessageDatabaseCommunicator.GetMessage(staffData.ID,
            (isSucceeded, data) =>
            {
                if (isSucceeded)
                {
                    data.Sort((messageA, messageB) => messageA.Timestamp > messageB.Timestamp ? 1 : -1);
                    foreach (var messageData in data)
                    {
                        AddDataItem(messageData);
                        // profilePicture.sprite = [something];
                        accountName.text = staffData.Name;
                        status.text = staffData.Role;
                        scrollRect.content.anchoredPosition =
                            new Vector2(0, scrollRect.content.sizeDelta.y - 600);

                        chatbox.RecipientID = staffData.ID;
                    }
                }
            });
    }

    private void OnDestroy()
    {
        if (ListViewManager.Instance != null && ListViewManager.Instance.InboxListView != null)
            ListViewManager.Instance.InboxListView.InboxChosen -= InboxChosenHandler;
    }

    public override Task AnimateHide()
    {
        return rectTransform.DOAnchorPosX(2000, VisualManager.Instance.ListAndPanelTime)
            .SetEase(Ease.InCubic)
            .AsyncWaitForCompletion();
    }
}