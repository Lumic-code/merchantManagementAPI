using FluentValidation;
using MerchantManagement.API.Requests;
using MerchantManagement.Domain.Entities;

namespace MerchantManagement.API.Validators
{
    public class UpdateMerchantRequestValidator : AbstractValidator<UpdateMerchantRequest>
    {
        public UpdateMerchantRequestValidator()
        {
            RuleFor(x => x.BusinessName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.Status)
                .Must(status => Enum.TryParse<MerchantStatus>(status, true, out _))
                .WithMessage("Invalid status. Allowed values: Pending, Active, Inactive.");
        }
    }
}
