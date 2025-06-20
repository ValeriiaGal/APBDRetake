using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace RetakeAPBD;

[ApiController]
[Route("api/mobiles")]
public class OperatorsController(IOperatorService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GetPhoneNumberDTO>>> GetAllPhoneNumbers()
    {
        try
        {
            var test = await service.GetAllPhoneNumbersAsync();
            return Ok(test);
        }
        catch (ServerException e)
        {
            return Problem(e.Message); //code: 500
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreatePhoneNumber([FromBody] CreatePhoneNumberDTO dto)
    {
        try
        {
            var id = await service.CreatePhoneNumberAsync(dto);
            return Created(id.ToString(), id);
        }
        catch (ClientInputException e)
        {
            return BadRequest(e.Message);
        }
        catch (ServerException e)
        {
            return Problem(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}