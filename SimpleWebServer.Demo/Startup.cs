using SimpleWebServer.Server;
using SimpleWebServer.Server.HTML;
using SimpleWebServer.Server.HTTP;
using SimpleWebServer.Server.Responses;

namespace SimpleWebServer.Demo
{
    public class Startup
    {
        private const string FileName = "content.txt";
        static async Task Main(string[] args)
        {
            await DownloadSitesAsTextFile(FileName, new string[] { "https://www.keybr.com/" });

            var server = new HttpServer(routes => routes
            .MapGet("/HTML", new HtmlResponse(new HtmlBuilder("/HTML").GetFile))
            .MapPost("/HTML", new TextResponse("", AddFormDataAction))
            .MapGet("/Redirect", new RedirectResponse("https://softuni.bg/"))
            .MapGet("/Content", new HtmlResponse(new HtmlBuilder("/Content").GetFile))
            .MapPost("/Content", new TextFileResponce(FileName)));

           await server.Start();
        }

        private static async Task<string> DownloadWebSiteContent(string url)
        {
            var httpClient = new HttpClient();
            using (httpClient)
            {
                var response = await httpClient.GetAsync(url);

                var html = await response.Content.ReadAsStringAsync();

                return html.Substring(0, 2000);
            }
        }

        private static async Task DownloadSitesAsTextFile(string fileName, string[] urls)
        {
            var downloads = new List<Task<string>>();

            foreach (var url in urls)
                downloads.Add(DownloadWebSiteContent(url));

            var responses = await Task.WhenAll(downloads);

            var responsesString = string.Join(Environment.NewLine + new String('-', 100), responses);

            await File.WriteAllTextAsync(fileName, responsesString);
        }
        public static void AddFormDataAction(Request request, Response response)
        {
            response.Body = "";
            foreach (var (key, value) in request.Form)
            {
                response.Body += $"{key} - {value}";
                response.Body += Environment.NewLine;
            }
        }
    }
}