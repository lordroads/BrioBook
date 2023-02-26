using Brio.Confirm.Models;
using Brio.Confirm.Models.Request;
using Brio.Confirm.Models.Response;
using Brio.Confirm.Services;
using Microsoft.AspNetCore.Mvc;

namespace Brio.Confirm.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfirmController : Controller
{
    private readonly IConfirmService _confirmService;

    public ConfirmController(IConfirmService confirmService)
    {
        _confirmService = confirmService;
    }

    [HttpPost("create")]
    public ActionResult<CreateConfirmResponse> Create([FromForm] CreateConfirmRequest request) 
    {
        var confirmId = _confirmService.Create(request.UserId);

        return Ok(new CreateConfirmResponse
        {
            Succeeded = true,
            ConfirmId = confirmId.ToString()
        }); 
    }

    [HttpPost("set-confirm/{confirmId:guid}")]
    public ActionResult<SetConfirmResponse> SetConfirm([FromRoute]Guid confirmId)
    {
        return Ok(new SetConfirmResponse
        {
            Succeeded = _confirmService.SetConfirm(confirmId)
        });
    }

    [HttpGet("get-all")]
    public ActionResult<IList<BigData>> GetAll()
    {
        return Ok(_confirmService.GetAll());
    }
}