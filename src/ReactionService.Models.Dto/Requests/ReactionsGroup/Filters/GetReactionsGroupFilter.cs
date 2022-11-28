using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup.Filters;

public record GetReactionsGroupFilter
{
  [FromQuery(Name = "reactionsgroupid")]
  public Guid ReactionsGroupId { get; set; }

  [FromQuery(Name = "includereactions")]
  public bool IncludeReactions { get; set; } = true;

  [FromQuery(Name = "includedeactivated")]
  public bool IncludeDeactivated { get; set; } = false;
}
