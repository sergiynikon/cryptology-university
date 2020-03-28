using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptology.Domain.Algorithms
{
    public interface ICipher
    {
        string Encrypt(string source, string key);
        string Decrypt(string source, string key);
    }
}
