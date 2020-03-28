using System;
using System.Text;

namespace Cryptology.Domain.Algorithms
{
    public class CaesarCipher : ICipher
    {
        const int AlphabetNumber = 26;

        public string Encrypt(string source, string key)
        {
            var intKey = ParseIntKey(key);

            var stringBuilder = new StringBuilder();
            foreach (var character in source)
            {
                stringBuilder.Append(EncodeChar(character, intKey));
            } 
            
            return stringBuilder.ToString();
        }

        public string Decrypt(string source, string key)
        {
            var intKey = ParseIntKey(key);

            var stringBuilder = new StringBuilder();
            foreach (var character in source)
            {
                stringBuilder.Append(DecodeChar(character, intKey));
            }

            return stringBuilder.ToString();
        }

        private int ParseIntKey(string key)
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

        private char EncodeChar(char source, int key)
        {
            if (!char.IsLetter(source))
            {
                return source;
            }

            char minChar = char.IsUpper(source) ? 'A' : 'a';
            return (char)((source + key - minChar) % AlphabetNumber + minChar);
        }

        private char DecodeChar(char source, int key)
        {
            char minChar = char.IsUpper(source) ? 'A' : 'a';
            return (char) ((AlphabetNumber + source - key - minChar) % AlphabetNumber + minChar);
        }
    }
}
