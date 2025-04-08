using BCommerce.Core.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Insfrastructure.Extensions
{
    public static class IdentityResultExtensions
    {
        public static Result ConvertToResult(this IdentityResult identityResult)
        {

            Result result = new Result();
            result.IsSuccess = identityResult.Succeeded;
            result.Errors = identityResult.Errors.Select(t=>t.Description).ToList();
            

            return result;
        }
    }
}
