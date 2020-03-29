using Cryptology.Domain.Abstract;
using System;
using System.Text;

namespace Cryptology.Domain.Ciphers
{
    public class AffineCipher : ICipher
    {
        const int AlphabetNumber = 26;

        public string Encrypt(string message, string key)
        {
            var affineParameters = ParseKey(key);

            var stringBuilder = new StringBuilder();
            foreach (var character in message)
            {
                stringBuilder.Append(EncryptChar(character, affineParameters));
            }

            return stringBuilder.ToString();
        }

        public string Decrypt(string message, string key)
        {
            var affineParameters = ParseKey(key);

            var stringBuilder = new StringBuilder();
            foreach (var character in message)
            {
                stringBuilder.Append(DecryptChar(character, affineParameters));
            }

            return stringBuilder.ToString();
        }

        private static char EncryptChar(char character, AffineParameters parameters)
        {
            if (!char.IsLetter(character))
            {
                return character;
            }

            char minChar = char.IsUpper(character) ? 'A' : 'a';

            return (char)((parameters.A * (character - minChar) + parameters.B) % AlphabetNumber + minChar);
        }

        private static char DecryptChar(char character, AffineParameters parameters)
        {
            if (!char.IsLetter(character))
            {
                return character;
            }

            char minChar = char.IsUpper(character) ? 'A' : 'a';

            return (char)((ModInverseNumber(parameters.A) * (AlphabetNumber + character - minChar - parameters.B) % AlphabetNumber) %
                AlphabetNumber + minChar);
        }

        private static int ModInverseNumber(int number)
        {
            for (int i = 0; i < AlphabetNumber; i++)
            {
                if (i * number % AlphabetNumber == 1)
                {
                    return i;
                }
            }

            return number;
        }

        private static AffineParameters ParseKey(string key)
        {
            try
            {
                var parameters = key.Split(", ");
                if (parameters.Length > 2)
                {
                    throw new IndexOutOfRangeException();
                }

                var affineParameters = new AffineParameters
                {
                    A = int.Parse(parameters[0]),
                    B = int.Parse(parameters[1])
                };
                return affineParameters;
            }
            catch (FormatException)
            {
                throw new FormatException($"Cannot convert key '{key}' to affine parameters!");
            }
            catch (IndexOutOfRangeException)
            {
                throw new FormatException($"Affine parameters must have two numbers, split by comma (for example '3, 5')!");
            }
        }

    }

    public class AffineParameters
    {
        public int A { get; set; }
        public int B { get; set; }
    }
}