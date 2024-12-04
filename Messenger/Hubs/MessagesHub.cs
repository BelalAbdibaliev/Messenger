using Messenger.Dto.MessageDto;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Hubs;

public class MessagesHub : Hub
{
    public async Task SendMessage(string messageContent)
    {
        Clients.All.SendAsync("ReceiveMessage", messageContent);
    }
}