namespace CulinaryGuide.Server.HelperClasses;

using System.Security.Cryptography;
using System.Text;

public static class Encrypter
{
    public static string CalculateSha256(string str)
    {
        SHA256 sha256 = SHA256.Create();
        byte[] hashValue;
        UTF8Encoding objUtf8 = new UTF8Encoding();
        hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));
        return objUtf8.GetString(hashValue);
    }
}