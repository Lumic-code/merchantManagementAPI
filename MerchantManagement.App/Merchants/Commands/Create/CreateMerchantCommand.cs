using MediatR;
using MerchantManagement.Domain.Entities;

namespace MerchantManagement.App.Merchants.Commands.Create
{
    public class CreateMerchantCommand : IRequest<Merchant>
    {
        public string BusinessName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public string Country { get; set; } = string.Empty;
    }
}
