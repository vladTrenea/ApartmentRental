using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Services;
using ApartmentRentalWebApi.Business.Core.Settings;
using ApartmentRentalWebApi.Presentation.Utils.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace ApartmentRentalWebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
	    private readonly IAuthService _authService;

	    private readonly AuthenticationSettings _jwtSettings;

		public AuthController(IAuthService authService, AuthenticationSettings jwtSettings)
		{
			_authService = authService;
			_jwtSettings = jwtSettings;
		}

	    [HttpPost("login")]
	    public async Task<IActionResult> Login([FromBody] LoginDto model)
	    {
		    if (model == null)
		    {
			    return BadRequest();
		    }

		    var authModel = await _authService.Login(model);
		    authModel.Token = JwtUtils.CreateToken(authModel.UserId, (int)authModel.RoleId, _jwtSettings);

		    return Ok(authModel);
	    }

	    [HttpPost("register")]
	    public async Task<IActionResult> Register([FromBody] UserRegistrationDto model)
	    {
		    if (model == null)
		    {
			    return BadRequest();
		    }

		    await _authService.Register(model);

		    return Ok();
	    }

	    [HttpPost("activate/{token}")]
	    public async Task<IActionResult> Activate(string token)
	    {
		    if (string.IsNullOrEmpty(token))
		    {
			    return BadRequest();
		    }

		    await _authService.ConfirmAccount(token);

		    return Ok();
	    }

	    [HttpPost("password/reset")]
	    public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDto model)
	    {
		    if (model == null)
		    {
			    return BadRequest();
		    }

		    await _authService.ResetPassword(model);

		    return Ok();
	    }

	    [HttpPost("passwordTokenValidity/{token}")]
	    public async Task<IActionResult> PasswordTokenValidity(string token)
	    {
		    if (string.IsNullOrEmpty(token))
		    {
			    return BadRequest();
		    }

		    var passwordTokenValidity = await _authService.GetPasswordTokenValidity(token);

		    return Ok(passwordTokenValidity);
	    }

		[HttpPut("password/change/{token}")]
	    public async Task<IActionResult> ChangePassword(string token, [FromBody] PasswordChangeDto model)
	    {
		    if (model == null || string.IsNullOrEmpty(token))
		    {
			    return BadRequest();
		    }

		    await _authService.ChangePassword(token, model);

		    return Ok();
	    }

	    [HttpPut("account")]
		[Authorize]
	    public async Task<IActionResult> Account([FromBody] AccountDto model)
	    {
		    var userId = this.User.Claims.FirstOrDefault(c => c.Type == JwtConstants.UserIdClaim).Value;

		    await _authService.UpdateAccount(new Guid(userId), model);

		    return Ok();
	    }
	}
}