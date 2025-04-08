using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.Helpers
{
    public interface IValidationHelper
    {
        bool IsValidEmail(string email);

        bool IsValidTC(string tc);
    }
}
