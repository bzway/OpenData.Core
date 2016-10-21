using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bzway.Common.Utility
{
    public class Cryptor : ICryptor
    {
        public string EnCode(string input)
        {
            return string.Empty;
        }
        public string UnCode(string input)
        {
            return string.Empty;
        }
    }
    public interface ICryptor
    {
        string EnCode(string input);
        string UnCode(string input);
    }
}