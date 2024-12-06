using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers.EncryptionDecryptionHelper;

public static class OneWayEncryptionHelper
{
    private static string GenerateSalt()
    {
        var saltBytes = new byte[16];
        
        RandomNumberGenerator.Fill(saltBytes);
        
        return Convert.ToBase64String(saltBytes);
    }
    
    public static string HashPassword(string password)
    {
        var salt = GenerateSalt();
        using var sha256 = SHA256.Create();
        
        var combinedBytes = Encoding.UTF8.GetBytes(password + salt);
        var hashBytes = sha256.ComputeHash(combinedBytes);
        
        return Convert.ToBase64String(hashBytes);
    }
}