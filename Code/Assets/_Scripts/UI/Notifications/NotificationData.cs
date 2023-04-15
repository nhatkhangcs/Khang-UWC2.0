public class NotificationData
{
    public NotificationType NotificationType;
    public string Content;

    public NotificationData(NotificationType notificationType, string content)
    {
        NotificationType = notificationType;
        Content = content;
    }
}