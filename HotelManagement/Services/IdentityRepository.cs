using HotelManagement.Data.Auth;
using HotelManagement.Helpers;
using HotelManagement.Models;
using HotelManagement.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly AppSettings _appSettings;
        private readonly DatabaseContext _context;

        public IdentityRepository(IOptions<AppSettings> appSettings, DatabaseContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest)
        {
            var admin = await _context.Administrators.SingleOrDefaultAsync(a => a.Email == authenticateRequest.Email && a.Password == Hash.GetHash(authenticateRequest.Password));

            if (admin == null)
            {
                return null;
            }

            var token = Token.GenerateToken(admin, _appSettings.Secret);

            return new AuthenticateResponse(admin, token);
        }
    }
}
