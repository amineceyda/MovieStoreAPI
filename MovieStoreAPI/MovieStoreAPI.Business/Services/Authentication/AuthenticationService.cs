using Microsoft.AspNetCore.Identity;
using MovieStoreAPI.Business.Models;
using MovieStoreAPI.Data.Entities;
using System.Security.Claims;

namespace MovieStoreAPI.Business.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;

    public AuthenticationService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task<Status> LoginAsync(LoginRequest request)
    {
        var status = new Status();
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is null)
        {
            status.StatusCode = 0;
            status.Message = "Invalid username";
            return status;
            
        }

        if(!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            status.StatusCode = 0;
            status.Message = "Invalid Password";
            return status;
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);
        if (signInResult.Succeeded)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            status.StatusCode = 1;
            status.Message = "Logged in successfully";
        }
        else if (signInResult.IsLockedOut) 
        {
            status.StatusCode = 0;
            status.Message = "User is locked out";
        }
        else
        {
            status.StatusCode = 0;
            status.Message = "Error on logging in";
        }

        return status;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<Status> RegisterAsync(RegistrationRequest request)
    {
        var status = new Status();
        var userExists = await _userManager.FindByNameAsync(request.Username);
        if(userExists is not null)
        {
            status.StatusCode = 0;
            status.Message = "User already exist";
            return status;
        }

        User user = new User()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username,
            Name = request.Name,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if(!result.Succeeded)
        {
            status.StatusCode = 0;
            status.Message = "User creation failed";
            return status;
        }

        if (!await _roleManager.RoleExistsAsync(request.Role))
            await _roleManager.CreateAsync(new IdentityRole(request.Role));
        if (await _roleManager.RoleExistsAsync(request.Role))
            await _userManager.AddToRoleAsync(user, request.Role);

        status.StatusCode = 1;
        status.Message = "You have registered successfully";
        return status;
       
    }
}
