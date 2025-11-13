using DialogFlowChatBot.DTOs;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Net;
using System.Text;

namespace DialogFlowChatBot.Services
{
    public class WebSocketService
    {

        #region fields
        private readonly IDialogFlowService _dialogflowService;
        #endregion

        #region ctor
        public WebSocketService(IDialogFlowService dialogflowService)
        {
            _dialogflowService = dialogflowService;
        }
        #endregion

        #region method
        public async Task HandleChatAsync(WebSocket webSocket)
        {
            var buffer = new byte[4096];
            var sessionId = Guid.NewGuid().ToString();

            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    break;
                }

                if (result.MessageType != WebSocketMessageType.Text)
                    continue;

                var userMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

                try
                {
                    var dialogflowResponse = await _dialogflowService.GetDialogFlowResponseAsync(userMessage, sessionId);

                    var response = new BaseResponseModel<ChatBotResponse>(true)
                    {
                        Result = dialogflowResponse.Result,
                        DepartureCity = dialogflowResponse.DepartureCity,
                        ArrivalCity = dialogflowResponse.ArrivalCity,
                        StatusCode = (int)HttpStatusCode.OK,
                        Messages = new List<string> { "Processed successfully" }
                    };

                    var jsonResponse = JsonConvert.SerializeObject(response);
                    var replyBytes = Encoding.UTF8.GetBytes(jsonResponse);
                    await webSocket.SendAsync(new ArraySegment<byte>(replyBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    var errorResponse = new BaseResponseModel<ChatBotResponse>(false)
                    {
                        Result = new ChatBotResponse
                        {
                            ResponseId = Guid.NewGuid().ToString(),
                            QueryResult = new ChatBotResponse.Result
                            {
                                QueryText = userMessage,
                                FulfillmentText = $"Error: {ex.Message}"
                            }
                        }
                    };

                    var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
                    var errorBytes = Encoding.UTF8.GetBytes(jsonErrorResponse);
                    await webSocket.SendAsync(new ArraySegment<byte>(errorBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
        
        #endregion
    }
}
