using DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using FluentValidation.Results;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.ReactionService.Broker.Requests.Interfaces;
using LT.DigitalOffice.ReactionService.Business.Commands.ReactionsGroup.Interfaces;
using LT.DigitalOffice.ReactionService.Data.Interfaces;
using LT.DigitalOffice.ReactionService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ReactionService.Models.Db;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests;
using LT.DigitalOffice.ReactionService.Models.Dto.Requests.ReactionsGroup;
using LT.DigitalOffice.ReactionService.Validation.ReactionsGroup.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ReactionService.Business.Commands.ReactionsGroup;

public class CreateReactionsGroupCommand : ICreateReactionsGroupCommand
{
  private readonly ICreateReactionsGroupRequestValidator _validator;
  private readonly IResponseCreator _responseCreator;
  private readonly IAccessValidator _accessValidator;
  private readonly IReactionsGroupRepository _reactionsGroupRepository;
  private readonly IImageService _imageService;
  private readonly IDbReactionsGroupMapper _reactionsGroupMapper;
  private readonly IDbReactionMapper _reactionMapper;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IImageCompressHelper _imageCompressHelper;
  private readonly IImageResizeHelper _imageResizeHelper;

  public CreateReactionsGroupCommand(
    ICreateReactionsGroupRequestValidator validator,
    IResponseCreator responseCreator,
    IAccessValidator accessValidator,
    IReactionsGroupRepository reactionsGroupRepository,
    IImageService imageService,
    IDbReactionsGroupMapper reactionsGroupMapper,
    IDbReactionMapper reactionMapper,
    IHttpContextAccessor httpContextAccessor,
    IImageCompressHelper imageCompressHelper,
    IImageResizeHelper imageResizeHelper)
  {
    _validator = validator;
    _responseCreator = responseCreator;
    _accessValidator = accessValidator;
    _reactionsGroupRepository = reactionsGroupRepository;
    _imageService = imageService;
    _reactionsGroupMapper = reactionsGroupMapper;
    _reactionMapper = reactionMapper;
    _httpContextAccessor = httpContextAccessor;
    _imageCompressHelper = imageCompressHelper;
    _imageResizeHelper = imageResizeHelper;
  }

  public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateReactionsGroupRequest request)
  {
    if (!await _accessValidator.IsAdminAsync(_httpContextAccessor.HttpContext.GetUserId()))
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

    List<DbReaction> dbReactions = new();

    foreach (CreateReactionRequest reaction in request.Reactions)
    {
      (bool isResizeSuccess, string imageContent, reaction.Image.Extension) = await _imageResizeHelper.ResizeAsync(reaction.Image.Content, reaction.Image.Extension, 24);

      if (!isResizeSuccess)
      {
        response.Errors.Add("Resize operation have been failed");

        if (Convert.FromBase64String(reaction.Image.Content).Length / 1000 > 10)
        {
          (bool isCompressSuccess, imageContent, reaction.Image.Extension) = await _imageCompressHelper.CompressAsync(reaction.Image.Content, reaction.Image.Extension, 10);
        }
      }

      if ((imageContent is not null) && (Convert.FromBase64String(imageContent).Length / 1000 > 10))
      {
        (bool isCompressSuccess, imageContent, reaction.Image.Extension) = await _imageCompressHelper.CompressAsync(imageContent, reaction.Image.Extension, 10);

        if (!isCompressSuccess)
        {
          response.Errors.Add("Compress operation have been failed");
        }
      }

      if (imageContent is not null)
      {
        reaction.Image.Content = imageContent;
      }

      reaction.Image.Name = reaction.Name;

      Guid? imageId = await _imageService.CreateImageAsync(reaction.Image, response.Errors);

      if (imageId is null)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      }

      dbReactions.Add(_reactionMapper.Map(reaction, imageId.Value));
    }

    response.Body = await _reactionsGroupRepository.CreateAsync(_reactionsGroupMapper.Map(request, dbReactions));

    if (response.Body is null)
    {
      return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
    }

    _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

    return response;
  }
}
