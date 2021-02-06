using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Identity.Models
{
    internal class UserParameters
    {
        public string EncryptedPassword { get; set; }
        public string EncryptedSalt { get; set; }
    }
}
