using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Business.Commands.Reaction.Interfaces;

[AutoInject]
public interface ICreateReactionCommand
{
  Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateSingleReactionRequest request);
}
