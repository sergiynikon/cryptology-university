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
                throw new FormatException($"Cannot convert key '{key}' to int value!");
            }
        }

        private static char EncryptChar(char character, int key)
        {
            // Using Caesar cipher don't encrypt non letter character
            if (!char.IsLetter(character))
            {
                return character;
            }

            char minChar = char.IsUpper(character) ? 'A' : 'a';
            return (char)((character + key - minChar) % AlphabetNumber + minChar);
        }

        private static char DecryptChar(char character, int key)
        {
            // Using Caesar cipher don't decrypt non letter character
            if (!char.IsLetter(character))
            {
                return character;
            }

            char minChar = char.IsUpper(character) ? 'A' : 'a';
            return (char)((AlphabetNumber + character - (key % AlphabetNumber) - minChar) % AlphabetNumber + minChar);
        }
    }
}
