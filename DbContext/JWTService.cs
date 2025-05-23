﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;

using Configuration;
using Models.DTO;

namespace DbContext;

public class JWTService
{
    private readonly JwtOptions _jwtOptions;

    public JWTService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;        
    }

    //Create a list of claims to encrypt into the JWT token
 private IEnumerable<Claim> CreateClaims(LoginUserSessionDto usrSession, out Guid TokenId)
{
    TokenId = Guid.NewGuid();

        IEnumerable<Claim> claims = [
            //used to carry the loginUserSessionDto in the token
            new("UserId", usrSession.UserId.ToString()),
            new("UserRole", usrSession.UserRole),
            new("UserName", usrSession.UserName),

            //used by Microsoft.AspNetCore.Authentication and used in the HTTP request pipeline
            new(ClaimTypes.Role, usrSession.UserRole),
            new(ClaimTypes.NameIdentifier, TokenId.ToString()),
            new(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTimeMinutes).ToString("MMM ddd dd yyyy HH:mm:ss tt")),
            new(ClaimTypes.Email, usrSession.Email),
        ];
        return claims;
    }

    public JwtUserToken CreateJwtUserToken(LoginUserSessionDto _usrSession)
    {   
        if (_usrSession == null) throw new ArgumentException($"{nameof(_usrSession)} cannot be null");

        var _userToken = new JwtUserToken();
        Guid tokenId = Guid.Empty;

        //get the key from user-secrets and set token expiration time
        var key = System.Text.Encoding.ASCII.GetBytes(_jwtOptions.IssuerSigningKey);
        DateTime expireTime = DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTimeMinutes);

        //generate the token, including my own defined claims, expiration time, signing credentials
        var JWToken = new JwtSecurityToken(issuer: _jwtOptions.ValidIssuer,
            audience: _jwtOptions.ValidAudience,
            claims: CreateClaims(_usrSession, out tokenId),
            notBefore: new DateTimeOffset(DateTime.UtcNow).DateTime,
            expires: new DateTimeOffset(expireTime).DateTime,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

        //generate a JWT user token with some unencrypted information as well
        _userToken.TokenId = tokenId;
        _userToken.EncryptedToken = new JwtSecurityTokenHandler().WriteToken(JWToken);
        _userToken.ExpireTime = expireTime;
        _userToken.UserRole = _usrSession.UserRole;
        _userToken.UserName = _usrSession.UserName;
        _userToken.UserId = _usrSession.UserId.Value;
        _userToken.Email = _usrSession.Email;

        Console.WriteLine($"CreateJwtUserToken was called with UserId: {_usrSession.UserId}");
        return _userToken;
    }

    public LoginUserSessionDto DecodeToken(string _encryptedtoken)
    {
        if (_encryptedtoken == null) return null;

        var _decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(_encryptedtoken);

        var _usr = new LoginUserSessionDto();
        foreach (var claim in _decodedToken.Claims)
        {
            switch (claim.Type)
            {
                case "UserId":
                    _usr.UserId = Guid.Parse(claim.Value);
                    break;
                case "UserName":
                    _usr.UserName = claim.Value;
                    break;
                case "UserRole":
                    _usr.UserRole = claim.Value;
                    break;
                case "Email":
                    _usr.Email = claim.Value;
                    break;
            }
        }
        return _usr;
    }
 private IEnumerable<Claim> CreateClaims(LoginStaffSessionDto usrSession, out Guid TokenId)
{
    TokenId = Guid.NewGuid();

        IEnumerable<Claim> claims = [
            //used to carry the loginUserSessionDto in the token
            new("StaffId", usrSession.StaffId.ToString()),
            new("UserRole", usrSession.UserRole),
            new("UserName", usrSession.UserName),

            //used by Microsoft.AspNetCore.Authentication and used in the HTTP request pipeline
            new(ClaimTypes.Role, usrSession.UserRole),
            new(ClaimTypes.NameIdentifier, TokenId.ToString()),
            new(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTimeMinutes).ToString("MMM ddd dd yyyy HH:mm:ss tt")),
            new(ClaimTypes.Email, usrSession.Email),
        ];
        return claims;
    }

    public JwtUserToken CreateJwtStaffToken(LoginStaffSessionDto _usrSession)
    {   
        if (_usrSession == null) throw new ArgumentException($"{nameof(_usrSession)} cannot be null");

        var _userToken = new JwtUserToken();
        Guid tokenId = Guid.Empty;

        //get the key from user-secrets and set token expiration time
        var key = System.Text.Encoding.ASCII.GetBytes(_jwtOptions.IssuerSigningKey);
        DateTime expireTime = DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTimeMinutes);

        //generate the token, including my own defined claims, expiration time, signing credentials
        var JWToken = new JwtSecurityToken(issuer: _jwtOptions.ValidIssuer,
            audience: _jwtOptions.ValidAudience,
            claims: CreateClaims(_usrSession, out tokenId),
            notBefore: new DateTimeOffset(DateTime.UtcNow).DateTime,
            expires: new DateTimeOffset(expireTime).DateTime,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

        //generate a JWT user token with some unencrypted information as well
        _userToken.TokenId = tokenId;
        _userToken.EncryptedToken = new JwtSecurityTokenHandler().WriteToken(JWToken);
        _userToken.ExpireTime = expireTime;
        _userToken.UserRole = _usrSession.UserRole;
        _userToken.UserName = _usrSession.UserName;
        _userToken.UserId = _usrSession.StaffId.Value;
        _userToken.Email = _usrSession.Email;

        Console.WriteLine($"CreateJwtUserToken was called with UserId: {_usrSession.StaffId}");
        return _userToken;
    }

    public LoginStaffSessionDto DecodeTokenStaff(string _encryptedtoken)
{
    if (_encryptedtoken == null) return null;

    var _decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(_encryptedtoken);

    var _usr = new LoginStaffSessionDto();
    foreach (var claim in _decodedToken.Claims)
    {
        switch (claim.Type)
        {
            case "StaffId":
                _usr.StaffId = Guid.Parse(claim.Value);
                break;
            case "UserName":
                _usr.UserName = claim.Value;
                break;
            case "UserRole":
                _usr.UserRole = claim.Value;
                break;
            case "Email":
                _usr.Email = claim.Value;
                break;
        }
    }

    Console.WriteLine($"Decoded Staff Token => ID: {_usr.StaffId}, Name: {_usr.UserName}, Role: {_usr.UserRole}, Email: {_usr.Email}");
    return _usr;
}

    
}