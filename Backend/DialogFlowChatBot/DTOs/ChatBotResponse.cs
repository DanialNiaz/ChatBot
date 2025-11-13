using Newtonsoft.Json;

namespace DialogFlowChatBot.DTOs
{
    public class ChatBotResponse
    {
        public string ResponseId { get; set; }
        public Result QueryResult { get; set; }
        public object WebhookStatus { get; set; }
        public List<object> OutputAudio { get; set; }
        public object OutputAudioConfig { get; set; }


        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ArivalCity
        {
            public int NullValue { get; set; }
            public bool HasNullValue { get; set; }
            public double NumberValue { get; set; }
            public bool HasNumberValue { get; set; }
            public string StringValue { get; set; }
            public bool HasStringValue { get; set; }
            public bool BoolValue { get; set; }
            public bool HasBoolValue { get; set; }
            public object StructValue { get; set; }
            public ListValue ListValue { get; set; }
            public int KindCase { get; set; }
        }

        public class ArivalCityOriginal
        {
            public int NullValue { get; set; }
            public bool HasNullValue { get; set; }
            public double NumberValue { get; set; }
            public bool HasNumberValue { get; set; }
            public string StringValue { get; set; }
            public bool HasStringValue { get; set; }
            public bool BoolValue { get; set; }
            public bool HasBoolValue { get; set; }
            public object StructValue { get; set; }
            public ListValue ListValue { get; set; }
            public double KindCase { get; set; }
        }

        public class ContextName
        {
            public int Type { get; set; }
            public object UnparsedResource { get; set; }
            public string ContextId { get; set; }
            public object EnvironmentId { get; set; }
            public object LocationId { get; set; }
            public string ProjectId { get; set; }
            public string SessionId { get; set; }
            public object UserId { get; set; }
            public bool IsKnownPattern { get; set; }
        }

        public class DepartureCity
        {
            public int NullValue { get; set; }
            public bool HasNullValue { get; set; }
            public double NumberValue { get; set; }
            public bool HasNumberValue { get; set; }
            public string StringValue { get; set; }
            public bool HasStringValue { get; set; }
            public bool BoolValue { get; set; }
            public bool HasBoolValue { get; set; }
            public object StructValue { get; set; }
            public ListValue ListValue { get; set; }
            public double KindCase { get; set; }
        }

        public class DepartureCityOriginal
        {
            public int NullValue { get; set; }
            public bool HasNullValue { get; set; }
            public double NumberValue { get; set; }
            public bool HasNumberValue { get; set; }
            public string StringValue { get; set; }
            public bool HasStringValue { get; set; }
            public bool BoolValue { get; set; }
            public bool HasBoolValue { get; set; }
            public object StructValue { get; set; }
            public ListValue ListValue { get; set; }
            public int KindCase { get; set; }
        }

        public class Fields
        {
            public ArivalCity ArivalCity { get; set; }
            public DepartureCity DepartureCity { get; set; }

        }

        public class FulfillmentMessage
        {
            public Text Text { get; set; }
            public object Image { get; set; }
            public object QuickReplies { get; set; }
            public object Card { get; set; }
            public object Payload { get; set; }
            public object SimpleResponses { get; set; }
            public object BasicCard { get; set; }
            public object Suggestions { get; set; }
            public object LinkOutSuggestion { get; set; }
            public object ListSelect { get; set; }
            public object CarouselSelect { get; set; }
            public object BrowseCarouselCard { get; set; }
            public object TableCard { get; set; }
            public object MediaContent { get; set; }
            public int Platform { get; set; }
            public int MessageCase { get; set; }
        }

        public class Intent
        {
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public int WebhookState { get; set; }
            public int Priority { get; set; }
            public bool IsFallback { get; set; }
            public bool MlDisabled { get; set; }
            public bool LiveAgentHandoff { get; set; }
            public bool EndInteraction { get; set; }
            public List<object> InputContextNames { get; set; }
            public List<object> Events { get; set; }
            public List<object> TrainingPhrases { get; set; }
            public string Action { get; set; }
            public List<object> OutputContexts { get; set; }
            public bool ResetContexts { get; set; }
            public List<object> Parameters { get; set; }
            public List<object> Messages { get; set; }
            public List<object> DefaultResponsePlatforms { get; set; }
            public string RootFollowupIntentName { get; set; }
            public string ParentFollowupIntentName { get; set; }
            public List<object> FollowupIntentInfo { get; set; }
            public IntentName IntentName { get; set; }
        }

        public class IntentName
        {
            public int Type { get; set; }
            public object UnparsedResource { get; set; }
            public string IntentId { get; set; }
            public object LocationId { get; set; }
            public string ProjectId { get; set; }
            public bool IsKnownPattern { get; set; }
        }

        public class ListValue
        {
            public List<Value> Values { get; set; }
        }

        public class OutputContext
        {
            public string Name { get; set; }
            public int LifespanCount { get; set; }
            public Parameters Parameters { get; set; }
            public ContextName ContextName { get; set; }
        }

        public class Parameters
        {
            public Fields Fields { get; set; }
        }

        public class Result
        {
            public string QueryText { get; set; }
            public string LanguageCode { get; set; }
            public double SpeechRecognitionConfidence { get; set; }
            public string Action { get; set; }
            public Parameters Parameters { get; set; }
            public bool AllRequiredParamsPresent { get; set; }
            public bool CancelsSlotFilling { get; set; }
            public string FulfillmentText { get; set; }
            public List<FulfillmentMessage> FulfillmentMessages { get; set; }
            public string WebhookSource { get; set; }
            public object WebhookPayload { get; set; }
            public List<OutputContext> OutputContexts { get; set; }
            public Intent Intent { get; set; }
            public double IntentDetectionConfidence { get; set; }
            public object DiagnosticInfo { get; set; }
            public object SentimentAnalysisResult { get; set; }
        }

        
        

        public class Text
        {
            public List<string> Text_ { get; set; }
        }

        public class Value
        {
            public int NullValue { get; set; }
            public bool HasNullValue { get; set; }
            public double NumberValue { get; set; }
            public bool HasNumberValue { get; set; }
            public string StringValue { get; set; }
            public bool HasStringValue { get; set; }
            public bool BoolValue { get; set; }
            public bool HasBoolValue { get; set; }
            public object StructValue { get; set; }
            public object ListValue { get; set; }
            public double KindCase { get; set; }
        }



    }
}
