using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.JWT
{
    public class JwtTokenConfig
    {
        // Secret key used for token signing and validation.
        public string Secret { get; set; }

        // The issuer of the token (typically your application's name).
        public string Issuer { get; set; }

        // The audience for which the token is intended.
        public string Audience { get; set; }

        // The expiration time (in minutes) for access tokens.
        public int AccessTokenExpiration { get; set; }

        // The expiration time (in minutes) for refresh tokens.
        public int RefreshTokenExpiration { get; set; }
    }
}
