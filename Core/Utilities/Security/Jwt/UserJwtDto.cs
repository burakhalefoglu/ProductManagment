using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt
{
    public class UserJwtDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string AuthenticationProviderType { get; set; }
    }
}
