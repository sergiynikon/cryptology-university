using System.Linq;
using System.Text;
using System.Xml.Xsl;
using Cryptology.Domain.Abstract;

namespace Cryptology.Domain.Ciphers
{
    public class VigenereCipher: ICipher
    {
        //only uppercase latin letters. Uncomment to use in test purposes.
        //private static string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        //all symbols on the keyboard
        private static string Alphabet =
            "`1234567890[]',.pyfgcrl/=\\aoeuidhtns-;qjkxbmwvz ~!@#$%^&*(){}\"<>PYFGCRL?+|AOEUIDHTNS_:QJKXBMWVZ";

        public string Encrypt(string message, string key)
        {
            var messageLength = message.Length;
            var fullKey = GetFullKey(messageLength, key);

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < messageLength; i++)
            {
                stringBuilder.Append(
                    Alphabet[(Alphabet.IndexOf(message[i]) + Alphabet.IndexOf(fullKey[i])) % Alphabet.Length]);
            }

            return stringBuilder.ToString();
        }

        public string Decrypt(string message, string key)
        {
            var messageLength = message.Length;
            var fullKey = GetFullKey(messageLength, key);

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < messageLength; i++)
            {
                stringBuilder.Append(
                    Alphabet[(Alphabet.IndexOf(message[i]) + Alphabet.Length - Alphabet.IndexOf(fullKey[i])) % Alphabet.Length]);
            }

            return stringBuilder.ToString();
        }

        private string GetFullKey(int length, string key)
        {
            var stringBuilder = new StringBuilder(length);
            while (stringBuilder.Length < length)
            {
                stringBuilder.Append(key);
            }

            return stringBuilder.ToString().Substring(0, length);
        }
    }
}