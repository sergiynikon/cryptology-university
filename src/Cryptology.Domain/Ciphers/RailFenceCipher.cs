using System;
using System.Text;
using Cryptology.Domain.Abstract;

namespace Cryptology.Domain.Ciphers
{
    // [message = WE ARE DISCOVERED. FLEE AT ONCE]
    // [Key = 3]
    // 
    // | | | | | | | | | | | | | | | |
    // V V V V V V V V V V V V V V V V
    //
    // W   R   I   V   D   L   A   N 
    //  E A E D S O E E . F E _ T O C
    //   _   _   C   R   _   E   _   E 
    //
    public class RailFenceCipher : ICipher
    {
        public string Encrypt(string message, string key)
        {
            int intKey = ParseKey(key);

            int messageLength = message.Length;
            int chunkIndent = (intKey - 1) * 2;

            var stringBuilder = new StringBuilder(messageLength);

            for (int i = 0; i < intKey; i++)
            {
                int chunkIndex = 0;
                while (i + chunkIndent * chunkIndex < messageLength)
                {
                    stringBuilder.Append(message[i + chunkIndent * chunkIndex]);
                    if (i > 0 && i < intKey - 1)
                    {
                        if((chunkIndent - i) + chunkIndent * chunkIndex < messageLength)
                        {
                            stringBuilder.Append(message[(chunkIndent - i) + chunkIndent * chunkIndex]);
                        }
                    }
                    chunkIndex++;
                }
            }

            return stringBuilder.ToString();
        }

        public string Decrypt(string message, string key)
        {
            var intKey = ParseKey(key);
            int messageLength = message.Length;
            int chunkIndent = (intKey - 1) * 2;

            char[] letters = new char[messageLength];
            int cipherMessageIndex = 0;
            for (int i = 0; i < intKey; i++)
            {
                int chunkIndex = 0;
                while (i + chunkIndex * chunkIndent < messageLength)
                {
                    letters[i + chunkIndex * chunkIndent] = message[cipherMessageIndex++];
                    if(i > 0 && i < intKey - 1)
                    {
                        if((chunkIndent - i) + chunkIndent * chunkIndex < messageLength)
                        {
                            letters[(chunkIndent - i) + chunkIndent * chunkIndex] = message[cipherMessageIndex++];
                        }
                    }

                    chunkIndex++;
                }
            }
            return new string(letters);
        }

        private int ParseKey(string strKey)
        {
            try
            {
                var res = int.Parse(strKey);
                return res;
            }
            catch (FormatException)
            {
                throw new FormatException($"Cannot convert key '{strKey}' to int value!");
            }
            catch (Exception e)
            {
                throw new Exception($"An unhandled exception occured: {e.Message}");
            }
        }
    }
}