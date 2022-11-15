using LT.DigitalOffice.ReactionService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.ReactionService.Data.Provider.MsSql.Ef.Migrations;

[DbContext(typeof(ReactionServiceDbContext))]
[Migration("20221114180200_EditColumn")]
public class EditColumn : Migration
{
  protected override void Up(MigrationBuilder builder)
  {
    builder.AlterColumn<string>(
      name: "Unicode",
      table: DbReaction.TableName,
      nullable: true);
  }
}
