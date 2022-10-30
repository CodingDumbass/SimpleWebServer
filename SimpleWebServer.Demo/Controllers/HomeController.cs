using SimpleWebServer.Server.Controllers;
using SimpleWebServer.Server.HTTP;
using SimpleWebServer.Server.Models;
using System.Text;
using System.Web;

namespace SimpleWebServer.Demo.Controllers
{
    public class HomeController : Controller
    {
        private const string FileName = @"E:\C#\SimpleWebServer\SimpleWebServer.Demo\bin\Debug\net6.0\content.txt";
        public HomeController(Request request) : base(request)
        {

        }
        public Response HtmlFormPost()
        {
            var name = this.Request.Form["Name"];
            var age = this.Request.Form["Age"];

            var model = new FormViewModel() 
            {
                Name = name,
                Age = int.Parse(age)
            };

            return View(model);
        }
        public Response Index() => Text("Hello from the server!");

        public Response Content() => View();

        public Response Cookies()
        {

            if (Request.Cookies.Any(c => c.Name != Session.SessionCookieName))
            {
                var cookieText = new StringBuilder();
                cookieText.AppendLine("<h1>Cookies</h1>");

                cookieText.Append("<table border='1'><tr><th>Name</th><th>Value</th></tr>");

                foreach (var cookie in this.Request.Cookies)
                {
                    cookieText.Append("<tr>");
                    cookieText.Append($"<td>{HttpUtility.HtmlEncode(cookie.Name)}</td>");
                    cookieText.Append($"<td>{HttpUtility.HtmlEncode(cookie.Value)}</td>");
                    cookieText.Append("</tr>");
                }
                cookieText.Append("</table>");
                return Html(cookieText.ToString());
            }
            var cookies = new CookieCollection();

            cookies.Add("My-Cookie", "My-Cookie");
            cookies.Add("My-Second-Cookie", "My-Second-Cookie");

            return Html("<h1>Cookies set!</h1>", cookies);
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
        public Response DownloadContent()
        {
            DownloadSitesAsTextFile(FileName, new string[] { "Sex On The Streets", "Kill All Gays" }).Wait();

            return _File(FileName);
        }

        public Response Html() => View();

        public Response Redirect() => Redirect("https://www.youtube.com/");

        public Response _Session()
        {
            string currentDateKey = "CurrentDate";

            var sessionExists = Request.Session.ContainsKey(currentDateKey);

            if (sessionExists)
            {
                var currentDate = Request.Session[currentDateKey];
                return Text($"Stored date: {currentDate}!");
            }

            return Text("Current date stored!");

        }
    }
}
