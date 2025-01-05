using Messenger.Dto.ContactDto;
using Messenger.Entities;
using Messenger.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Messenger.Services;

public class FindContactsService : IFindContactsService
{
    private readonly UserManager<AppUser> _userManager;

    public FindContactsService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }


    public async Task<ContactInfoDto> FindByUserNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        
        if (user == null)
            throw new ArgumentException("User not found");

        return new ContactInfoDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = userName
        };
    }
}