using Google.Apis.Auth.OAuth2;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GooglePushNotification.Service
{
    public class FCMService
    {
        private readonly string _projectId = "";
        private readonly string _firebaseKeyPath = @"App_Data\push-notification-dafd2-firebase-adminsdk-fbsvc-73ff61be89.json";
        private readonly HttpClient _httpClient;

        public FCMService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendNotificationAsync(string deviceToken, string title, string body)
        {
            var message = new
            {
                message = new
                {
                    token = deviceToken,
                    notification = new
                    {
                        title = title,
                        body = body
                    },
                    android = new
                    {
                        priority = "HIGH",
                        notification = new
                        {
                            channel_id = "default",
                            click_action = "OPEN_ACTIVITY_1"
                        }
                    }
                }
            };

            var jsonMessage = JsonSerializer.Serialize(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            //Get Access To The Credential                                                                                                       
            var credential = GoogleCredential.FromFile(_firebaseKeyPath).CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
            var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://fcm.googleapis.com/v1/projects/{_projectId}/messages:send")
            {
                Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result); // log response from FCM

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to send notification:");
                Console.WriteLine($"Status: {response.StatusCode}");
                Console.WriteLine($"Reason: {response.ReasonPhrase}");
                Console.WriteLine($"Details: {result}");
            }
            else
            {
                Console.WriteLine(" Notification sent successfully!");
                Console.WriteLine(result);
            }

        }

    }
}
