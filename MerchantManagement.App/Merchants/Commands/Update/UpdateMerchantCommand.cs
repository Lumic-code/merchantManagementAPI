using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManagement.App.Merchants.Commands.Update
{
    public class UpdateMerchantCommand : IRequest<Unit>
    {
            public Guid Id { get; set; }
            public string BusinessName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string Status { get; set; } = "Pending";
            public string Country { get; set; } = string.Empty;
    }
}
