using Requistador.Identity.Enumerations;
using System;

namespace Requistador.Identity.Dtos
{
    public class AppUserDto
    {
        public Guid Id { get; set; }
        public eUserRole Role { get; set; }
        public eUserStatus Status { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Jwt { get; set; }
    }
}
