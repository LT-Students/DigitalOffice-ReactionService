using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models.Image;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using LT.DigitalOffice.ReactionService.Broker.Requests.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
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

  public async Task<Guid?> CreateImageAsync(CreateReactionRequest request, List<string> errors)
  {
    CreateImageData imageData = new(request.Name, request.Content, request.Extension);

    object imageRequest = ICreateImagesRequest.CreateObj(
      new() { imageData },
      ImageSource.Reaction,
      _httpContextAccessor.HttpContext.GetUserId());

    return request is null
      ? null
      : (await _rcCreateImages.ProcessRequest<ICreateImagesRequest, ICreateImagesResponse>(
        imageRequest,
        errors,
        _logger))
          ?.ImagesIds?.FirstOrDefault();
  }
}
