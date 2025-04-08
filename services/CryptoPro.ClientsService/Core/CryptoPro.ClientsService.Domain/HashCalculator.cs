using System.Security.Cryptography;
using System.Text;

namespace CryptoPro.ClientsService.Domain;

public static class HashCalculator
{
    public static string CalculateHmacSha256Hash(string text, string salt)
    {
        var encoding = Encoding.Default;

        var baText2BeHashed = encoding.GetBytes(text);
        var baSalt = encoding.GetBytes(salt);
        using var hasher = new HMACSHA256(baSalt);
        var baHashedText = hasher.ComputeHash(baText2BeHashed);

        var result = string.Join("", baHashedText.ToList()
            .Select(b => b.ToString("x2")).ToArray());

        return result;
    }
    
    public static bool CheckHash(string password, string salt, string hashedPassword)
    {
        return CalculateHmacSha256Hash(password, salt) == hashedPassword;
    }
}