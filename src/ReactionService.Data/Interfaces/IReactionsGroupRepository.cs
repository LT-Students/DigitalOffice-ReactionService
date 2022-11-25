using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.ReactionService.Models.Db;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup.Filters;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Data.Interfaces;

[AutoInject]
public interface IReactionsGroupRepository
{
  Task<Guid?> CreateAsync(DbReactionsGroup dbReactionsGroup);

  Task<DbReactionsGroup> GetAsync(GetReactionsGroupFilter filter);

  Task<bool> DoesNameExist(string name);

  Task<bool> DoesExistAsync(Guid reactionsGroupId);
}
