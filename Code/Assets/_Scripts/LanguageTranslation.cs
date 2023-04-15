using System;
using System.Collections.Generic;
using System.Globalization;

public static class LanguageTranslation
{
    public enum TextType
    {
        Login,
        Login_Unfilled_Fields,
        Login_Failed,

        Staff_Role_Collector,
        Staff_Role_Janitor,
        Staff_Currently_At,
        Staff_Is_Driving,
        Staff_Assign_Task,
        Staff_View_Calendar,
        Staff_Send_Message,

        Major_Collecting_Point,
        MCP_Not_Full,
        MCP_Almost_Full,
        MCP_Fully_Loaded,
        MCP_Assign_Succeed,
        MCP_Assign_Fail,

        Vehicle_Vehicle,
        Vehicle_Model,
        Vehicle_Weight,
        Vehicle_Capacity,
        Vehicle_Fuel_Consumption,

        Setting_On,
        Setting_Off,
        Setting_English,
        Setting_Vietnamese,
        Setting_Interface_Setting,
        Setting_Theme,
        Setting_Dark_Theme,
        Setting_Colorblind_Mode,
        Setting_Reduced_Motion,
        Setting_Language,
        Setting_Notification_Setting,
        Setting_Message,
        Setting_Employees_Logged_In,
        Setting_Employees_Logged_Out,
        Setting_MCPs_Fully_Loaded,
        Setting_MCPs_Emptied,
        Setting_Maintenance_Log_Updated,
        Setting_Software_Update_Available,
    }

