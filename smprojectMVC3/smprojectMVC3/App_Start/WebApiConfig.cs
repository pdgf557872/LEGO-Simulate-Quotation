using smprojectMVC3.Models;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace smprojectMVC3
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務
            config.MapHttpAttributeRoutes();
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<ProjectUsedTable>("ProjectUsedTables");
            builder.EntitySet<LegoLibrary>("LegoLibraries");
            builder.EntitySet<SaveTable>("SaveTables");
            builder.EntitySet<MemberTable>("MemberTables");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            // Web API 路由

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}