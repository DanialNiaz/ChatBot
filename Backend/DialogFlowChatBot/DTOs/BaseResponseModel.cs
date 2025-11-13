using System.Net;

namespace DialogFlowChatBot.DTOs
{
    public class BaseResponseModel<T>
    {
        public BaseResponseModel()
        {
        }

        public BaseResponseModel(bool setDefaultSuccessResponse)
        {
            if (setDefaultSuccessResponse)
            {
                StatusCode = (int)HttpStatusCode.OK;
                Messages = new List<string>() { "Processed successfully" };
            }
        }


        public int StatusCode { get; set; }
        public List<string> Messages { get; set; }
        public T Result { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }

    }
}
