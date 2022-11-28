using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.ReactionService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Db;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
using Microsoft.AspNetCore.Http;
using System;

namespace LT.DigitalOffice.ReactionService.Mappers.Db;

public class DbReactionMapper : IDbReactionMapper
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public DbReactionMapper(
    IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public DbReaction Map(CreateSingleReactionRequest request, Guid imageId)
  {
    return request is null
      ? null
      : new DbReaction
      {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Unicode = request.Unicode,
        ReactionsGroupId = request.ReactionsGroupId,
        ImageId = imageId,
        IsActive = true,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow
      };
  }

  public DbReaction Map(CreateReactionRequest request, Guid imageId)
  {
    return request is null
      ? null
      : new DbReaction
      {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Unicode = request.Unicode,
        ImageId = imageId,
        IsActive = true,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow
      };
  }
}
