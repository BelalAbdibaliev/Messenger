using Messenger.Dto.MessageDto;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Hubs;

public class MessagesHub : Hub
{
    public async Task SendMessage(ReceiveMessageDto messageDto)
    {
        await Clients.All.SendAsync("ReceiveMessage", messageDto);
    }
}