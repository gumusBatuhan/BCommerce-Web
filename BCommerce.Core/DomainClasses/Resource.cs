using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Core.DomainClasses
{
    public class Resource : BaseEntity
    { 
        public string LanguageKey { get; set; } //tr-TR  en-US

        public string Key { get; set; } //ValidationError_Email

        public string Value { get; set; } 
    }
}


//id languagekey  key value

//1   tr-TR      ValidationError_Email   "HATALI EMAİL ADRESİ"
//2   en-US      ValidationError_Email   "ınvalid mail adres"
//3   ru-RU      ValidationError_Email    "....."