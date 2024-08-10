using PhoneNumbers;
using System.ComponentModel.DataAnnotations;

namespace BankingControlPanelAPI.Util
{
    public class MobileNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();

                var parsedPhoneNumber = phoneNumberUtil.Parse(value.ToString(), null);

                var isValidNumber = phoneNumberUtil.IsValidNumber(parsedPhoneNumber);

                if (!isValidNumber)
                    return new ValidationResult(GetErrorMessage());

                return ValidationResult.Success;
            }
            catch (NumberParseException npex)
            {
                Console.WriteLine(npex);

                return new ValidationResult(GetErrorMessage(npex.ErrorType.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new ValidationResult(GetErrorMessage());
            }
        }

        private string GetErrorMessage(string errorCode = "")
        {
            string message = string.Empty;
            
            switch (errorCode)
            {
                case "INVALID_COUNTRY_CODE":
                    message = "Phone number must be with valid country code.";
                    break;
                default:
                    message = "Invalid phone number."; 
                    break;
            }

            return message;
        }
    }
}
