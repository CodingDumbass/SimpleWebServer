namespace SimpleWebServer.Server.HTML
{
    public class HtmlBuilder
    {
        private Dictionary<string, List<string>> Files;
        public HtmlBuilder(string route)
        {
            Files = new Dictionary<string, List<string>>();

            Files.Add("/HTML", new List<string> {
                @"E:\C#\SimpleWebServer\SimpleWebServer.Server\Pages\HtmlForm.html"
            });
            Files.Add("/Content", new List<string> {
                @"E:\C#\SimpleWebServer\SimpleWebServer.Server\Pages\DownloadForm.html"
            });
            Files.Add("/Login", new List<string> {
                @"E:\C#\SimpleWebServer\SimpleWebServer.Server\Pages\LoginForm.html"
            });

            string line;
            using (var reader = new StreamReader(Files[route][0]))
                while ((line = reader.ReadLine()) != null)
                    GetFile += line.Trim() + "\n";
        }
        public string GetFile { get; private set; }

    }
}
