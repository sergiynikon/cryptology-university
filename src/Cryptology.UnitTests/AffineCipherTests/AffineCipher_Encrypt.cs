﻿using System;
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
        public void Encrypt_SimpleMessage_EncryptsCorrectly()
        {
            var message = "hello";
            var key = "3, 5";
            var expected = "armmv";
            var result = _affineCipher.Encrypt(message, key);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(" ", "3, 5", " ")]
        [InlineData("%", "3, 5", "%")]
        public void Encrypt_NotLetterCharacters_ReturnsSameCharacters(string message, string key, string expected)
        {
            var result = _affineCipher.Encrypt(message, key);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1,,2")]
        [InlineData("2.3")]
        [InlineData("2 3")]
        [InlineData("1")]
        [InlineData("1,3,4")]
        public void Encrypt_InvalidKeyFormat_ThrowsArgumentException(string key)
        {
            var message = "a";
            Assert.Throws<ArgumentException>(() => _affineCipher.Encrypt(message, key));
        }


        [Theory]
        [InlineData("3, 27")]
        [InlineData("3, 1000")]
        public void Encrypt_BetaGreaterThanAlphabetNumber_ThrowsArgumentException(string key)
        {
            var message = "a";
            Assert.Throws<ArgumentException>(() => _affineCipher.Encrypt(message, key));
        }
    }
}