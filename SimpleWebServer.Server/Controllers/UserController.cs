using SimpleWebServer.Server.HTTP;

namespace SimpleWebServer.Server.Controllers
{
    public class UserController : Controller
    {
        private const string Username = "user";
        private const string Password = "user";
        public UserController(Request request) : base(request)
        {

        }
        public Response Login() => View();

        public Response LogInUser()
        {
            Request.Session.Clear();

            var usernameMatches = Request.Form["Username"] == Username;
            var passwordMatches = Request.Form["Password"] == Password;

            if (usernameMatches && passwordMatches)
            {
                if (!this.Request.Session.ContainsKey(Session.SessionUserKey))
                {
                    Request.Session[Session.SessionUserKey] = "MyUserId";

                    var cookies = new CookieCollection();
                    cookies.Add(Session.SessionCookieName, Request.Session.Id);

                    return Html("<h1>Logged successfully!</h1>", cookies);
                }
                return Html("<h1>Logged successfully!</h1>");
            }
            return Login();
        }
        public Response Logout()
        {
            Request.Session.Clear();

            return Html("<h1>Logged out successfully!</h1>");
        }
        public Response GetUserData()
        {
            if (Request.Session.ContainsKey(Session.SessionUserKey))
                return Html($"<h1>Currently logged-in with username: {Username}</h1>");

            return Login();
        }
    }
}
