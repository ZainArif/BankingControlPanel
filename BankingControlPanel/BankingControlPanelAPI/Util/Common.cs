using BankingControlPanelAPI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankingControlPanelAPI.Util
{
    public class Common
    {
        public static ObjectResult GetExceptionResponse(Exception ex)
        {
            Console.WriteLine(ex.ToString());

            var response = new ResponseDto()
            {
                IsSuccess = false,
                Message = "Operation failed due to technical error."
            };

            return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
