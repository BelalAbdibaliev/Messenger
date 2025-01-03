﻿namespace Messenger.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public DateTime SendTime { get; set; }
    public bool IsRead { get; set; }
}