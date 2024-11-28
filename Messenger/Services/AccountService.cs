using Messenger.Data;
using Messenger.Dto;
using Messenger.Entities;
using Messenger.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Messenger.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TokenProvider _tokenProvider;

    public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenProvider tokenProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenProvider = tokenProvider;
    }
    
    public async Task<AccountResponse> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            throw new ApplicationException("Incorrect email or password");
        
        var result = _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

        var token = _tokenProvider.CreateToken(user);
        
        return new AccountResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.UserName,
            Token = token
        };
    }

    public async Task<AccountResponse> Register(RegisterDto registerDto)
    {
        var user = await _userManager.FindByEmailAsync(registerDto.Email);
        if(user != null)
            throw new ApplicationException("Email already registered");

        var newUser = new AppUser
        {
            Email = registerDto.Email,
            UserName = registerDto.UserName,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
        };
        
        var creationResult = await _userManager.CreateAsync(newUser, registerDto.Password);
        
        if(!creationResult.Succeeded)
            throw new ApplicationException("Failed to create user");
        
        await _userManager.AddToRoleAsync(newUser, UserRoles.User);
        var token = _tokenProvider.CreateToken(newUser);
        
        return new AccountResponse
        {
            Email = newUser.Email,
            UserName = registerDto.UserName,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Token = token
        };
    }
}