using HotelManagement.Data.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Services
{
    public interface IIdentityRepository
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest authenticateRequest);
    }
}
