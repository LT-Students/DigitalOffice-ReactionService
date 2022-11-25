using FluentValidation;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup;

namespace LT.DigitalOffice.ReactionService.Validation.ReactionsGroup.Interfaces;

[AutoInject]
public interface ICreateReactionsGroupRequestValidator : IValidator<CreateReactionsGroupRequest>
{
}
