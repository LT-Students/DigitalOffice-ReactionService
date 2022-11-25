using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.ReactionService.Models.Dto.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Broker.Requests.Interfaces;

[AutoInject]
public interface IImageService
{
  Task<Guid?> CreateImageAsync(ImageContent image, List<string> errors);
}
