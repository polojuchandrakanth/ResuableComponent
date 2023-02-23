using Feature.BusinessModel.ViewModel;
using Feature.Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.BusinessModel.Validators
{
    public class UserDetailsValidator : AbstractValidator<UserProfileViewModel>
    {

        public UserDetailsValidator()
        {
            // Check UserId is not null, empty and is between 1 and 250 characters
            RuleFor(user => user.UserId).NotNull().NotEmpty().Length(1, 250);

            // Check name is not null, empty and is between 1 and 250 characters
            RuleFor(user => user.Firstname).NotNull().NotEmpty().Length(1, 250);
            RuleFor(user => user.LastName).NotNull().NotEmpty().Length(1, 250);

            // Validate Email with a custom error message
            RuleFor(user => user.Email).NotEmpty().WithMessage("Please add a Email");

            // Validate Password with a custom error message
            RuleFor(user => user.Password).NotEmpty().WithMessage("Please add a Password");


        }
    }
}
