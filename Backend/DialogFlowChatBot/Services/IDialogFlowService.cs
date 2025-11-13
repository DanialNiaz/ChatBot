using DialogFlowChatBot.DTOs;

namespace DialogFlowChatBot.Services
{
    public interface IDialogFlowService
    {
        //Task<string> DetectIntentAsync(string sessionId, string text);
        Task<BaseResponseModel<ChatBotResponse>> GetDialogFlowResponseAsync(string userMessage, string sessionId);
    }
}
