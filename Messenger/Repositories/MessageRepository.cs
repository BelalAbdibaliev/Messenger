using Messenger.Data;
using Messenger.Entities;
using Messenger.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories;

public class MessageRepository : IMessageRepository
{
    private IMessageRepository _messageRepositoryImplementation;
    private MessengerDbContext _dbContext;

    public MessageRepository(MessengerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CreateMessage(Message message)
    {
        await _dbContext.Messages.AddAsync(message);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteMessage(Guid messageId)
    {
        var message = await _dbContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId); 
        _dbContext.Messages.Remove(message);
        
        return await _dbContext.SaveChangesAsync() > 0;
    }
}