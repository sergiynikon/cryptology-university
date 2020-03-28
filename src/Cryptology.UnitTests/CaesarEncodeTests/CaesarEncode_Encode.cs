using Cryptology.Domain.Algorithms;
using Xunit;

namespace Cryptology.UnitTests.CaesarEncodeTests
{
    public class CaesarEncode_Encode
    {
        private readonly CaesarCipher _caesarCipher;

        public CaesarEncode_Encode()
        {
            _caesarCipher = new CaesarCipher();
        }

        [Fact]
        public void Encode_SimpleWord_Encodes()
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
        public void Encode_LimitValues_EncodesCorrectly(string value, int key, string expected)
        {
            var result = _caesarCipher.Encrypt(value, key.ToString());
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Encode_BigValues_EncodesCorrectly()
        {
            var input = "a";
            var key = "26";
            var expected = "a";
            var result = _caesarCipher.Encrypt(input, key);
            Assert.Equal(expected, result);
        }
    }
}