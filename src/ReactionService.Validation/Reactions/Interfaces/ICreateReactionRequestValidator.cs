using FluentValidation;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;

namespace LT.DigitalOffice.ReactionService.Validation.Reactions.Interfaces;

[AutoInject]
public interface ICreateReactionRequestValidator : IValidator<CreateReactionRequest>
{
}
