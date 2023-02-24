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
    public ActionResult<CreateConfirmResponse> Create([FromBody] CreateConfirmRequest request) 
    {
        var confirmId = _confirmService.Create(request.UserId);

        return Ok(new CreateConfirmResponse
        {
            Succeeded = true,
            ConfirmId = confirmId
        }); 
    }

    [HttpPost("set-confirm")]
    public ActionResult<SetConfirmResponse> SetConfirm([FromBody] SetConfirmRequest request)
    {
        return new SetConfirmResponse
        {
            Succeeded = _confirmService.SetConfirm(request.ConfirmId)
        };
    }
}