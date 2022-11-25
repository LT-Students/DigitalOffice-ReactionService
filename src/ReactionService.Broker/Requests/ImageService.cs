using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models.Image;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using LT.DigitalOffice.ReactionService.Broker.Requests.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Dto.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Broker.Requests;

public class ImageService : IImageService
{
  private readonly IRequestClient<ICreateImagesRequest> _rcCreateImages;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly ILogger<ImageService> _logger;

  public ImageService(
    IRequestClient<ICreateImagesRequest> rcCreateImages,
    IHttpContextAccessor httpContextAccessor,
    ILogger<ImageService> logger)
  {
    _rcCreateImages = rcCreateImages;
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
  }

  public async Task<Guid?> CreateImageAsync(ImageContent image, List<string> errors)
  {
    return image is null
      ? null
      : (await _rcCreateImages.ProcessRequest<ICreateImagesRequest, ICreateImagesResponse>(
        ICreateImagesRequest.CreateObj(
          new() { new CreateImageData(image.Name, image.Content, image.Extension) },
          ImageSource.Reaction,
          _httpContextAccessor.HttpContext.GetUserId()),
        errors,
        _logger))
        ?.ImagesIds?.FirstOrDefault();
  }
}
