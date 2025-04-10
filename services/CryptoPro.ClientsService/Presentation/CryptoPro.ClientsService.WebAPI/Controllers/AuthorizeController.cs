﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CryptoPro.ClientsService.Application.Users.Queries.GetUserByLoginData;
using CryptoPro.ClientsService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CryptoPro.ClientsService.WebAPI.Controllers;

[ApiController]
[Route("api/auth/")]
public sealed class AuthorizeController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IMediator _mediator;
    
    public AuthorizeController(IConfiguration config, IMediator mediator)
    {
        _config = config;
        _mediator = mediator;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromQuery] GetUserByLoginDataQuery request)
    {
        var user = await _mediator.Send(new GetUserByLoginDataQuery(request.Login, request.Password));

        if (user == null)
            return Unauthorized("Invalid credentials");
        
        var token = GenerateJwtToken(user);
        
        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(UserEntity user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Id.ToString()),  
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpiresInMinutes")),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}