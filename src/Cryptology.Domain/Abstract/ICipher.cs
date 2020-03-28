namespace Cryptology.Domain.Abstract
{
    public interface ICipher
    {
        string Encrypt(string source, string key);
        string Decrypt(string source, string key);
    }
}
