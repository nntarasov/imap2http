using System.Security.Cryptography;
using System.Text;
using HttpProtocol.Responses;
using ServiceStack;

namespace HttpProtocol
{
    public static class Extensions
    {
        public static bool IsOperationSuccessful(this string response)
        {
            if (string.IsNullOrWhiteSpace(response))
            {
                return false;
            }
            return response.FromJson<OperationResponse>()?.Success ?? false;
        }
        
        public static byte[] GetHash(this string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(this string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}