using MovieStoreAPI.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreAPI.Business.Services;

public interface IAuthenticationService
{
    Task<Status> LoginAsync(LoginRequest request);
    Task LogoutAsync();
    Task<Status> RegisterAsync(RegistrationRequest request);
}
