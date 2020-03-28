using System.Reflection.Metadata.Ecma335;
using Cryptology.Domain.Algorithms;

namespace Cryptology.Domain
{
    public class Cipher
    {
        private ICipher _cipher;
        public string Key { get; set; }
        public string Message { get; set; }

        public Cipher()
        {
        }

        public Cipher(ICipher cipher)
        {
            _cipher = cipher;
        }

        public void SetCipher(ICipher cipher)
        {
            _cipher = cipher;
        }

        public string Encode()
        {
            return _cipher.Encrypt(Message, Key);
        }

        public string Decode()
        {
            return _cipher.Decrypt(Message, Key);
        }

    }
}