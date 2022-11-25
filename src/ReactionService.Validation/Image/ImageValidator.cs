using FluentValidation;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Dto.Models;
using LT.DigitalOffice.ReactionService.Validation.Image.Interfaces;
using System.Collections.Immutable;

namespace LT.DigitalOffice.ReactionService.Validation.Image;

public class ImageValidator : AbstractValidator<ImageContent>, IImageValidator
{
  public ImageValidator(
    IImageContentValidator imageContentValidator)
  {
    RuleFor(r => r.Content)
      .SetValidator(imageContentValidator);

    RuleFor(r => r.Extension)
      .Must(x => ImmutableList.Create(
          ImageFormats.jpg,
          ImageFormats.jpeg,
          ImageFormats.png,
          ImageFormats.svg,
          ImageFormats.gif,
          ImageFormats.webp)
        .Contains(x))
      .WithMessage("Wrong image extension.");
  }
}
