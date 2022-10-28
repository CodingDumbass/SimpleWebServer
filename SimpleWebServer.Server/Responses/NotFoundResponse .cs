using SimpleWebServer.Server.HTTP;

namespace SimpleWebServer.Server.Responses
{
    public class NotFoundResponse : Response
    {
        public NotFoundResponse() : base(StatusCode.NotFound) { }
    }
}
