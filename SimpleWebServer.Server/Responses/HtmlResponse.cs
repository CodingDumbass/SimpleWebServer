using SimpleWebServer.Server.HTTP;

namespace SimpleWebServer.Server.Responses
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string text) : base(text, ContentType.Html) { }

    }
}
