using Microsoft.Extensions.Configuration;

namespace SpyStore.MVC.Configuration
{
    public class WebServiceLocator : IWebServiceLocator
    {
        public string ServiceAddress { get; }

        public WebServiceLocator(IConfigurationRoot config)
        {
            var customSectcion = config.GetSection(nameof(WebServiceLocator));
            ServiceAddress = customSectcion?.GetSection(nameof(ServiceAddress))?.Value;
        }
    }
}
