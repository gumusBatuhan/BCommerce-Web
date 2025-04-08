using Azure;
using BCommerce.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BCommerce.Insfrastructure.Helpers
{
    public class ValidationHelper : IValidationHelper
    {
        public bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            return match.Success;
          
        }

        public bool IsValidTC(string tc)
        {
            Regex regex = new Regex(@"^[1-9]{1}[0-9]{9}[02468]{1}$");
            Match match = regex.Match(tc);

            return match.Success;
        }
    }
}
