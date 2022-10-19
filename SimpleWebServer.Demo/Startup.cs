using SimpleWebServer.Server;
using SimpleWebServer.Server.HTTP;
using SimpleWebServer.Server.Responses;

namespace SimpleWebServer.Demo
{
    public class Startup
    {
        static void Main(string[] args)
            => new HttpServer(routes => routes
            .MapGet("/", new TextResponse("Hello from the server!"))
            .MapGet("/HTML", new HtmlResponse("<h1>HTML response<h1>"))
            .MapGet("/Redirect", new RedirectResponse("https://softuni.bg/")))
            .Start();
    }
}