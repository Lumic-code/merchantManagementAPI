using FluentValidation;
using MerchantManagement.App.Merchants.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManagement.App.Merchants.Validators
{
    public class CreateMerchantCommandValidator : AbstractValidator<CreateMerchantCommand>
    {
        public CreateMerchantCommandValidator()
        {
            RuleFor(x => x.BusinessName)
                .NotEmpty().WithMessage("Business Name is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.");

            RuleFor(x => x.Status)
                .Must(status => new[] { "Pending", "Active", "Inactive" }.Contains(status))
                .WithMessage("Invalid status. Allowed values: Pending, Active, Inactive.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.");
        }
    }
}
