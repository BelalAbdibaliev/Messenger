using System.Runtime.InteropServices.JavaScript;
using Messenger.Dto;
using Messenger.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[Route("account/")]
[ApiController]
public class AccountController : ControllerBase
{
    private IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if(!ModelState.IsValid)
            return BadRequest();
        
        return Ok(await _accountService.Login(loginDto));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if(!ModelState.IsValid)
            return BadRequest();
        
        return Ok(await _accountService.Register(registerDto));
    }
}