using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SPF
{
    class Seguridad
    {
        byte[] encripted;

        public string Encriptar(string a)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();

            TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
            tDes.Key = md5.ComputeHash(utf8.GetBytes(a));
            tDes.Mode = CipherMode.ECB;
            tDes.Padding = PaddingMode.PKCS7;
            ICryptoTransform trans = tDes.CreateEncryptor();
            encripted = trans.TransformFinalBlock(utf8.GetBytes(a), 0, utf8.GetBytes(a).Length);
            a = BitConverter.ToString(encripted);

            return a;
        }

        public string Decriptar(string a)
        {



            return a;
        }
    }
}
