namespace GooglePushNotification.Model
{
    public class PushNotificationModel
    {
        public string DeviceToken { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Body { get; set; } = default!;
    }
}
