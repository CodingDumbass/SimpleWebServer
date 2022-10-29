using SimpleWebServer.Server.HTTP;
using SimpleWebServer.Server.Routing;
using SimpleWebServer.Server.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebServer.Server.Controllers
{
    public static class RoutingTableExtensions
    {
        public static IRoutingTable MapGet<TController>(this IRoutingTable routingTable, string path, Func<TController, Response> controllerFuction)
            where TController : Controller
            => routingTable.MapGet(path, request => controllerFuction(CreateController<TController>(request)));

        public static IRoutingTable MapPost<TController>(this IRoutingTable routingTable, string path, Func<TController, Response> controllerFuction)
            where TController : Controller
            => routingTable.MapPost(path, request => controllerFuction(CreateController<TController>(request)));

        private static TController CreateController<TController>(Request request) where TController : Controller
            => (TController)Activator.CreateInstance(typeof(TController), new[] { request });

    }
}
