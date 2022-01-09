using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Core.Utils
{

    public static class PasswordHashUtils
    {

        public static string GetPasswordHash(string password)
        {
            return string.Concat(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("x2")));
        }

        public static bool IsPasswordMatch(string password, string hashedPassword)
        {
            return hashedPassword.Equals(GetPasswordHash(password));
        }

    }

}
