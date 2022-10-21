namespace SimpleWebServer.Server.HTML
{
    public class HtmlBuilder
    {
        private string? htmlFile;
        public HtmlBuilder()
        {
            string line;
            using (var reader = new StreamReader(@"E:\C#\SimpleWebServer\SimpleWebServer.Server\Pages\Page1.html"))
                while ((line = reader.ReadLine()) != null)
                    htmlFile += line.Trim() + "\n";
        }
        public string GetFile() => htmlFile;
    }
}
