﻿using FluentValidation;
using LT.DigitalOffice.ReactionService.Data.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
using LT.DigitalOffice.ReactionService.Validation.Image.Interfaces;
using LT.DigitalOffice.ReactionService.Validation.Reactions.Interfaces;
using System.Text.RegularExpressions;

namespace LT.DigitalOffice.ReactionService.Validation.Reactions;

public class CreateReactionRequestValidator : AbstractValidator<CreateReactionRequest>, ICreateReactionRequestValidator
{
  private readonly Regex _nameRegex = new(@"^([a-zA-Zа-яА-ЯёЁ]+)$");

  public CreateReactionRequestValidator(
    IReactionRepository reactionRepository,
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

    RuleFor(r => r.Image)
      .SetValidator(imageValidator);
  }
}
