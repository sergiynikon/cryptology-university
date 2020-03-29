using Cryptology.Domain.Abstract;
using Cryptology.Domain.Ciphers;

namespace Cryptology.Domain
{
    public class CipherProvider
    {
        private ICipher _cipher;
        public string Key { get; set; }
        public string Message { get; set; }

        public CipherProvider()
        {
        }

        public CipherProvider(ICipher cipher)
        {
            _cipher = cipher;
        }

        public static CipherProvider CreateDefault()
        {
            return new CipherProvider(new DefaultCipher());
        }

        public static string DefaultCipherName => nameof(DefaultCipher);

        public void SetCipher(ICipher cipher)
        {
            _cipher = cipher;
        }

        public string Encrypt()
        {
            return _cipher.Encrypt(Message, Key);
        }

        public string Decrypt()
        {
            return _cipher.Decrypt(Message, Key);
        }

    }
}