using FluentValidation;
using LT.DigitalOffice.ReactionService.Data.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Db;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup.Filters;
using LT.DigitalOffice.ReactionService.Validation.Image.Interfaces;
using LT.DigitalOffice.ReactionService.Validation.Reactions.Interfaces;
using System.Text.RegularExpressions;

namespace LT.DigitalOffice.ReactionService.Validation.Reactions;

public class CreateSingleReactionRequestValidator : AbstractValidator<CreateSingleReactionRequest>, ICreateSingleReactionRequestValidator
{
  private readonly Regex _nameRegex = new(@"^([a-zA-Zа-яА-ЯёЁ]+)$");

  public CreateSingleReactionRequestValidator(
    IReactionRepository reactionRepository,
    IReactionsGroupRepository reactionsGroupRepository,
    IImageValidator imageValidator)
  {
    RuleFor(r => r.Name)
      .MaximumLength(20)
      .WithMessage("Name is too long.")
      .Must(x => _nameRegex.IsMatch(x.Trim()))
      .WithMessage("Name contains invalid characters.")
      .MustAsync(async (x, _) => !await reactionRepository.DoesNameExist(x))
      .WithMessage("Reaction with this name already exists.");

    RuleFor(r => r.Unicode)
      .MinimumLength(7)
      .WithMessage("Unicode is too short.");

    RuleFor(r => r.ReactionsGroupId)
      .MustAsync(async (x, _) => await reactionsGroupRepository.DoesExistAsync(x))
      .WithMessage("This group does not exist.")
      .MustAsync(async (x, _) =>
      {
        DbReactionsGroup dbReactionsGroup = await reactionsGroupRepository.GetAsync(new GetReactionsGroupFilter
        {
          ReactionsGroupId = x
        });

        return dbReactionsGroup.Reactions.Count < 16;
      })
      .WithMessage("Maximum number of reactions in this group has been reached.");

    RuleFor(r => r.Image)
      .SetValidator(imageValidator);
  }
}
