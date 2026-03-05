namespace PrivateMSGService.API.Contracts
{
    public record PrivateMessageResponse(Guid Id, Guid fromUserID, Guid toUserID, string message, DateTime sentTime);
}
