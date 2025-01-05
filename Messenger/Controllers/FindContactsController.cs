using Messenger.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[Route("contacts/")]
[ApiController]
public class FindContactsController : ControllerBase
{
    private readonly IFindContactsService _findContactsService;

    public FindContactsController(IFindContactsService findContactsService)
    {
        _findContactsService = findContactsService;
    }

    [HttpGet("findcontact")]
    public async Task<IActionResult> GetContactAsync([FromQuery] string contactName)
    {
        if(contactName == null)
            return BadRequest();
        
        return Ok(await _findContactsService.FindByUserNameAsync(contactName));
    }
}