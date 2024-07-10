using Commons.DTOs;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.AuthService
{
    public class AuthService : IAuthService
    {

        private readonly string secretKey;

        private readonly IRepository<User> userRepository;

        public AuthService(IRepository<User> userRepository, IConfiguration config)
        {
            this.userRepository = userRepository;

            secretKey = config.GetSection("settings").GetSection("secretKey")?.Value;

        }

        public async Task<Response<AuthResponse>> Authenticate(AuthRequest userReq)
        {

            var response = new Response<AuthResponse>();

            try
            {

                Func<User, bool> predicate = u => u.UserName == userReq.UserName && u.Password == userReq.Password;

                var userAuth = await userRepository.GetByEntity(predicate);

                if (userAuth != null)
                {
                    var keyBytes = Encoding.ASCII.GetBytes(secretKey);

                    var claims = new ClaimsIdentity();

                    claims.AddClaim(new Claim(ClaimTypes.Name, userAuth.UserName));
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userAuth.Id.ToString()));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(120),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string tokencreated = tokenHandler.WriteToken(tokenConfig);

                    var authResponse = new AuthResponse()
                    {
                        UserId = userAuth.Id,
                        UserName = userAuth.UserName,
                        Token = tokencreated
                    };
                    response.message = "Usuario autenticado";
                    response.data = authResponse;
                    response.status = 200;
                }
                else
                {
                    response.message = "Usuario invalido";
                    response.status = 401;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message + ex.InnerException;
                response.status = 400;
            }

            return response;
        }
    }
}
