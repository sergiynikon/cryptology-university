using Cryptology.Domain.Ciphers;
using Xunit;

namespace Cryptology.UnitTests.CaesarCipherTests
{
    public class CaesarCipher_Encrypt
    {
        private readonly CaesarCipher _caesarCipher;

        public CaesarCipher_Encrypt()
        {
            _caesarCipher = new CaesarCipher();
        }

        [Fact]
        public void Encode_SimpleWord_EncryptsCorrectly()
        {
            var input = "aaa";
            var key = "3";
            var expected = "ddd";
            var result = _caesarCipher.Encrypt(input, key);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(" ", 3, " ")]
        [InlineData("%", 1, "%")]
        public void Encode_NotLetterCharacters_ReturnsSameCharacters(string value, int key, string expected)
        {
            var result = _caesarCipher.Encrypt(value, key.ToString());
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("z", 1, "a")]
        [InlineData("X", 4, "B")]
        public void Encode_LimitValues_EncryptsCorrectly(string value, int key, string expected)
        {
            var result = _caesarCipher.Encrypt(value, key.ToString());
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Encode_LimitKeys_EncryptsCorrectly()
        {
            var input = "a";
            var key = "26";
            var expected = "a";
            var result = _caesarCipher.Encrypt(input, key);
            Assert.Equal(expected, result);
        }
    }
}