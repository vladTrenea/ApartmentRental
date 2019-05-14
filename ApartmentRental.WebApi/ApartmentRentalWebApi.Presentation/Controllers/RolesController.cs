using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ApartmentRentalWebApi.Business.Core.Services;
using ApartmentRentalWebApi.Presentation.Utils.Jwt;

namespace ApartmentRentalWebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = JwtConstants.AdminPolicy)]
	[ApiController]
	public class RolesController : ControllerBase
    {
	    private readonly IRoleService _roleService;

	    public RolesController(IRoleService roleService)
	    {
		    _roleService = roleService;
	    }

		[HttpGet]
	    public async Task<IActionResult> Get()
	    {
		    return Ok(await _roleService.GetRoles());
	    }
    }
}