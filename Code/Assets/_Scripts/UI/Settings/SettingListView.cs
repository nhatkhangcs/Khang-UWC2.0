using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingListView : ListView
{
    private const float HORIZONTAL_MARGIN = 10f;

    private List<ITMPDeferrable> deferrables = new();

    protected override void Init()
    {
        var headerPrefab = ResourceManager.Instance.SettingSectionHeader;
        var itemPrefab = ResourceManager.Instance.SettingListItemView;
        var SM = SettingManager.Instance;
        var settingTitle = "";

        #region InterfaceSettings

        var interfaceHeader = Instantiate(headerPrefab).GetComponent<SettingSectionHeader>();
        settingTitle = LanguageTranslation.GetText(
            LanguageTranslation.TextType.Setting_Interface_Setting,
            LanguageTranslation.ReturnTextOption.Title_Case);
        interfaceHeader.Init(settingTitle, rectTransform.rect.width - HORIZONTAL_MARGIN * 2);

        AddItem(interfaceHeader);


        settingTitle = LanguageTranslation.GetText(LanguageTranslation.TextType.Setting_Dark_Theme,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var darkThemeOption = Instantiate(itemPrefab).GetComponent<SettingListItemView>();
        darkThemeOption.Init(settingTitle + ":", () => SM.DarkThemeOption,
            e => SM.DarkThemeOption = (ToggleOption)e, typeof(ToggleOption));
        AddItem(darkThemeOption);


        settingTitle = LanguageTranslation.GetText(LanguageTranslation.TextType.Setting_Colorblind_Mode,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var colorblindModeOption = Instantiate(itemPrefab).GetComponent<SettingListItemView>();
        colorblindModeOption.Init(settingTitle + ":", () => SM.ColorblindOption,
            e => SM.ColorblindOption = (ToggleOption)e, typeof(ToggleOption));

        AddItem(colorblindModeOption);

        settingTitle = LanguageTranslation.GetText(LanguageTranslation.TextType.Setting_Reduced_Motion,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var reducedMotionOption = Instantiate(itemPrefab).GetComponent<SettingListItemView>();
        reducedMotionOption.Init(settingTitle + ":", () => SM.ReducedMotionOption,
            e => SM.ReducedMotionOption = (ToggleOption)e, typeof(ToggleOption));

        AddItem(reducedMotionOption);

        settingTitle = LanguageTranslation.GetText(LanguageTranslation.TextType.Setting_Language,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var languageOption = Instantiate(itemPrefab).GetComponent<SettingListItemView>();
        languageOption.Init(settingTitle + ":", () => SM.LanguageOption,
            e => SM.LanguageOption = (LanguageOption)e, typeof(LanguageOption));

        AddItem(languageOption);

        #endregion

        #region NotificationSettings

        settingTitle = LanguageTranslation.GetText(
            LanguageTranslation.TextType.Setting_Notification_Setting,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var notificationHeader = Instantiate(headerPrefab).GetComponent<SettingSectionHeader>();
        notificationHeader.Init(settingTitle, rectTransform.rect.width - HORIZONTAL_MARGIN * 2);

        AddItem(notificationHeader);

        settingTitle = LanguageTranslation.GetText(LanguageTranslation.TextType.Setting_Message,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var messageNotificationOption = Instantiate(itemPrefab).GetComponent<SettingListItemView>();
        messageNotificationOption.Init(settingTitle + ":", () => SM.MessageNotificationOption,
            e => SM.MessageNotificationOption = (ToggleOption)e, typeof(ToggleOption));

        AddItem(messageNotificationOption);

        settingTitle = LanguageTranslation.GetText(
            LanguageTranslation.TextType.Setting_Employees_Logged_In,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var employeesLoggedInNotificationOption =
            Instantiate(itemPrefab).GetComponent<SettingListItemView>();
        employeesLoggedInNotificationOption.Init(settingTitle + ":",
            () => SM.EmployeesLoggedInNotificationOption,
            e => SM.EmployeesLoggedInNotificationOption = (ToggleOption)e, typeof(ToggleOption));

        AddItem(employeesLoggedInNotificationOption);

        settingTitle = LanguageTranslation.GetText(
            LanguageTranslation.TextType.Setting_Employees_Logged_Out,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var employeesLoggedOutNotificationOption =
            Instantiate(itemPrefab).GetComponent<SettingListItemView>();
        employeesLoggedOutNotificationOption.Init(settingTitle + ":",
            () => SM.EmployeesLoggedOutNotificationOption,
            e => SM.EmployeesLoggedOutNotificationOption = (ToggleOption)e, typeof(ToggleOption));

        AddItem(employeesLoggedOutNotificationOption);

        settingTitle = LanguageTranslation.GetText(
            LanguageTranslation.TextType.Setting_MCPs_Fully_Loaded,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var MCPsFullyLoadedNotificationOption =
            Instantiate(itemPrefab).GetComponent<SettingListItemView>();
        MCPsFullyLoadedNotificationOption.Init(settingTitle + ":",
            () => SM.MCPsFullyLoadedNotificationOption,
            e => SM.MCPsFullyLoadedNotificationOption = (ToggleOption)e, typeof(ToggleOption));

        AddItem(MCPsFullyLoadedNotificationOption);

        settingTitle = LanguageTranslation.GetText(LanguageTranslation.TextType.Setting_MCPs_Emptied,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var MCPsEmptiedNotificationOption = Instantiate(itemPrefab).GetComponent<SettingListItemView>();
        MCPsEmptiedNotificationOption.Init(settingTitle + ":", () => SM.MCPsEmptiedNotificationOption,
            e => SM.MCPsEmptiedNotificationOption = (ToggleOption)e, typeof(ToggleOption));

        AddItem(MCPsEmptiedNotificationOption);

        settingTitle = LanguageTranslation.GetText(
            LanguageTranslation.TextType.Setting_Maintenance_Log_Updated,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var maintenanceLogsUpdatedNotificationOption =
            Instantiate(itemPrefab).GetComponent<SettingListItemView>();
        maintenanceLogsUpdatedNotificationOption.Init(settingTitle + ":",
            () => SM.MaintenanceLogsUpdatedNotificationOption,
            e => SM.MaintenanceLogsUpdatedNotificationOption = (ToggleOption)e, typeof(ToggleOption));

        AddItem(maintenanceLogsUpdatedNotificationOption);

        settingTitle = LanguageTranslation.GetText(
            LanguageTranslation.TextType.Setting_Software_Update_Available,
            LanguageTranslation.ReturnTextOption.Sentence_case);
        var softwareUpdateAvailableNotificationOption =
            Instantiate(itemPrefab).GetComponent<SettingListItemView>();
        softwareUpdateAvailableNotificationOption.Init(settingTitle + ":",
            () => SM.SoftwareUpdateAvailableNotificationOption,
            e => SM.SoftwareUpdateAvailableNotificationOption = (ToggleOption)e, typeof(ToggleOption));

        AddItem(softwareUpdateAvailableNotificationOption);

        #endregion

        SettingManager.Instance.StartCoroutine(ExecuteDeferredWork());
    }

    public override void AddItem(ListItemView itemView)
    {
        base.AddItem(itemView);
        deferrables.Add(itemView.GetComponent<ITMPDeferrable>());
    }

    private IEnumerator ExecuteDeferredWork()
    {
        yield return null;
        yield return new WaitForSeconds(0.5f);
        foreach (var deferrable in deferrables)
        {
            deferrable.ExecuteDeferredWork();
        }
    }
}