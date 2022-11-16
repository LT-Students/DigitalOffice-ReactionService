using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.ReactionService.Models.Db;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Data.Interfaces;

[AutoInject]
public interface IReactionRepository
{
  Task<Guid?> CreateAsync(DbReaction dbReaction);

  Task<bool> DoesNameExist(string name);
}
