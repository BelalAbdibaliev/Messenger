using Messenger.Dto;
using Messenger.Entities;
using Messenger.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Messenger.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<AccountResponse> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            throw new ApplicationException("Incorrect email or password");
        
        var result = _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
        
        await _signInManager.SignInAsync(user, false);
        
        return new AccountResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserName = user.UserName,
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
            UserName = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
        };
        
        var creationResult = await _userManager.CreateAsync(newUser, registerDto.Password);
        
        if(!creationResult.Succeeded)
            throw new ApplicationException("Failed to create user");
        
        await _userManager.AddToRoleAsync(newUser, "User");
        await _signInManager.SignInAsync(newUser, false);
        
        return new AccountResponse
        {
            Email = newUser.Email,
            UserName = registerDto.UserName,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
        };
    }
}