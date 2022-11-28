using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.ReactionService.Business.Commands.ReactionsGroup.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Controllers;

[Route("[controller]")]
[ApiController]
public class ReactionsGroupController : ControllerBase
{
  [HttpPost("create")]
  public async Task<OperationResultResponse<Guid?>> CreateAsync(
    [FromServices] ICreateReactionsGroupCommand command,
    [FromBody] CreateReactionsGroupRequest request)
  {
    return await command.ExecuteAsync(request);
  }
}
