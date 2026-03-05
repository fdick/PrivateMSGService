namespace PrivateMSGService.API.Contracts
{
    public record PrivateMessageRequest(Guid fromUserID, Guid toUserID, string message);
}
