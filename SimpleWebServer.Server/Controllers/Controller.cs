using SimpleWebServer.Server.HTTP;
using SimpleWebServer.Server.Responses;
using System.Runtime.CompilerServices;

namespace SimpleWebServer.Server.Controllers
{
    public abstract class Controller
    {
        public Request Request { get; private set; }
        protected Controller(Request request)
        {
            this.Request = request;
        }
        protected Response View([CallerMemberName] string viewName = "") => new ViewResponse(viewName, this.GetControllerName());
        protected Response View(object model, [CallerMemberName] string viewName = "") => new ViewResponse(viewName, this.GetControllerName(), model);
        private string GetControllerName() => this.GetType().Name.Replace(nameof(Controller), string.Empty);

        protected Response Text(string text) => new TextResponse(text);
        protected Response Html(string html, CookieCollection cookies = null)
        {
            var response = new HtmlResponse(html);

            if (cookies != null)
                foreach (var cookie in cookies)
                    response.Cookies.Add(cookie.Name, cookie.Value);

            return response;
        }
        protected Response BadRequest() => new BadRequestResponse();
        protected Response Unauthorized() => new UnauthorizedResponse();
        protected Response NotFound() => new NotFoundResponse();
        protected Response Redirect(string location) => new RedirectResponse(location);
        protected Response _File(string fileName) => new TextFileResponce(fileName);
    }
}
