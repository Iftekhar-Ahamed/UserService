using System.Security.Cryptography;
using System.Text;

namespace Application.Core.Helpers.EncryptionDecryptionHelper;

public static class OneWayEncryptionHelper
{
    private static string GenerateSalt()
    {
        var saltBytes = new byte[16];
        
        RandomNumberGenerator.Fill(saltBytes);
        
        return Convert.ToBase64String(saltBytes);
    }
    
    public static string EncryptPassword(string password, string? salt = null)
    {
        salt ??= GenerateSalt();
        using var sha256 = SHA256.Create();
        
        var combinedBytes = Encoding.UTF8.GetBytes(password + salt);
        var hashPassword = Convert.ToBase64String(sha256.ComputeHash(combinedBytes));
        
        string encryptedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(salt + hashPassword));
        
        return encryptedPassword;
    }
    
    public static bool IsValidPassword(string givenPassword, string password)
    {
        string decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(password));
        
        var salt = decodedPassword[..24];
        string encryptedPassword = EncryptPassword(givenPassword, salt);
        
        return encryptedPassword == password;
    }
}