using FluentValidation;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.ReactionService.Models.Dto.Models;

namespace LT.DigitalOffice.ReactionService.Validation.Image.Interfaces;

[AutoInject]
public interface IImageValidator : IValidator<ImageContent>
{
}
