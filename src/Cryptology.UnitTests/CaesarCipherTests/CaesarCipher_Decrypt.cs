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
        public void Decrypt_SimpleWord_DecryptsCorrectly()
        {
            var message = "ddd";
            var key = "3";
            var expected = "aaa";
            var result = _caesarCipher.Decrypt(message, key);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("abc", 3, "xyz")]
        [InlineData("a", 1, "z")]
        public void Decrypt_LimitValues_DecryptsCorrectly(string message, int key, string expected)
        {
            var result = _caesarCipher.Decrypt(message, key.ToString());
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decrypt_LimitKeys_EncryptsCorrectly()
        {
            var message = "a";
            var key = "26";
            var expected = "a";
            var result = _caesarCipher.Decrypt(message, key);
            Assert.Equal(expected, result);
        }
    }
}