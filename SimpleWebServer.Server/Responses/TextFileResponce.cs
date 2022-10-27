using SimpleWebServer.Server.HTTP;
namespace SimpleWebServer.Server.Responses
{
    public class TextFileResponce : Response
    {
        public string FileName { get; init; }

        public TextFileResponce(string filename) : base(StatusCode.OK)
        {
            this.FileName = filename;

            this.Headers.Add(Header.ContentType, ContentType.PlainText);

        }
        public override string ToString()
        {
            if (File.Exists(this.FileName))
            {
                this.Body = File.ReadAllTextAsync(this.FileName).Result;

                var fileBytesCount = new FileInfo(this.FileName).Length;

                this.Headers.Add(Header.ContentLength, fileBytesCount.ToString());

                this.Headers.Add(Header.ContentDisposition, $"attachment; filename ="+FileName+";");
            }
            return base.ToString();
        }
    }
}
