using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    { 
        public static void CreatePasswordHash(string Password,out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hMac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hMac.Key;
                PasswordHash = hMac.ComputeHash(Encoding.UTF8.GetBytes(Password));
            } 
        }

        public static bool VerifyPassword(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hMac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
                var computedHash = hMac.ComputeHash(Encoding.UTF8.GetBytes(Password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (PasswordHash[i]!=computedHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
