using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.ReactionService.Models.Dto.Requests;

public record CreateReactionRequest
{
  [Required]
  public string Name { get; set; }
  public string Unicode { get; set; }
  [Required]
  public string Content { get; set; }
  [Required]
  public string Extension { get; set; }
}
