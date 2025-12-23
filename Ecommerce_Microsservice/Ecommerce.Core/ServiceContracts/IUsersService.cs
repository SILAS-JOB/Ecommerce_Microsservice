
using Ecommerce.Core.DTO;

namespace Ecommerce.Core.ServiceContracts;
    public interface IUsersService
    {
        Task<AuthenticationResponse?> Login(LoginRequest loginRequest);
        Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);
    }