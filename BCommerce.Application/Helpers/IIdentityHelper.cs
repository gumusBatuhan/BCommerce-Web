using BCommerce.Core.DomainClasses;
using BCommerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.Helpers
{
    public interface IIdentityHelper
    {

        Task<Result<ApplicationUser>> CheckUserEmailAndPassword(string email, string password);
        Task<Result> RegisterAsync(ApplicationUser applicationUser, string password,string role);
    }
}
