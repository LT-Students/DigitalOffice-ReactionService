using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.ReactionService.Models.Db;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup;
using System.Collections.Generic;

namespace LT.DigitalOffice.ReactionService.Mappers.Db.Interfaces;

[AutoInject]
public interface IDbReactionsGroupMapper
{
  DbReactionsGroup Map(CreateReactionsGroupRequest request, List<DbReaction> dbReactions);
}
