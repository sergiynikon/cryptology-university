using Cryptology.Domain.Abstract;

namespace Cryptology.Domain.Ciphers
{
    public class DefaultCipher : ICipher
    {
        public string Encrypt(string message, string key)
        {
            return message;
        }

        public string Decrypt(string message, string key)
        {
            return message;
        }
    }
}