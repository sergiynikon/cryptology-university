using Cryptology.Domain.Ciphers;
using Xunit;

namespace Cryptology.UnitTests.CaesarCipherTests
{
    public class CaesarCipher_Decrypt
    {
        private readonly CaesarCipher _caesarCipher;

        public CaesarCipher_Decrypt()
        {
            _caesarCipher = new CaesarCipher();
        }

        [Fact]
        public void Decode_SimpleWord_DecryptsCorrectly()
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
        public void Decode_LimitValues_DecryptsCorrectly(string value, int key, string expected)
        {
            var result = _caesarCipher.Decrypt(value, key.ToString());
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_LimitKeys_EncryptsCorrectly()
        {
            var input = "a";
            var key = "26";
            var expected = "a";
            var result = _caesarCipher.Decrypt(input, key);
            Assert.Equal(expected, result);
        }
    }
}