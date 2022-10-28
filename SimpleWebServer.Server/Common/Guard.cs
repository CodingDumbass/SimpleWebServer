namespace SimpleWebServer.Server.Common
{
    public class Guard
    {
        public static void AgainstNull(object value, string name = null)
        {
            if (value == null)
            {
                name ??= "value";

                throw new ArgumentException($"{name} cannot be null.");
            }
        }
    }
}
