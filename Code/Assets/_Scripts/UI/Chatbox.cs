using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class Chatbox : MonoBehaviour, IShowHideAnimatable
{
    [SerializeField] private MessageDataListView messageDataListView;
    [SerializeField] private TMP_InputField inputField;
    private RectTransform rectTransform;

    public string RecipientID;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        inputField.onSubmit.AddListener(content =>
        {
            if (RecipientID == String.Empty) return; 
            
            MessageData messageData = new()
            {
                SenderID = AccountManager.Instance.AccountID,
                ReceiverID = RecipientID,
                Date = DateTime.Now.ToString("dd/MM/yyyy"),
                Time = DateTime.Now.ToString("hh:mm"),
                Content = content
            };

            BackendCommunicator.Instance.MessageDatabaseCommunicator.AddMessage(messageData,
                isSucceeded =>
                {
                    if (isSucceeded)
                    {
                    }
                });
        });
    }

    public void SubmitMessage()
    {
        inputField.onSubmit.Invoke(inputField.text);
    }
    
    public virtual Task AnimateShow()
    {
        return rectTransform.DOAnchorPosX(0, VisualManager.Instance.ListAndPanelTime)
            .SetEase(Ease.OutCubic)
            .AsyncWaitForCompletion();
    }

    public virtual Task AnimateHide()
    {
        return rectTransform.DOAnchorPosX(2000, VisualManager.Instance.ListAndPanelTime)
            .SetEase(Ease.InCubic)
            .AsyncWaitForCompletion();
    }
}