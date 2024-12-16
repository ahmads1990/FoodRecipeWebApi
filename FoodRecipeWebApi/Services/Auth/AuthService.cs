﻿using FoodRecipeWebApi.Data.Repo;
using FoodRecipeWebApi.Helpers;
using FoodRecipeWebApi.Helpers.Config;
using FoodRecipeWebApi.Models;
using FoodRecipeWebApi.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodRecipeWebApi.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepo;
    private readonly IRepository<UserClaim> _UserClaimRepo;
    private readonly JwtConfig _jwtConfig;

    public AuthService(IRepository<User> userRepo, IRepository<UserClaim> userClaimRepo, IOptions<JwtConfig> jwtConfig)
    {
        _userRepo = userRepo;
        _UserClaimRepo = userClaimRepo;
        _jwtConfig = jwtConfig.Value;
    }

    public async Task<AuthViewModel> LoginUser(LoginViewModel loginViewModel)
    {
        AuthViewModel authDto = new AuthViewModel();
        // return if email doesn't exist OR email+password don't match
        var user = _userRepo.GetByCondition(u => u.Email.Equals(loginViewModel.Email)).FirstOrDefault();

        if (user is null || !_userRepo.CheckByConidition(u => u.Password.Equals(loginViewModel.Password)))
        {
            authDto.Message = "Email or Password is incorrect!";
            return authDto;
        }

        var claims = _UserClaimRepo.GetByCondition(c => c.UserId == user.ID).ToList();

        var jwtToken = TokenHelper.CreateJwtToken(user, claims, _jwtConfig);

        authDto.IsAuthenticated = true;
        authDto.UserID = user.ID;
        authDto.Username = user.Name ?? string.Empty;
        authDto.Email = user.Email ?? string.Empty;
        authDto.Claims = claims.Select(c => c.Type).ToList();
        authDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        authDto.ExpiresOn = jwtToken?.ValidTo ?? DateTime.Now;

        return authDto;
    }

    public async Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var emailIsExist = _userRepo.CheckByConidition(x => x.Email == request.Email);
        if (emailIsExist)
            throw new InvalidOperationException("Email is already registered.");

        var passwordHasher = new PasswordHasher<User>();
        var user = request.Map<User>();
        user.Password = passwordHasher.HashPassword(user, request.Password);
        _userRepo.Add(user);
        await _userRepo.SaveChangesAsync();

    }




}
