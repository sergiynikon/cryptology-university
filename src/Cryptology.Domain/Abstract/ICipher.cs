namespace Cryptology.Domain.Abstract
{
    public interface ICipher
    {
        string Encrypt(string message, string key);
        string Decrypt(string message, string key);
    }
}
