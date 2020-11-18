using HotelManagement.Data.Auth;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IIdentityRepository
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);
    }
}
