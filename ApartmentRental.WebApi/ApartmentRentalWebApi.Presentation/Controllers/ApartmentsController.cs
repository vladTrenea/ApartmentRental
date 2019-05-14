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
    public class ApartmentsController : ControllerBase
    {
	    private readonly IApartmentService _apartmentService;

	    public ApartmentsController(IApartmentService apartmentService)
	    {
		    _apartmentService = apartmentService;
	    }

	    [Authorize(Policy = JwtConstants.RealtorPolicy)]
		[HttpGet]
		public async Task<IActionResult> Get([FromQuery]ApartmentFilterDto model)
	    {
		    if (model == null)
		    {
			    return BadRequest();
		    }

		    return Ok(await _apartmentService.GetFiltered(model));
	    }

		[Authorize]
		[HttpGet("rentable")]
		public async Task<IActionResult> GetRentable([FromQuery]ApartmentFilterDto model)
		{
			if (model == null)
			{
				return BadRequest();
			}

			return Ok(await _apartmentService.GetFilteredRentable(model));
		}

		[Authorize]
		[HttpGet("{id}")]
	    public async Task<IActionResult> Get(Guid id)
	    {
		    if (id == Guid.Empty)
		    {
			    return BadRequest();
		    }

		    return Ok(await _apartmentService.GetById(id));
	    }

	    [Authorize(Policy = JwtConstants.RealtorPolicy)]
	    [HttpPost]
	    public async Task<IActionResult> Post([FromBody] ApartmentAddUpdateDto model)
	    {
		    if (model == null)
		    {
			    return BadRequest();
		    }

		    await _apartmentService.Add(model);

		    return Ok();
	    }

	    [Authorize(Policy = JwtConstants.RealtorPolicy)]
	    [HttpPut("{id}")]
	    public async Task<IActionResult> Put(Guid id, [FromBody] ApartmentAddUpdateDto model)
	    {
		    if (id == Guid.Empty || model == null)
		    {
			    return BadRequest();
		    }

		    await _apartmentService.Update(id, model);

		    return Ok();
	    }

	    [Authorize(Policy = JwtConstants.RealtorPolicy)]
	    [HttpDelete("{id}")]
	    public async Task<IActionResult> Delete(Guid id)
	    {
		    if (id == Guid.Empty)
		    {
			    return BadRequest();
		    }

		    await _apartmentService.Delete(id);

		    return Ok();
	    }
    }
}