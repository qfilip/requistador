using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Requistador.Identity.Models
{
    internal class AppKey
    {
        public Guid AppId { get; set; }
        public string Name { get; set; }

        public RSAParameters PublicKey { get; set; }
        public RSAParameters PrivateKey { get; set; }
    }
}
