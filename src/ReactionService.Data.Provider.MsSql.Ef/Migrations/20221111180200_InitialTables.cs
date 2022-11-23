using LT.DigitalOffice.ReactionService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LT.DigitalOffice.ReactionService.Data.Provider.MsSql.Ef.Migrations;

[DbContext(typeof(ReactionServiceDbContext))]
[Migration("20221111180200_InitialTables")]
public class InitialTables : Migration
{
  protected override void Up(MigrationBuilder builder)
  {
    builder.CreateTable(
      name: DbReaction.TableName,
      columns: table => new
      {
        Id = table.Column<Guid>(nullable: false),
        Name = table.Column<string>(nullable: false),
        Unicode = table.Column<string>(nullable: true),
        ReactionsGroupId = table.Column<Guid>(nullable: false),
        ImageId = table.Column<Guid>(nullable: false),
        IsActive = table.Column<bool>(nullable: false),
        CreatedBy = table.Column<Guid>(nullable: false),
        CreatedAtUtc = table.Column<DateTime>(nullable: false),
        ModifiedBy = table.Column<Guid>(nullable: true),
        ModifiedAtUtc = table.Column<DateTime>(nullable: true)
      },
      constraints: table =>
      {
        table.PrimaryKey($"PK_{DbReaction.TableName}", x => x.Id);
      });

    builder.CreateTable(
      name: DbReactionsGroup.TableName,
      columns: table => new
      {
        Id = table.Column<Guid>(nullable: false),
        Name = table.Column<string>(nullable: false),
        IsActive = table.Column<bool>(nullable: false),
        CreatedBy = table.Column<Guid>(nullable: false),
        CreatedAtUtc = table.Column<DateTime>(nullable: false),
        ModifiedBy = table.Column<Guid>(nullable: true),
        ModifiedAtUtc = table.Column<DateTime>(nullable: true)
      },
      constraints: table =>
      {
        table.PrimaryKey($"PK_{DbReactionsGroup.TableName}", x => x.Id);
      });
  }
}
