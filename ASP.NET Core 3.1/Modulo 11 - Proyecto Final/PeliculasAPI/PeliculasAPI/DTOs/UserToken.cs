using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
