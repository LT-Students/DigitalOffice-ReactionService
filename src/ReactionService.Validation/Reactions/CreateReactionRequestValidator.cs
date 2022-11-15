using FluentValidation;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using LT.DigitalOffice.ReactionService.Data.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
using LT.DigitalOffice.ReactionService.Validation.Reactions.Interfaces;
using System.Text.RegularExpressions;

namespace LT.DigitalOffice.ReactionService.Validation.Reactions;

public class CreateReactionRequestValidator : AbstractValidator<CreateReactionRequest>, ICreateReactionRequestValidator
{
  private readonly Regex nameRegex = new(@"^([a-zA-Zа-яА-ЯёЁ]+)$");

  public CreateReactionRequestValidator(
    IReactionRepository reactionRepository,
    IReactionsGroupRepository reactionsGroupRepository,
    IImageContentValidator imageContentValidator,
    IImageExtensionValidator imageExtensionValidator)
  {
    RuleFor(r => r.Name)
      .MaximumLength(20)
      .WithMessage("Name is too long.")
      .Must(x => nameRegex.IsMatch(x.Trim()))
      .WithMessage("Name contains invalid characters.")
      .MustAsync(async(x, _) => !await reactionRepository.DoesNameExist(x))
      .WithMessage("Reaction name already exist.");

    RuleFor(r => r.Unicode)
      .MinimumLength(7)
      .WithMessage("Unicode is too short.");

    RuleFor(r => r.ReactionsGroupId)
      .MustAsync(async (x, _) => await reactionsGroupRepository.DoesExistAsync(x))
      .WithMessage("Reaction group id does not exist.");

    RuleFor(r => r.Content)
      .SetValidator(imageContentValidator);

    RuleFor(r => r.Extension)
      .Must(x => ImageFormats.formats.Contains(x) && !x.Equals(".bmp") && !x.Equals(".tga"))
      .WithMessage("Wrong image extension.");
  }
}
