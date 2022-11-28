using LT.DigitalOffice.ReactionService.Data.Interfaces;
using LT.DigitalOffice.ReactionService.Data.Provider;
using LT.DigitalOffice.ReactionService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Data;

public class ReactionRepository : IReactionRepository
{
  private readonly IDataProvider _provider;

  public ReactionRepository(
    IDataProvider provider)
  {
    _provider = provider;
  }

  public async Task<Guid?> CreateAsync(DbReaction dbReaction)
  {
    if (dbReaction is null)
    {
      return null;
    }

    _provider.Reactions.Add(dbReaction);
    await _provider.SaveAsync();

    return dbReaction.Id;
  }

  public Task<bool> DoesNameExist(string name)
  {
    return _provider.Reactions.AnyAsync(x => x.Name == name);
  }
}
