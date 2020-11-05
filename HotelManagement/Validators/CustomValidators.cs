using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Validators
{
    public static class CustomValidators
    {
        public static IRuleBuilderInitial<T, string> MatchPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Custom((name, context) => {

                if(name.Length < 9)
                {
                    context.AddFailure("The phone number is to short");
                }

                if (name.Length > 17)
                {
                    context.AddFailure("The phone number is to long");
                }

                name = name.Replace("+", "");
                name = name.Replace(" ", "");
                name = name.Replace("-", "");
                if (name.All(char.IsDigit) == false)
                {
                    context.AddFailure("The phone number contains invalid characters.");
                }
            });
        }

        public static IRuleBuilderInitial<T, string> MatchName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            
            return ruleBuilder.Custom((name, context) => {

                if(name == null)
                {
                    return;
                }

                name = name.Replace(" ", "");
                name = name.Replace("-", "");
                if( name.All(char.IsLetter) == false)
                {
                    context.AddFailure("The name contains invalid characters.");
                }
            });
        }
    }
}
