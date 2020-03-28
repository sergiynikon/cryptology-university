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

        private static char EncryptChar(char message, int key)
        {
            // Using Caesar cipher don't encrypt non letter character
            if (!char.IsLetter(message))
            {
                return message;
            }

            char minChar = char.IsUpper(message) ? 'A' : 'a';
            return (char)((message + key - minChar) % AlphabetNumber + minChar);
        }

        private static char DecryptChar(char message, int key)
        {
            if (!char.IsLetter(message))
            {
                return message;
            }

            char minChar = char.IsUpper(message) ? 'A' : 'a';
            return (char)((AlphabetNumber + message - key - minChar) % AlphabetNumber + minChar);
        }
    }
}
