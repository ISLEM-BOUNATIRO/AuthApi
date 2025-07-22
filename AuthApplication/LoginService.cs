using AuthApplication.Models;
using AuthDomain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApplication;

public class LoginService
{
    private readonly IUserRepository _userRepo;
    private readonly string _jwtKey;

    public LoginService(IUserRepository userRepo, string jwtKey)
    {
        _userRepo = userRepo;
        _jwtKey = jwtKey;
    }

    public LoginResponse? Authenticate(LoginRequest request)
    {
        var user = _userRepo.GetUserByEmail(request.Email);
        if (user == null || user.Password != request.Password)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new LoginResponse { Token = tokenHandler.WriteToken(token) };
    }
}
