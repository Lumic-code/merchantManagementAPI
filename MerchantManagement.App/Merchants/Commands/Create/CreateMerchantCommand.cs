﻿using MediatR;
using MerchantManagement.App.Merchants.DTOs;

namespace MerchantManagement.App.Merchants.Commands.Create
{
    public class CreateMerchantCommand : IRequest<MerchantDto>
    {
        public string BusinessName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
