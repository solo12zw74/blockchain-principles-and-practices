using System.Security.Cryptography;

namespace BlockWithSingleTransaction;

public class HashUtil
{
    public static byte[] ComputeHashSha256(byte[] toBeHashed)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(toBeHashed);
        }
    }
}