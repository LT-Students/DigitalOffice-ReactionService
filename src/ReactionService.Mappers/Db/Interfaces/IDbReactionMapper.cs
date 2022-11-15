using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.ReactionService.Models.Db;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
using System;

namespace LT.DigitalOffice.ReactionService.Mappers.Db.Interfaces;

[AutoInject]
public interface IDbReactionMapper
{
  DbReaction Map(CreateReactionRequest request, Guid imageId);
}
