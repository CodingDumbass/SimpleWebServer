using SimpleWebServer.Server;
using SimpleWebServer.Server.HTTP;
using SimpleWebServer.Server.Responses;
using SimpleWebServer.Server.HTML;

namespace SimpleWebServer.Demo
{
    public class Startup
    {
        static void Main(string[] args)
            => new HttpServer(routes => routes
            .MapGet("/HTML", new HtmlResponse(new HtmlBuilder().GetFile()))
            .MapGet("/Redirect", new RedirectResponse("https://softuni.bg/"))
            .MapPost("/HTML", new TextResponse("", AddFormDataAction)))
            .Start();

        public static void AddFormDataAction(Request request, Response response)
        {
            response.Body = "";
            foreach( var(key, value) in request.Form)
            {
                response.Body += $"{key} - {value}";
                response.Body += Environment.NewLine;
            }
        }
    }
}