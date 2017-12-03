using System;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2016.Solvers
{
    internal class Md5Hasher
    {
        private readonly MD5 _hashFunction = MD5.Create();

        public virtual string HashDataAsHexString(string testData)
        {
            _hashFunction.Initialize();
            var hashBytes = _hashFunction.ComputeHash(Encoding.ASCII.GetBytes(testData));

            return BitConverter.ToString(hashBytes, 0, hashBytes.Length)
                .Replace("-", string.Empty);
        }
    }
}