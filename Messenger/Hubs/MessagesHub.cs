using Messenger.Dto.MessageDto;
using Messenger.Entities;
using Messenger.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Hubs;

public class MessagesHub : Hub
{
    private readonly IMessageRepository _messageRepo;

    public MessagesHub(IMessageRepository messageRepo)
    {
        _messageRepo = messageRepo;
    }
    
    public async Task SendMessage(ReceiveMessageDto messageDto)
    {
        Message messageModel = new Message
        {
            Id = Guid.NewGuid(),
            SendTime = DateTime.Now,
            ReceiverId = messageDto.ReceiverId,
            SenderId = messageDto.SenderId,
            Content = messageDto.Content,
            IsRead = messageDto.IsRead,
        };
        
        var result = await _messageRepo.CreateMessage(messageModel);
        
        if(result)
            Console.WriteLine($"Successfully sent message: {messageDto.Content}");
        
        await Clients.All.SendAsync("ReceiveMessage", messageDto);
    }
}