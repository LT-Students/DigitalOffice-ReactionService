using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup;

public record CreateReactionsGroupRequest
{
  [Required]
  public string Name { get; set; }
  [Required]
  public List<CreateReactionRequest> Reactions { get; set; }
}
