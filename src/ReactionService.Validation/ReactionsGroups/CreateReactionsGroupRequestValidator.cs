using FluentValidation;
using LT.DigitalOffice.ReactionService.Data.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup;
using LT.DigitalOffice.ReactionService.Validation.Reactions.Interfaces;
using LT.DigitalOffice.ReactionService.Validation.ReactionsGroup.Interfaces;

namespace LT.DigitalOffice.ReactionService.Validation.Reactions;

public class CreateReactionsGroupRequestValidator : AbstractValidator<CreateReactionsGroupRequest>, ICreateReactionsGroupRequestValidator
{
  public CreateReactionsGroupRequestValidator(
    IReactionsGroupRepository reactionsGroupRepository,
    ICreateReactionRequestValidator createReactionRequestValidator)
  {
    RuleFor(rg => rg.Name)
      .MaximumLength(30)
      .WithMessage("Name is too long.")
      .MustAsync(async (x, _) => !await reactionsGroupRepository.DoesNameExist(x))
      .WithMessage("Group with this name already exists.");

    RuleFor(rg => rg.Reactions)
      .NotEmpty()
      .WithMessage("Group must contain at least 1 reaction.")
      .Must(x => x.Count < 17)
      .WithMessage("Number of reactions should not exceed 16.");

    RuleForEach(r => r.Reactions)
      .SetValidator(createReactionRequestValidator);
  }
}
