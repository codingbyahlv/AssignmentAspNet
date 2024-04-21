using Infrastructure_Api.Models;
using Infrastructure_Api.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class ContactController(ContactService contactService) : ControllerBase
{
    private readonly ContactService _contactService = contactService;

    #region SendContactForm
    [HttpPost]
    public async Task<IActionResult> ContactForm(ContactModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _contactService.SendForm(model))
                return Created("", null);
            else return Problem("Unable to send contact form");

        }
        return BadRequest();
    }
    #endregion
}
