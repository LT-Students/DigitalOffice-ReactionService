using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Broker.Requests.Interfaces;

[AutoInject]
public interface IImageService
{
  Task<Guid?> CreateImageAsync(string name, string content, string extension, List<string> errors);
}
