using Infrastructure_Api.Models;
using Infrastructure_Api.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;


namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class SubscribersController( SubscriberService subscriberService) : ControllerBase
{
    private readonly SubscriberService _subscriberService = subscriberService;

    #region CreateSubsriber
    [HttpPost]
    public async Task<IActionResult> Create(SubscriberModel model)
    {
        if(ModelState.IsValid)
        {
            if(!await _subscriberService.IsEmailExist(model.Email))
            {
                if (await _subscriberService.CreateSubscriber(model)) 
                    return Created("", null); 
                else return Problem("Unable to create subscription");
            }
            else return Conflict("Email address already exist");
        }
        return BadRequest();
    }
    #endregion

    #region DeleteSubsriber
    [HttpDelete]
    public async Task<IActionResult> Delete(string email)
    {
        if (ModelState.IsValid)
        {

            if (await _subscriberService.IsEmailExist(email))
            {
                if (await _subscriberService.DeleteSubscriber(email))
                    return Ok();
                else return Problem("Unable to remove email adress");
            }
            else return NotFound();   
        }
        return BadRequest();
    }
    #endregion
}
