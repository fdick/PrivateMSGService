namespace PrivateMSGService.DataAccess.Entities
{
    public class PrivateMessageEntity
    {
        public Guid ID { get; set; }
        public Guid ToUserID { get; set; }
        public string Message { get; set; }
        public DateTime SentTime { get; set; }

        public Guid FromUserID { get; set; }
        public UserEntity FromUser { get; set; }
    }
}
