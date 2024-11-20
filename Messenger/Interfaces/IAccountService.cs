using Messenger.Dto;
using Messenger.Entities;

namespace Messenger.Interfaces;

public interface IAccountService
{
    Task<AccountResponse> Login(LoginDto loginDto);
    Task<AccountResponse> Register(RegisterDto registerDto);
}