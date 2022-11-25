using LT.DigitalOffice.ReactionService.Models.Dto.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.ReactionService.Models.Dto.Requests;

public record CreateSingleReactionRequest
{
  [Required]
  public string Name { get; set; }
  public string Unicode { get; set; }
  public Guid ReactionsGroupId { get; set; }
  [Required]
  public ImageContent Image { get; set; }
}
