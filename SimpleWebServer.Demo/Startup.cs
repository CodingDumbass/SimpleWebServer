using SimpleWebServer.Server;
using SimpleWebServer.Server.HTML;
using SimpleWebServer.Server.HTTP;
using SimpleWebServer.Server.Responses;
using System.Text;
using System.Web;

namespace SimpleWebServer.Demo
{
    public class Startup
    {
        private const string FileName = "content.txt";
        static async Task Main(string[] args)
        {
            await DownloadSitesAsTextFile(FileName, new string[] { "https://www.keybr.com/" });

            var server = new HttpServer(routes => routes
            .MapGet("/", new TextResponse("Hello from the server!"))
            .MapGet("/HTML", new HtmlResponse(new HtmlBuilder("/HTML").GetFile))
            .MapPost("/HTML", new TextResponse("", AddFormDataAction))
            .MapGet("/Redirect", new RedirectResponse("https://softuni.bg/"))
            .MapGet("/Content", new HtmlResponse(new HtmlBuilder("/Content").GetFile))
            .MapPost("/Content", new TextFileResponce(FileName))
            .MapGet("/Cookies", new HtmlResponse("", AddCookiesAction))
            .MapGet("/Session", new TextResponse("", DisplaySessionInfoAction)));

           await server.Start();
        }
        private static void DisplaySessionInfoAction(Request request, Response response)
        {
            var sessionExists = request.Session.ContainsKey(Session.SessionCurrentDateKey);

            var bodyText = "";

            if (sessionExists)
            {
                var currentDate = request.Session[Session.SessionCurrentDateKey];
                bodyText = $"Stored date: {currentDate}!";
            }
            else
                bodyText = "Current date stored!";

            response.Body = "";
            response.Body += bodyText;

        }
        public static void AddCookiesAction(Request request, Response response)
        {
            var requestHasCookies = request.Cookies.Any(c => c.Name != Session.SessionCookieName);
            response.Body = "";

            if (requestHasCookies)
            {
                var cookieText = new StringBuilder();
                cookieText.AppendLine("<h1>Cookies</h1>");

                cookieText.Append("<table border='1'><tr><th>Name</th><th>Value</th></tr>");

                foreach (var cookie in request.Cookies)
                {
                    cookieText.Append("<tr>");
                    cookieText.Append($"<td>{HttpUtility.HtmlEncode(cookie.Name)}</td>");
                    cookieText.Append($"<td>{HttpUtility.HtmlEncode(cookie.Value)}</td>");
                    cookieText.Append("</tr>");
                }
                cookieText.Append("</table>");

                response.Body = cookieText.ToString();
            }
            else
                response.Body = "<h1>Cookies set!</h1>";

            if (!requestHasCookies)
            {
                response.Cookies.Add("My-Cookie", "My-Value");
                response.Cookies.Add("My-Second-Cookie", "My-Second-Value");
            }
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