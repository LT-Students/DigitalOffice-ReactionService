using FluentValidation;
using LT.DigitalOffice.ReactionService.Data.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Db;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup.Filters;
using LT.DigitalOffice.ReactionService.Validation.Reactions.Interfaces;

namespace LT.DigitalOffice.ReactionService.Validation.Reactions;

public class CreateSingleReactionRequestValidator : AbstractValidator<CreateSingleReactionRequest>, ICreateSingleReactionRequestValidator
{
  public CreateSingleReactionRequestValidator(
    IReactionsGroupRepository reactionsGroupRepository,
    ICreateReactionRequestValidator createReactionRequestValidator)
  {
    RuleFor(r => new CreateReactionRequest
    {
      Name = r.Name,
      Unicode = r.Unicode,
      Content = r.Content,
      Extension = r.Extension
    })
      .SetValidator(createReactionRequestValidator);

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
  }
}
