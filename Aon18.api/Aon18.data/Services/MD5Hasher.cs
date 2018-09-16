using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Aon18.data.Interfaces;

namespace Aon18.data.Services
{
    public static class MD5Hasher
    {
        public static string CalculateHash(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = new MemoryStream(data))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
