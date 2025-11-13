using DialogFlowChatBot.DTOs;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DialogFlowChatBot.Services
{
    public class DialogFlowService : IDialogFlowService
    {
        private readonly HttpClient _httpClient;
        private readonly string _projectId = "chatbot-i9hq";
        private readonly string _credentialsPath = @"D:\ChatBot\Backend\ChatBotJsonKey.json";

        public DialogFlowService()
        {
            _httpClient = new HttpClient();
        }

        private async Task<string> GetAccessTokenAsync()
        {
            // Load service account credentials
            GoogleCredential credential;
            using (var stream = new FileStream(_credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped("https://www.googleapis.com/auth/cloud-platform");
            }

            var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
            return accessToken;
        }

        public async Task<BaseResponseModel<ChatBotResponse>> GetDialogFlowResponseAsync(string userMessage, string sessionId)
        {
            var response = new BaseResponseModel<ChatBotResponse>(true);

            var accessToken = await GetAccessTokenAsync();

            var url = $"https://dialogflow.googleapis.com/v2/projects/{_projectId}/agent/sessions/{sessionId}:detectIntent";

            var requestBody = new
            {
                query_input = new
                {
                    text = new
                    {
                        text = userMessage,
                        language_code = "en"
                    }
                }
            };

            var jsonRequest = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var httpResponse = await _httpClient.PostAsync(url, content);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {

                response.Messages = new List<string> { $"error: {jsonResponse}" };
                return response;
            }

            // Deserialize Dialogflow JSON response
            var dialogflowResult = JsonConvert.DeserializeObject<ChatBotResponse>(jsonResponse);

            string departureCity = string.Empty;
            string arrivalCity = string.Empty;

            var depField = dialogflowResult?.QueryResult?.Parameters?.Fields?.DepartureCity;
            var arrField = dialogflowResult?.QueryResult?.Parameters?.Fields?.ArivalCity;

            if (!string.IsNullOrWhiteSpace(depField?.StringValue))
                departureCity = depField.StringValue;
            else if (depField?.ListValue?.Values?.Any() == true)
                departureCity = depField.ListValue.Values.FirstOrDefault()?.StringValue ?? "N/A";

            if (!string.IsNullOrWhiteSpace(arrField?.StringValue))
                arrivalCity = arrField.StringValue;
            else if (arrField?.ListValue?.Values?.Any() == true)
                arrivalCity = arrField.ListValue.Values.FirstOrDefault()?.StringValue ?? "N/A";

            response.Result = dialogflowResult;
            response.DepartureCity = departureCity;
            response.ArrivalCity = arrivalCity;

            return response;
        }
    }
}
