using SimpleWebServer.Demo.Controllers;
using SimpleWebServer.Server;
using SimpleWebServer.Server.Controllers;

namespace SimpleWebServer.Demo
{
    public class Startup
    {
        static async Task Main(string[] args)
        {
            var server = new HttpServer(routes => routes
            .MapGet<HomeController>("/", x => x.Index())
            .MapGet<HomeController>("/Redirect", x => x.Redirect())
            .MapGet<HomeController>("/HTML", x => x.Html())
            .MapPost<HomeController>("/HTML", x => x.HtmlFormPost())
            .MapGet<HomeController>("/Content", x => x.Content())
            .MapPost<HomeController>("/Content", x => x.DownloadContent())
            .MapGet<HomeController>("/Cookies", x => x.Cookies())
            .MapGet<HomeController>("/Session", x => x._Session())
            .MapGet<UserController>("/Login", x => x.Login())
            .MapPost<UserController>("/Login", x => x.LogInUser())
            .MapGet<UserController>("/Logout", x => x.Logout())
            .MapGet<UserController>("/UserProfile", x => x.GetUserData()));

            await server.Start();
        }
    }
}