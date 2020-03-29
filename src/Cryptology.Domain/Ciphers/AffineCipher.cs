using Cryptology.Domain.Abstract;
using System;
using System.Linq;
using System.Text;

namespace Cryptology.Domain.Ciphers
{
    public class AffineCipher : ICipher
    {
        private const int AlphabetNumber = 26;
        private static readonly int[] MultiplicativeGroup = GetMultiplicativeGroup(AlphabetNumber);

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

            return (char)((parameters.Alpha * (character - minChar) + parameters.Beta) % AlphabetNumber + minChar);
        }

        private static char DecryptChar(char character, AffineParameters parameters)
        {
            if (!char.IsLetter(character))
            {
                return character;
            }

            char minChar = char.IsUpper(character) ? 'A' : 'a';

            return (char)((parameters.InverseAlpha * (AlphabetNumber + character - minChar - parameters.Beta) % AlphabetNumber) %
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
                var parameters = key.Trim().Split(',', ';');
                
                if (parameters.Length != 2)
                {
                    throw new ArgumentException("Affine parameters must have two values, split by comma");
                }

                var alpha = int.Parse(parameters[0]);
                var beta = int.Parse(parameters[1]);
                if (!MultiplicativeGroup.Contains(alpha))
                {
                    throw new ArgumentException(
                        $"Alpha must be in multiplicative group (greatest common divisor of alpha and {AlphabetNumber} must be 1)");
                }

                if (beta > AlphabetNumber)
                {
                    throw new ArgumentException($"Beta must be less, that {AlphabetNumber}");
                }

                var affineParameters = new AffineParameters
                {
                    Alpha = alpha,
                    InverseAlpha = ModInverseNumber(alpha),
                    Beta = beta
                };

                return affineParameters;
            }
            catch (FormatException)
            {
                throw new FormatException($"Cannot convert key '{key}' to affine parameters!");
            }
        }

        private static int[] GetMultiplicativeGroup(int number)
        {
            var rangeOfNumbers = Enumerable.Range(1, number - 1);
            var multiplicativeGroup = rangeOfNumbers
                .Where(current => GreatestCommonDivisor(current, number) == 1)
                .ToArray();

            return multiplicativeGroup;
        }

        private static int GreatestCommonDivisor(int num1, int num2)
        {
            while (num1 != 0 && num2 != 0)
            {
                if (num1 > num2)
                {
                    num1 %= num2;
                }
                else
                {
                    num2 %= num1;
                }
            }

            return num1 == 0 ? num2 : num1;
        }
    }

    public class AffineParameters
    {
        public int Alpha { get; set; }
        public int InverseAlpha { get; set; }
        public int Beta { get; set; }
    }
}