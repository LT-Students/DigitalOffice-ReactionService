using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.ReactionService.Business.Commands.Reaction.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Controllers;

[Route("[controller]")]
[ApiController]
public class ReactionController : ControllerBase
{
  [HttpPost("create")]
  public async Task<OperationResultResponse<Guid?>> CreateAsync(
    [FromServices] ICreateReactionCommand command,
    [FromBody] CreateReactionRequest request)
  {
    return await command.ExecuteAsync(request);
  }
}
