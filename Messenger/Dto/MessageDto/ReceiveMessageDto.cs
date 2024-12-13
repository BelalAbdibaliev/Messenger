namespace Messenger.Dto.MessageDto;

public class ReceiveMessageDto
{
    public string Content { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public bool IsRead { get; set; }
}