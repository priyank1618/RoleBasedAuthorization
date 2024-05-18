using Microsoft.AspNetCore.Mvc;
using RoleBasedAuthorization.Interface;
using RoleBasedAuthorization.Modals;
using RoleBasedAuthorization.ViewModal;
using System;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuth _auth;

    public AuthController(IAuth auth)
    {
        _auth = auth;
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public string Login([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var token = _auth.Login(loginRequest);
            return token;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // POST: api/auth/adduser
    [HttpPost("adduser")]
   
    public IActionResult AddUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdUser = _auth.AddUser(user);
        return Ok(createdUser);
    }


    // POST: api/auth/addroles
    [HttpPost("addroles")]
    public IActionResult AddRoles([FromBody] Role roles)
    {
        try
        {
            var createdRole = _auth.AddRoles(roles);
            return Ok(createdRole);
        }
        catch (Exception ex)
        {
            // Log the exception
            // _logger.LogError(ex, "Error adding roles");
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/auth/assignroles
    [HttpPost("assignroles")]
    public IActionResult AssignRoles([FromBody] AssignRoles roles)
    {
        try
        {
            var result = _auth.AssignRole(roles);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            // _logger.LogError(ex, "Error assigning roles");
            return StatusCode(500, "Internal server error");
        }
    }
}
