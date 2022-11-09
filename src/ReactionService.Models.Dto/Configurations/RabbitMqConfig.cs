using LT.DigitalOffice.Kernel.BrokerSupport.Attributes;
using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;
using LT.DigitalOffice.Models.Broker.Requests.Image;

namespace LT.DigitalOffice.ReactionService.Models.Dto.Configurations;

public class RabbitMqConfig : BaseRabbitMqConfig
{
  // Image
  [AutoInjectRequest(typeof(IGetImagesRequest))]
  public string GetImagesEndpoint { get; set; }

  [AutoInjectRequest(typeof(ICreateImagesRequest))]
  public string CreateImagesEndpoint { get; set; }
}
