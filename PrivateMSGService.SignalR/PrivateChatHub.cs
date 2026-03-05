using Microsoft.AspNetCore.SignalR;
using PrivateMSGService.Core.Abstractions;
using PrivateMSGService.Core.Models;

namespace PrivateMSGService.SignalR
{
    public class PrivateChatHub : Hub
    {
        private readonly IPrivateMessagesService _messagesService;

        public PrivateChatHub(IPrivateMessagesService messagesService)
        {
            this._messagesService = messagesService;
        }

        public async Task SendMessage(string toUserId, string message)
        {
            var fromUserGuid = Guid.Parse(Context.UserIdentifier);
            var toUserGuid = Guid.Parse(toUserId);

            var fromUser = await _messagesService.GetOneAsync(fromUserGuid);

            if (!string.IsNullOrEmpty(fromUser.Item2))
            {

                return;
            }

            var (msg, error) = PrivateMessage.Create(
                Guid.NewGuid(),
                fromUserGuid,
                toUserGuid,
                message,
                DateTime.UtcNow
                );

            await _messagesService.CreatePrivateMsgAsync(msg);

            // Используем UserId - сообщение придет на ВСЕ устройства пользователя
            await Clients.User(toUserId).SendAsync("SignalRReceiveMessage", message);
        }



    }
}
