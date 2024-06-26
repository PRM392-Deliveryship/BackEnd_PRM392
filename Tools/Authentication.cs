
using GaVietNam_Repository.Entities;
using GaVietNam_Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Tools;
public class Authentication
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public Authentication(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    public string GenerateToken(Admin info)
    {
        List<Claim> claims = new List<Claim>()
        {            
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, info.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
        };

        if (info.Id != 0)
        {
            var role = _unitOfWork.RoleRepository.Get(filter: r => r.Id == info.Id).FirstOrDefault();
            claims.Add(new Claim("role", role.RoleName));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    public bool VerifyPassword(string providedPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static string GetUserIdFromHttpContext(HttpContext httpContext)
    {
        if (!httpContext.Request.Headers.ContainsKey("Authorization"))
        {
            throw new CustomException.InternalServerErrorException("Need Authorization");
        }

        string? authorizationHeader = httpContext.Request.Headers["Authorization"];

        if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.StartsWith("bearer "))
        {
            throw new CustomException.InternalServerErrorException(
                $"Invalid authorization header: {authorizationHeader}");
        }

        string jwtToken = authorizationHeader["bearer ".Length..];
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(jwtToken);
        var idClaim = token.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);
        return idClaim?.Value ??
               throw new CustomException.InternalServerErrorException($"Can not get userId from token");

    }
}
