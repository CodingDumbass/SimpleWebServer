using SimpleWebServer.Server.HTTP;

namespace SimpleWebServer.Server.Responses
{
    public class UnauthorizedResponse : Response
    {
        public UnauthorizedResponse() : base(StatusCode.Unauthorized) { }
    }
}
