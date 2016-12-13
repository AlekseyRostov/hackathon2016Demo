using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Swagger;
using Swashbuckle.Application;
using System.Web.Http;
using System.Web.Http.Description;


namespace Feedbacks.Azure
{
    /// <summary>
    /// Документация
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Регистрация сервиса для документации https://github.com/Azure/azure-mobile-apps-net-server/wiki/Adding-Swagger-Metadata-and-Help-UI-to-a-Mobile-App
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Use the custom ApiExplorer that applies constraints. This prevents
            // duplicate routes on /api and /tables from showing in the Swagger doc.
            config.Services.Replace(typeof(IApiExplorer), new MobileAppApiExplorer(config));
            config
               .EnableSwagger(c =>
               {
                   c.SingleApiVersion("v1", "Feedbacks.Azure");

                   // Tells the Swagger doc that any MobileAppController needs a
                   // ZUMO-API-VERSION header with default 2.0.0
                   c.OperationFilter<MobileAppHeaderFilter>();

                   // Looks at attributes on properties to decide whether they are readOnly.
                   // Right now, this only applies to the DatabaseGeneratedAttribute.
                   c.SchemaFilter<MobileAppSchemaFilter>();
               })
               .EnableSwaggerUi();
        }
    }
}
