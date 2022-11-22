using DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using FluentValidation.Results;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.ReactionService.Broker.Requests.Interfaces;
using LT.DigitalOffice.ReactionService.Business.Commands.Reaction.Interfaces;
using LT.DigitalOffice.ReactionService.Data.Interfaces;
using LT.DigitalOffice.ReactionService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
using LT.DigitalOffice.ReactionService.Validation.Reactions.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Business.Commands.Reaction;

public class CreateReactionCommand : ICreateReactionCommand
{
  private readonly ICreateReactionRequestValidator _validator;
  private readonly IResponseCreator _responseCreator;
  private readonly IAccessValidator _accessValidator;
  private readonly IReactionRepository _reactionRepository;
  private readonly IImageService _imageService;
  private readonly IDbReactionMapper _mapper;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IImageCompressHelper _imageCompressHelper;
  private readonly IImageResizeHelper _imageResizeHelper;

  public CreateReactionCommand(
    ICreateReactionRequestValidator validator,
    IResponseCreator responseCreator,
    IAccessValidator accessValidator,
    IReactionRepository reactionRepository,
    IImageService imageService,
    IDbReactionMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IImageCompressHelper imageCompressHelper,
    IImageResizeHelper imageResizeHelper)
  {
    _validator = validator;
    _responseCreator = responseCreator;
    _accessValidator = accessValidator;
    _reactionRepository = reactionRepository;
    _imageService = imageService;
    _mapper = mapper;
    _httpContextAccessor = httpContextAccessor;
    _imageCompressHelper = imageCompressHelper;
    _imageResizeHelper = imageResizeHelper;
  }

  public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateReactionRequest request)
  {
    if (!(await _accessValidator.IsAdminAsync(_httpContextAccessor.HttpContext.GetUserId())))
    {
      return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
    }

    ValidationResult validationResult = await _validator.ValidateAsync(request);

    if (!validationResult.IsValid)
    {
      return _responseCreator.CreateFailureResponse<Guid?>(
        HttpStatusCode.BadRequest,
        validationResult.Errors.Select(x => x.ErrorMessage).ToList());
    }

    OperationResultResponse<Guid?> response = new();

    (bool isResizeSuccess, string imageContent, request.Extension) = await _imageResizeHelper.ResizeAsync(request.Content, request.Extension, 24);

    if (!isResizeSuccess)
    {
      response.Errors.Add("Resize operation have been failed");

      if (Convert.FromBase64String(request.Content).Length / 1000 > 10)
      {
        (bool isCompressSuccess, imageContent, request.Extension) = await _imageCompressHelper.CompressAsync(request.Content, request.Extension, 10);
      }
    }

    if ((imageContent is not null) && (Convert.FromBase64String(imageContent).Length / 1000 > 10))
    {
      (bool isCompressSuccess, imageContent, request.Extension) = await _imageCompressHelper.CompressAsync(imageContent, request.Extension, 10);

      if (!isCompressSuccess)
      {
        response.Errors.Add("Compress operation have been failed");
      }
    }

    if (imageContent is not null)
    {
      request.Content = imageContent;
    }

    Guid? imageId = await _imageService.CreateImageAsync(request, response.Errors);

    if (imageId is null)
    {
      return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
    }

    response.Body = await _reactionRepository.CreateAsync(_mapper.Map(request, imageId.Value));

    _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

    return response;
  }
}
