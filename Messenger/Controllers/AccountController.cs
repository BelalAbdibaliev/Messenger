using System.Runtime.InteropServices.JavaScript;
using Messenger.Dto;
using Messenger.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[Route("api/[controller]")]
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
        return Ok(await _accountService.Login(loginDto));
    }

    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        return Ok();
    }
}