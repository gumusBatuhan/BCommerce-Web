using BCommerce.Application.Helpers;
using BCommerce.Core.DomainClasses;
using BCommerce.Core.DTO;
using Microsoft.AspNetCore.Identity;
using BCommerce.Insfrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Insfrastructure.Helpers
{
    public class IdentityHelper : IIdentityHelper
    {
        //UserManager identity tarafından sağlanan kullanıcı ile ilgili kayıt ii şifremi unuttum .... gibi işlemleri içeren güçlü bir sınıftır.
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityHelper(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<Result<ApplicationUser>> CheckUserEmailAndPassword(string email, string password)
        {
            Result<ApplicationUser> result = new Result<ApplicationUser>();
            ApplicationUser user = await _userManager.FindByEmailAsync(email);


            if (user == null)
            {
                result.Errors = new List<string> { "Kullanıcı adı veya şifre hatalı" };
                return result;
            }

            bool hasThisPass = await _userManager.CheckPasswordAsync(user, password);

            if(!hasThisPass)
            {
                result.Errors = new List<string> { "Kullanıcı adı veya şifre hatalı" };
                return result;
            }

            result.IsSuccess = true;
            result.Data = user;
            return result;
        }


        public async Task<Result> RegisterAsync(ApplicationUser applicationUser, string password, string role)
        {

            IdentityResult identityResult = await _userManager.CreateAsync(applicationUser, password);




            if (identityResult == IdentityResult.Success)
            {
                await _userManager.AddToRoleAsync(applicationUser, role);
            }

            return identityResult.ConvertToResult();

        }


    }
}
