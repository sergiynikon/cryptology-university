using Cryptology.Domain.Algorithms;
using Xunit;

namespace Cryptology.UnitTests.CaesarEncodeTests
{
    public class CaesarEncode_Decode
    {
        private readonly CaesarCipher _caesarCipher;

        public CaesarEncode_Decode()
        {
            _caesarCipher = new CaesarCipher();
        }

        [Fact]
        public void Decode_SimpleWord_DecodesCorrectly()
        {
            var input = "ddd";
            var key = "3";
            var expected = "aaa";
            var result = _caesarCipher.Decrypt(input, key);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("abc", 3, "xyz")]
        [InlineData("a", 1, "z")]
        public void Decode_LimitValues_DecodesCorrectly(string value, int key, string expected)
        {
            var result = _caesarCipher.Decrypt(value, key.ToString());
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_BigValues_EncodesCorrectly()
        {
            var input = "a";
            var key = "26";
            var expected = "a";
            var result = _caesarCipher.Decrypt(input, key);
            Assert.Equal(expected, result);
        }
    }
}