    private static Dictionary<LanguageOption, Dictionary<TextType, string>> Translation = new()
    {
        {
            LanguageOption.English,
            new Dictionary<TextType, string>
            {
                {TextType.Login, "login"},
                {TextType.Login_Unfilled_Fields, "some fields are not filled"},
                {TextType.Login_Failed, "login failed"},
                {TextType.Staff_Role_Collector, "collector"},
                {TextType.Staff_Role_Janitor, "janitor"},
                {TextType.Staff_Currently_At, "currently at"},
                {TextType.Staff_Is_Driving, "is driving"},
                {TextType.Staff_Assign_Task, "assign task"},
                {TextType.Staff_View_Calendar, "view calendar"},
                {TextType.Staff_Send_Message, "send message"},
                {TextType.Major_Collecting_Point, "major collecting point"},
                {TextType.MCP_Not_Full, "not full"},
                {TextType.MCP_Almost_Full, "almost full"},
                {TextType.MCP_Fully_Loaded, "fully loaded"},
                {TextType.MCP_Assign_Succeed, "MCPs assigned successfully"},
                {TextType.MCP_Assign_Fail, "failed to assign MCPs"},
                {TextType.Vehicle_Vehicle, "vehicle"},
                {TextType.Vehicle_Model, "model"},
                {TextType.Vehicle_Weight, "weight"},
                {TextType.Vehicle_Capacity, "capacity"},
                {TextType.Vehicle_Fuel_Consumption, "fuel consumption"},
                {TextType.Setting_On, "on"},
                {TextType.Setting_Off, "off"},
                {TextType.Setting_English, "english"},
                {TextType.Setting_Vietnamese, "vietnamese"},
                {TextType.Setting_Interface_Setting, "interface setting"},
                {TextType.Setting_Theme, "theme"},
                {TextType.Setting_Dark_Theme, "dark theme"},
                {TextType.Setting_Colorblind_Mode, "colorblind mode"},
                {TextType.Setting_Reduced_Motion, "reduced motion"},
                {TextType.Setting_Language, "language"},
                {TextType.Setting_Notification_Setting, "notification setting"},
                {TextType.Setting_Message, "message"},
                {TextType.Setting_Employees_Logged_In, "employees logged in"},
                {TextType.Setting_Employees_Logged_Out, "employees logged out"},
                {TextType.Setting_MCPs_Fully_Loaded, "MCPs fully loaded"},
                {TextType.Setting_MCPs_Emptied, "MCPs emptied"},
                {TextType.Setting_Maintenance_Log_Updated, "maintenance log updated"},
                {TextType.Setting_Software_Update_Available, "software update available"},
            }
        },
        {
            LanguageOption.Vietnamese,
            new Dictionary<TextType, string>
            {

                {TextType.Login, "đăng nhập"},
                {TextType.Staff_Assign_Task, "giao việc"},
                {TextType.Staff_View_Calendar, "xem lịch"},
                {TextType.Staff_Send_Message, "gửi tin"},

                {TextType.Login_Unfilled_Fields, "còn vài ô chưa điền"},
                {TextType.Login_Failed, "đăng nhập thất bại"},
                {TextType.Staff_Role_Collector, "người thu rác"},
                {TextType.Staff_Role_Janitor, "người dọn rác"},
                {TextType.Staff_Currently_At, "hiện đang ở"},
                {TextType.Staff_Is_Driving, "đang lái xe"},

                {TextType.Major_Collecting_Point, "điểm tập trung rác"},
                {TextType.MCP_Not_Full, "chưa đầy"},
                {TextType.MCP_Almost_Full, "gần đầy"},
                {TextType.MCP_Fully_Loaded, "đã đầy"},
                {TextType.MCP_Assign_Succeed, "ĐTTR gán thành công"},
                {TextType.MCP_Assign_Fail, "ĐTTR gán thất bại"},

                {TextType.Vehicle_Vehicle, "phương tiện"},
                {TextType.Vehicle_Model, "mẫu"},
                {TextType.Vehicle_Weight, "cân nặng"},
                {TextType.Vehicle_Capacity, "sức chứa"},
                {TextType.Vehicle_Fuel_Consumption, "mức tiêu thụ xăng"},
                {TextType.Setting_On, "bật"},
                {TextType.Setting_Off, "tắt"},
                {TextType.Setting_English, "tiếng anh"},
                {TextType.Setting_Vietnamese, "tiếng việt"},
                {TextType.Setting_Interface_Setting, "cài đặt giao diện"},
                {TextType.Setting_Theme, "chủ đề nền"},
                {TextType.Setting_Dark_Theme, "chủ đề nền tối"},
                {TextType.Setting_Colorblind_Mode, "chủ đề mù màu"},
                {TextType.Setting_Reduced_Motion, "giảm hiệu ứng"},
                {TextType.Setting_Language, "ngôn ngữ"},
                {TextType.Setting_Notification_Setting, "cài đặt thông báo"},
                {TextType.Setting_Message, "tin nhắn"},
                {TextType.Setting_Employees_Logged_In, "nhân viên đăng nhập"},
                {TextType.Setting_Employees_Logged_Out, "nhân viên đăng xuất"},
                {TextType.Setting_MCPs_Fully_Loaded, "ĐTTR đã đầy"},
                {TextType.Setting_MCPs_Emptied, "ĐTTR được dọn"},
                {TextType.Setting_Maintenance_Log_Updated, "biên bản bảo trì"},
                {TextType.Setting_Software_Update_Available, "cập nhật phần mềm"},
            }
        }
    };

    public enum ReturnTextOption
    {
        lower_case,
        UPPER_CASE,
        Sentence_case,
        Title_Case,
    }

    public static string GetText(TextType textType, ReturnTextOption returnTextOption)
    {
        var rawText = Translation[SettingManager.Instance.LanguageOption][textType];

        switch (returnTextOption)
        {
            case ReturnTextOption.lower_case:
                return rawText;
            case ReturnTextOption.UPPER_CASE:
                return rawText.ToUpper();
            case ReturnTextOption.Sentence_case:
                return rawText[0].ToString().ToUpper() + rawText[1..];
            case ReturnTextOption.Title_Case:
                return new CultureInfo("en-US", false).TextInfo.ToTitleCase(rawText);
            default:
                throw new ArgumentOutOfRangeException(nameof(returnTextOption), returnTextOption, null);
        }
    }
}