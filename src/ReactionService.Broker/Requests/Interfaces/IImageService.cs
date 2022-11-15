using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Broker.Requests.Interfaces;

[AutoInject]
public interface IImageService
{
  Task<Guid?> CreateImageAsync(CreateReactionRequest request, List<string> errors);
}
