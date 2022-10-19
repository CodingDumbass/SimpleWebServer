using SimpleWebServer.Server.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebServer.Server.Responses
{
    public class UnauthorizedResponse:Response
    {
        public UnauthorizedResponse() : base(StatusCode.Unauthorized) { }
    }
}
