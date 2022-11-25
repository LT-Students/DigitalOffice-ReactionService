﻿using LT.DigitalOffice.ReactionService.Models.Dto.Models;
using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.ReactionService.Models.Dto.Requests;

public record CreateReactionRequest
{
  [Required]
  public string Name { get; set; }
  public string Unicode { get; set; }
  [Required]
  public ImageContent Image { get; set; }
}
