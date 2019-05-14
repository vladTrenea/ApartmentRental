using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Services;
using ApartmentRentalWebApi.Presentation.Utils.Jwt;

namespace ApartmentRentalWebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
    public class UsersController : ControllerBase
    {
	    private readonly IUserService _userService;

	    public UsersController(IUserService userService)
	    {
		    _userService = userService;
	    }

	    [HttpGet]
	    [Authorize(Policy = JwtConstants.AdminPolicy)]
		public async Task<IActionResult> Get()
	    {
		    return Ok(await _userService.GetUsers());
	    }

	    [HttpGet("{id}")]
	    [Authorize(Policy = JwtConstants.AdminPolicy)]
		public async Task<IActionResult> Get(Guid id)
	    {
		    return Ok(await _userService.GetById(id));
	    }

	    [HttpGet("realtors")]
	    [Authorize(Policy = JwtConstants.RealtorPolicy)]
		public async Task<IActionResult> GetRealtors()
	    {
		    return Ok(await _userService.GetRealtors());
	    }

	    [HttpPost]
	    [Authorize(Policy = JwtConstants.AdminPolicy)]
		public async Task<IActionResult> Post([FromBody] UserAddDto model)
	    {
		    await _userService.AddUser(model);

		    return Ok();
	    }

		[HttpPut("{id}")]
		[Authorize(Policy = JwtConstants.AdminPolicy)]
		public async Task<IActionResult> Put(Guid id, [FromBody] UserUpdateDto model)
	    {
		    if (model == null || Guid.Empty == id)
		    {
			    return BadRequest();
		    }

		    await _userService.UpdateUser(id, model);

			return Ok();
	    }

	    [HttpDelete("{id}")]
	    [Authorize(Policy = JwtConstants.AdminPolicy)]
		public async Task<IActionResult> Delete(Guid id)
	    {
		    if (Guid.Empty == id)
		    {
			    return BadRequest();
		    }

		    await _userService.DeleteUser(id);

		    return Ok();
	    }
    }
}