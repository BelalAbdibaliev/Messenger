using Messenger.Dto.ContactDto;

namespace Messenger.Interfaces;

public interface IFindContactsService
{
    Task<ContactInfoDto> FindByUserNameAsync(string userName);
}