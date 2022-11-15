using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Data.Interfaces;

[AutoInject]
public interface IReactionsGroupRepository
{
  Task<bool> DoesExistAsync(Guid reactionsGroupId);
}
