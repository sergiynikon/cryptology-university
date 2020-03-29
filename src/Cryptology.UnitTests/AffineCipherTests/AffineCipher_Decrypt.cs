using System;
using Cryptology.Domain.Ciphers;
using Xunit;

namespace Cryptology.UnitTests.AffineCipherTests
{
    public class AffineCipher_Decrypt
    {
        private readonly AffineCipher _affineCipher;

        public AffineCipher_Decrypt()
        {
            _affineCipher = new AffineCipher();
        }

        [Fact]
        public void Decrypt_SimpleMessage_EncryptsCorrectly()
        {
            var message = "armmv";
            var key = "3, 5";
            var expected = "hello";
            var result = _affineCipher.Decrypt(message, key);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(" ", "3, 5", " ")]
        [InlineData("%", "3, 5", "%")]
        public void Decrypt_NotLetterCharacters_ReturnsSameCharacters(string message, string key, string expected)
        {
            var result = _affineCipher.Decrypt(message, key);
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData("1,,2")]
        [InlineData("2.3")]
        [InlineData("2 3")]
        [InlineData("1")]
        [InlineData("1,3,4")]
        public void Decrypt_InvalidKeyFormat_ThrowsArgumentException(string key)
        {
            var message = "a";
            Assert.Throws<ArgumentException>(() => _affineCipher.Decrypt(message, key));
        }


        [Theory]
        [InlineData("3, 27")]
        [InlineData("3, 1000")]
        public void Decrypt_BetaGreaterThanAlphabetNumber_ThrowsArgumentException(string key)
        {
            var message = "a";
            Assert.Throws<ArgumentException>(() => _affineCipher.Decrypt(message, key));
        }
    }
}