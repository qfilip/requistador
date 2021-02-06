using Requistador.Identity.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Requistador.Identity
{
    internal static partial class Functions
    {
        internal static string GeneratePasswordSalt()
        {
            var bytes = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            return Convert.ToBase64String(bytes);
        }

        internal static void GenerateAppKey(Guid appId, string keyName)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                var appKey = new AppKey
                {
                    AppId = appId,
                    Name = keyName,
                    PublicKey = rsa.ExportParameters(false),
                    PrivateKey = rsa.ExportParameters(true)
                };
            }
        }

        internal static UserParameters EncryptUserPassword(AppKey appKey, string password)
        {
            var salt = GeneratePasswordSalt();
            var saltEncrypted = Encrypt(appKey, salt);
            var passwordEncrypted = Encrypt(appKey, password);

            var builder = new StringBuilder();
            var length = passwordEncrypted.Length;

            for (int i = 0; i < length; i++)
            {
                builder.Append((char)(passwordEncrypted[i] + saltEncrypted[i]));
            }


            return new UserParameters
            {
                EncryptedPassword = builder.ToString(),
                EncryptedSalt = saltEncrypted
            };
        }

        internal static string DecryptuserPassword(AppKey appKey, string passwordEncrypted, string saltEncrypted)
        {
            var builder = new StringBuilder();
            var length = passwordEncrypted.Length;

            for (int i = 0; i < length; i++)
            {
                builder.Append((char)(passwordEncrypted[i] - saltEncrypted[i]));
            }


            return Decrypt(appKey, builder.ToString());
        }

        private static string Encrypt(AppKey appKey, string plainText)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(appKey.PublicKey);
                var data = Encoding.UTF8.GetBytes(plainText);
                var cypher = rsa.Encrypt(data, false);

                return Convert.ToBase64String(cypher);
            }
        }

        private static string Decrypt(AppKey appKey, string cypherText)
        {
            var dataBytes = Convert.FromBase64String(cypherText);
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(appKey.PrivateKey);
                var plainText = rsa.Decrypt(dataBytes, false);

                return Encoding.UTF8.GetString(plainText);
            }
        }
    }
}
