namespace PrivateMSGService.API.Contracts
{
    public record UserResponse(Guid id, string nickname, string name, string lastName, string email);
}
