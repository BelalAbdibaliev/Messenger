﻿using Messenger.Entities;

namespace Messenger.Interfaces;

public interface IMessageRepository
{
    Task<bool> CreateMessage(Message message);
}