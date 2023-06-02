using System;

namespace MyMarket.Common
{
    internal class KeyDerivationPrf
    {
        public static KeyDerivationPrf HMACSHA256 { get; internal set; }

        public static explicit operator uint(KeyDerivationPrf v)
        {
            throw new NotImplementedException();
        }
    }
}