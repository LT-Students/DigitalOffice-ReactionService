using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LT.DigitalOffice.ReactionService.Models.Db;

public class DbReaction
{
  public const string TableName = "Reactions";

  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Unicode { get; set; }
  public Guid ReactionsGroupId { get; set; }
  public Guid ImageId { get; set; }
  public bool IsActive { get; set; }
  public Guid CreatedBy { get; set; }
  public DateTime CreatedAtUtc { get; set; }
  public Guid? ModifiedBy { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }

  public DbReactionsGroup ReactionsGroup { get; set; }
}

public class ReactionConfiguration : IEntityTypeConfiguration<DbReaction>
{
  public void Configure(EntityTypeBuilder<DbReaction> builder)
  {
    builder
      .ToTable(DbReaction.TableName);

    builder
      .HasKey(r => r.Id);

    builder
      .Property(r => r.Name)
      .IsRequired();

    builder
      .Property(r => r.Unicode)
      .IsRequired();

    builder
      .HasOne(r => r.ReactionsGroup)
      .WithMany(rg => rg.Reactions);
  }
}
