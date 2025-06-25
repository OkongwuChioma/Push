using GooglePushNotification.Model;
using GooglePushNotification.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GooglePushNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly FCMService _fcmService;
        public NotificationController(FCMService fcmService)
        {
            _fcmService = fcmService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] PushNotificationModel model)
        {
            await _fcmService.SendNotificationAsync(model.DeviceToken, model.Title, model.Body);
            return Ok("Notification sent!");
        }
    }
}
