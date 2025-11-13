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

        #region fields
        private readonly HttpClient _httpClient;
        private readonly string _projectId = "chatbot-i9hq";
        private readonly string _credentialsPath = @"D:\ChatBot\Backend\ChatBotJsonKey.json";
        #endregion

        #region ctor
        public DialogFlowService()
        {
            _httpClient = new HttpClient();
        }

        #endregion

        #region method
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
            // Create a response model object with default success = true
            var response = new BaseResponseModel<ChatBotResponse>(true);

            // Get a valid access token using the service account JSON key
            var accessToken = await GetAccessTokenAsync();

            // Dialogflow REST API endpoint for DetectIntent
            // sessionId ensures a unique conversation context per user
            var url = $"https://dialogflow.googleapis.com/v2/projects/{_projectId}/agent/sessions/{sessionId}:detectIntent";

            // Build the request body for Dialogflow DetectIntent
            // It specifies the query input type (text) and the language code
            var requestBody = new
            {
                query_input = new
                {
                    text = new
                    {
                        text = userMessage,       // The user's message
                        language_code = "en"      
                    }
                }
            };

            // Serialize the request object to JSON
            var jsonRequest = JsonConvert.SerializeObject(requestBody);

            // Wrap the JSON in an HttpContent object with correct encoding and content type
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            //Authorization : Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var httpResponse = await _httpClient.PostAsync(url, content);

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                // If failed, set error message in response and return immediately
                response.Messages = new List<string> { $"error: {jsonResponse}" };
                return response;
            }

            // Deserialize the JSON response from Dialogflow into ChatBotResponse DTO
            var dialogflowResult = JsonConvert.DeserializeObject<ChatBotResponse>(jsonResponse);

            
            // Assign the Dialogflow response and extracted cities to the BaseResponseModel
            response.Result = dialogflowResult;

            // Return the final response object
            return response;
        }
        
        #endregion

    }
}
