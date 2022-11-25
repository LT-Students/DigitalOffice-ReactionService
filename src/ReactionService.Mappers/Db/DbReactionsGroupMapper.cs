using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.ReactionService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Db;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ReactionService.Mappers.Db;

public class DbReactionsGroupMapper : IDbReactionsGroupMapper
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public DbReactionsGroupMapper(
    IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public DbReactionsGroup Map(CreateReactionsGroupRequest request, List<DbReaction> dbReactions)
  {
    Guid reactionGroupId = Guid.NewGuid();

    dbReactions.ForEach(x => x.ReactionsGroupId = reactionGroupId);

    return request is null
      ? null
      : new DbReactionsGroup
      {
        Id = reactionGroupId,
        Name = request.Name,
        Reactions = dbReactions,
        IsActive = true,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow
      };
  }
}
