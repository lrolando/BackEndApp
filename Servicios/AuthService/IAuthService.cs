using Commons.DTOs;
using Entities;

namespace Services.AuthService
{
    public interface IAuthService
    {
        public Task<Response<AuthResponse>> Authenticate(AuthRequest userReq);

    }
}
