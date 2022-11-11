using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ReactionService.Models.Db;

public class DbReactionsGroup
{
  public const string TableName = "ReactionsGroups";

  public Guid Id { get; set; }
  public string Name { get; set; }
  public bool IsActive { get; set; }
  public Guid CreatedBy { get; set; }
  public DateTime CreatedAtUtc { get; set; }
  public Guid? ModifiedBy { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }

  public ICollection<DbReaction> Reactions { get; set; }

  public DbReactionsGroup()
  {
    Reactions = new HashSet<DbReaction>();
  }
}

public class ReactionsGroupConfiguration : IEntityTypeConfiguration<DbReactionsGroup>
{
  public void Configure(EntityTypeBuilder<DbReactionsGroup> builder)
  {
    builder
      .ToTable(DbReactionsGroup.TableName);

    builder
      .HasKey(rg => rg.Id);

    builder
      .Property(rg => rg.Name)
      .IsRequired();

    builder
      .HasMany(rg => rg.Reactions)
      .WithOne(r => r.ReactionsGroup);
  }
}
