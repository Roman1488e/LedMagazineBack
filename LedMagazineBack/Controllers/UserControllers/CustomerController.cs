using LedMagazineBack.Constants;
using LedMagazineBack.Entities;
using LedMagazineBack.Models;
using LedMagazineBack.Models.ProductModels.UpdateModels;
using LedMagazineBack.Models.UserModels.Auth;
using LedMagazineBack.Models.UserModels.UpdateModels;
using LedMagazineBack.Services.UserServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LedMagazineBack.Controllers.UserControllers;

[ApiController]
public class CustomerController(ICustomerService customerService, IGuestService guestService) : Controller
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IGuestService _guestService = guestService;

    [HttpGet("api/customers")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllUsers();
        return Ok(customers);
    }
    
    [HttpGet("api/customers/by-id/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var customer = await _customerService.GetById(id);
            return Ok(customer);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("api/customers/by-username/{username}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        try
        {
            var customer = await _customerService.GetByUsername(username);
            return Ok(customer);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("api/customers/register")]
    [Authorize(Roles = "guest")]
    public async Task<IActionResult> Register([FromBody] RegisterModel customer)
    {
        try
        {
           var result= await _customerService.Register(customer);
           return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("api/customers/change-password")]
    [Authorize(Roles = "admin, customer")]
    public async Task<IActionResult> ChangePassword([FromBody]UpdatePasswordModel model)
    {
        try
        {
            var result = await _customerService.UpdatePassword(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("api/customers/change-number")]
    [Authorize(Roles = "admin, customer")]
    public async Task<IActionResult> ChangeNumber(UpdateContactNumberModel model)
    {
        try
        {
            var result = await _customerService.UpdateContactNumber(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("api/customers/{id}")]
    [Authorize(Roles = "admin, customer")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        try
        {
            var result = await _customerService.Delete(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPut("api/customers/change-name")]
    [Authorize(Roles = "admin, customer")]
    public async Task<IActionResult> ChangeName(UpdateClientGenInfModel model)
    {
        try
        {
            var result = await _customerService.UpdateGenInfo(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("api/customers/change-username")]
    [Authorize(Roles = "admin, customer")]
    public async Task<IActionResult> ChangeUsername([FromBody] UpdateUsernameModel model)
    {
        try
        {
            var result = await _customerService.UpdateUsername(model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("api/customers/change-role")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> ChangeRole(Guid id, [FromBody]UpdateRoleModel model)
    {
        try
        {
            var result = await _customerService.UpdateRole(id, model);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("api/customers/login")]
    [Authorize(Roles = "guest")]
    public async Task<IActionResult> Login([FromBody] LoginModel customer)
    {
        try
        {
            var token = await _customerService.Login(customer);
            await _guestService.DeleteBySessionId();
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("api/customers/verify")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Verify(Guid id)
    {
        try
        {
            var result = await _customerService.ChangeVerify(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("api/admins")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetAllAdmins()
    {
        var customers = await _customerService.GetAllAdmins();
        return Ok(customers);
    }
    
    
}