using BCommerce.Application.Data;
using BCommerce.Application.Helpers;
using BCommerce.Application.Utility;
using BCommerce.Core.DomainClasses;
using BCommerce.Core.DTO;
using BCommerceWeb.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BCommerce.Application.Services
{
    public class IdentityService
    {
        private readonly IIdentityHelper _identityHelper;
        private readonly IValidationHelper _validationHelper;
        private readonly IApplicationDbContext _applicationDbContext;


        public IdentityService(IIdentityHelper identityHelper, IValidationHelper validationHelper, IApplicationDbContext applicationDbContext)
        {
            _identityHelper = identityHelper;
            _validationHelper = validationHelper;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Result> Register(ApplicationUser applicationUser, string password, string role)
        {
            if (!_validationHelper.IsValidEmail(applicationUser.Email))
            {
                return Result.Error(Resources.L("ValidationError_Email"));
            }


            return await _identityHelper.RegisterAsync(applicationUser, password, role);

        }

        public async Task<Result<JwtSecurityToken>> LoginWithJwtAsync(string email, string password, string signingKey, string issuer, string audience)
        {
            //username ve password doğru değilse bu kişiye token üretmemiz lazjım.
            Result<ApplicationUser> result = await _identityHelper.CheckUserEmailAndPassword(email, password);

            if (!result.IsSuccess)
            {
                return new Result<JwtSecurityToken>
                {
                    IsSuccess = false,
                    Errors = result.Errors

                };

            }


           
            var claims = new List<Claim>();
            claims.Add(new Claim("userid", result.Data.Id));
            claims.Add(new Claim("username", result.Data.Email));
            claims.Add(new Claim("displayname", result.Data.NameSurname));
            claims.Add(new Claim(ClaimTypes.Name, result.Data.Email));


            //bize bir jwt tokoen olusturur.
            var token = JwtHelper.GetJwtToken(result.Data.Id, result.Data.UserName, signingKey, issuer, audience, TimeSpan.FromMinutes(1200), claims.ToArray());



            return new Result<JwtSecurityToken>
            {
                Data = token,
                IsSuccess = true
            };

        }




    }
}
