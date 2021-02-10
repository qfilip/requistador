using Requistador.Identity.Enumerations;
using System;

namespace Requistador.Identity.Entites
{
    internal class AppUser
    {
        public string Id { get; set; }
        public string AppId { get; set; }

        public eUserRole Role { get; set; }
        public eUserStatus Status { get; set; }
        
        public string InternalName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string PasswordSalt { get; set; }
    }
}
