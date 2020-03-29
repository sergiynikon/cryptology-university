using Cryptology.Domain.Ciphers;
using Xunit;

namespace Cryptology.UnitTests.AffineCipherTests
{
    public class AffineCipher_Encrypt
    {
        private readonly AffineCipher _affineCipher;

        public AffineCipher_Encrypt()
        {
            _affineCipher = new AffineCipher();
        }

        [Fact]
        public void AffineCipher_SimpleMessage_EncryptsCorrectly()
        {
            var message = "ab";
            var key = "3, 5";
            var expected = "fi";
            var result = _affineCipher.Encrypt(message, key);
            Assert.Equal(expected, result);
        }
    }
}