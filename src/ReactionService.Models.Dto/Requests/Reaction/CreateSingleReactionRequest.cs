using System;
using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.ReactionService.Models.Dto.Requests;

public record CreateSingleReactionRequest : CreateReactionRequest
{
  [Required]
  public Guid ReactionsGroupId { get; set; }
}
