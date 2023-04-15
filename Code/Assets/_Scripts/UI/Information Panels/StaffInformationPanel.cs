using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaffInformationPanel : InformationPanel<StaffData>
{
    [SerializeField] private TMP_Text staffName;
    [SerializeField] private TMP_Text role;
    [SerializeField] private TMP_Text genderAndAge;
    [SerializeField] private TMP_Text address;
    [SerializeField] private TMP_Text phoneNumber;

    [SerializeField] private Button assignTaskButton;
    [SerializeField] private Button viewCalendarButton;
    [SerializeField] private Button sendMessageButton;

    [SerializeField] private TaskDataListView taskDataListView;
    [SerializeField] private Calendar calendar;

    [SerializeField] private AssigningMCPListView assigningMcpListView;
    [SerializeField] private RectTransform taskAndCalendarRectTransform;
    private Vector2 initialTaskAndCalendarAnchorPos;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        assignTaskButton.onClick.AddListener(EnterAssignMode);
        viewCalendarButton.onClick.AddListener(EnterCalendarViewingMode);
        sendMessageButton.onClick.AddListener(GoToInbox);

        assignTaskButton.GetComponentInChildren<TMP_Text>().text = LanguageTranslation.GetText(
            LanguageTranslation.TextType.Staff_Assign_Task,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        viewCalendarButton.GetComponentInChildren<TMP_Text>().text = LanguageTranslation.GetText(
            LanguageTranslation.TextType.Staff_View_Calendar,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        sendMessageButton.GetComponentInChildren<TMP_Text>().text = LanguageTranslation.GetText(
            LanguageTranslation.TextType.Staff_Send_Message,
            LanguageTranslation.ReturnTextOption.Sentence_case);

        initialTaskAndCalendarAnchorPos = taskAndCalendarRectTransform.anchoredPosition;
        taskAndCalendarRectTransform.anchoredPosition = new Vector2(0, 0);

        MCPMapEntity.ToggleStateChanged += (entity, isSelected) =>
        {
            if (isSelected)
                assigningMcpListView.AddDataItem(entity.Data);
            else
                assigningMcpListView.RemoveDataItem(entity.Data);
        };
    }

    protected override void SetData(StaffData data)
    {
        base.SetData(data);

        staffName.text = data.Name;
        role.text = data.Role;
        genderAndAge.text = data.Gender + ", " + (DateTime.Today - data.DateOfBirth).Days / 365;
        address.text = data.HomeAddress;
        phoneNumber.text = data.PhoneNumber;
        taskDataListView.ShowTodayTasksOf(data);
    }

    private void EnterAssignMode()
    {
        MCPMapEntity.GroupingSelect = true;

        assigningMcpListView.AnimateShow();

        AnimateHide();
    }

    private void EnterCalendarViewingMode()
    {
        taskAndCalendarRectTransform.DOAnchorPos(initialTaskAndCalendarAnchorPos, 0.2f);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0);
    }

    private void GoToInbox()
    {
        PrimarySidebar.Instance.OnViewChanged(ViewType.MessagesOverview);
        ListViewManager.Instance.InboxListView.SelectInboxByStaffId(Data.ID);
    }
}