using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Core.DTO
{

    public class Result
    {
        //geri dönüş başarılı mı
        public bool IsSuccess { get; set; }

        public int ErrorCode { get; set; }


        //eğer basarısızsa nedeni yada nedenleri (hatalar)
        public List<string> Errors { get; set; }

        public static Result Success()
        {
            Result result = new Result();
            result.IsSuccess = true;

            return result;
        }

        public static Result Error(string message,int errorCode)
        {
            Result result = new Result();
            result.IsSuccess = false;
            result.ErrorCode = errorCode;
            result.Errors = new List<string> { message };

            return result;
        }

        public static Result Error(string message)
        {
            Result result = new Result();
            result.IsSuccess = false;
            result.Errors = new List<string> { message};

            return result;
        }
    }

    public class Result<T>
    {
        public T Data { get; set; }

        public bool IsSuccess { get; set; }


        public List<string> Errors { get; set; }
    }
}
