using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.ReactionService.Models.Dto.Models;

public record ImageContent
{
  public string Name { get; set; }
  [Required]
  public string Content { get; set; }
  [Required]
  public string Extension { get; set; }
}
