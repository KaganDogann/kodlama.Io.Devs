using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Dtos
{
    public class LoginUserDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
