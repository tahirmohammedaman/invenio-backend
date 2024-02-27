using System.Security.Cryptography;
using System.Text;

namespace invenio.Services;

public class AuthService
{
    static int saltSize = 16;
    static int iterations = 5000;
    
    static HashAlgorithmName algorithm = HashAlgorithmName.SHA512;
    
    public static string HashPassword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(saltSize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            algorithm,
            saltSize);
        return Convert.ToHexString(hash);
    }
    
    public static bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, algorithm, saltSize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }

    public static string GeneratePassword()
    {
        var choices = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()[]{}<>.,";
        return RandomNumberGenerator.GetString(choices, 10);
    }
}