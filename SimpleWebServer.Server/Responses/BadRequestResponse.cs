using SimpleWebServer.Server.HTTP;

namespace SimpleWebServer.Server.Responses
{
    public class BadRequestResponse : Response
    {
        public BadRequestResponse() : base(StatusCode.BadRequest) { }
    }
}
