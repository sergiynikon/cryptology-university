namespace Cryptology.Domain.Algorithms
{
    public class DefaultCipher : ICipher
    {
        public string Encrypt(string source, string key)
        {
            return source;
        }

        public string Decrypt(string source, string key)
        {
            return source;
        }
    }
}