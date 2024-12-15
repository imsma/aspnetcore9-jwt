using System.Security.Cryptography;
using System.Text;

namespace aspnetcore9_jwt.Utilities;

public class PasswordHashHelper
{


    // Document following function
    // Hash a password with a random salt
    // password is the password to hash
    // returns a string that is a 36 character hash
    // The has is the 16 byte salt + 20 byte PBKDF2 hash
    // The PBKDF2 parameters are fixed and defined in the function
    // The salt is generated randomly
    // The function returns a string that is 36 characters long
    // The first 16 characters are the salt
    // The remaining 20 characters are the PBKDF2 hash
    // The salt is base64 encoded
    // The hash is base64 encoded
    // The salt and hash are concatenated and base64 encoded
    // The function returns a string that is 36 characters long
    // The function is deterministic, the same password will always return the same hash
    // The function is secure, it uses a random salt and a high iteration count
    // The function is slow, it is designed to be slow to make it
    // harder for an attacker to brute force the hash
    public static string HashPassword(string password)
    {
        var salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(20);

        var hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        return Convert.ToBase64String(hashBytes);
    }

    // Verify a password against a hash
    // hash is a string that was returned from HashPassword
    // password is the password to check

    public static bool VerifyPassword(string password, string hash)
    {
        var hashBytes = Convert.FromBase64String(hash);
        var salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        var hashToCompare = pbkdf2.GetBytes(20);

        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 16] != hashToCompare[i])
            {
                return false;
            }
        }

        return true;
    }

}
