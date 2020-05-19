namespace HttpProtocol.Contracts
{
    public interface IHttpClient
    {
        string Request(string path, string jsonData);
        string Request(string path, object request);
    }
}