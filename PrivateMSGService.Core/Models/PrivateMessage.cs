namespace PrivateMSGService.Core.Models
{
    public class PrivateMessage
    {
        private PrivateMessage(Guid Id, Guid fromUserID, Guid toUserID, string message, DateTime sentTime)
        {
            FromUserID = fromUserID;
            ToUserID = toUserID;
            Message = message;
            this.ID = Id;
            SentTime = sentTime;
        }

        public Guid ID { get; }
        public Guid FromUserID { get; }
        public Guid ToUserID { get; }
        public string Message { get; }
        public DateTime SentTime { get; }

        public static (PrivateMessage, string) Create(Guid ID, Guid fromUserID, Guid toUserID, string message, DateTime sentTime)
        {
            string error = string.Empty;

            var msg = new PrivateMessage(ID, fromUserID, toUserID, message, sentTime);

            return (msg, error);
        }
    }
}
