using backend.Account;
using backend.Services;
using backend.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(Response<AuthenticationResponse>), 200)]
    public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
    {
        return Ok(await _accountService.AuthenticateAsync(request, GenerateIPAddress()));
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(Response<string>), 200)]

    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        var origin = Request.Headers["origin"];
        return Ok(await _accountService.RegisterAsync(request, origin));
    }

    [HttpGet("confirm-email")]
    [ProducesResponseType(typeof(Response<string>), 200)]

    public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
    {
        var origin = Request.Headers["origin"];
        origin.AsQueryable();
        return Ok(await _accountService.ConfirmEmailAsync(userId, code));
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
    {
        await _accountService.ForgotPassword(model, Request.Headers["origin"]);
        return Ok();
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(Response<string>), 200)]

    public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
    {
        return Ok(await _accountService.ResetPassword(model));
    }

    private string GenerateIPAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}