using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Business.Commands.ReactionsGroup.Interfaces;

[AutoInject]
public interface ICreateReactionsGroupCommand
{
  Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateReactionsGroupRequest request);
}
