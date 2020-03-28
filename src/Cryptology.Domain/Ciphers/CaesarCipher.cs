using System;
using System.Text;
using Cryptology.Domain.Abstract;

namespace Cryptology.Domain.Ciphers
{
    public class CaesarCipher : ICipher
    {
        const int AlphabetNumber = 26;

        public string Encrypt(string message, string key)
        {
            var intKey = ParseIntKey(key);

            var stringBuilder = new StringBuilder();
            foreach (var character in message)
            {
                stringBuilder.Append(EncryptChar(character, intKey));
            }

            return stringBuilder.ToString();
        }

        public string Decrypt(string message, string key)
        {
            var intKey = ParseIntKey(key);

            var stringBuilder = new StringBuilder();
            foreach (var character in message)
            {
                stringBuilder.Append(DecryptChar(character, intKey));
            }

            return stringBuilder.ToString();
        }

        private static int ParseIntKey(string key)
        {
            try
            {
                var res = int.Parse(key);
                return res;
            }
            catch (FormatException)
            {
                throw new FormatException("Cannot convert key to int value!");
            }
        }

        private static char EncryptChar(char source, int key)
        {
            // Using Caesar cipher don't encrypt non letter character
            if (!char.IsLetter(source))
            {
                return source;
            }

            char minChar = char.IsUpper(source) ? 'A' : 'a';
            return (char)((source + key - minChar) % AlphabetNumber + minChar);
        }

        private static char DecryptChar(char source, int key)
        {
            char minChar = char.IsUpper(source) ? 'A' : 'a';
            return (char)((AlphabetNumber + source - key - minChar) % AlphabetNumber + minChar);
        }
    }
}
