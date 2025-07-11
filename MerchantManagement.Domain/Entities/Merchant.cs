﻿namespace MerchantManagement.Domain.Entities
{
    public class Merchant
    {
        public Guid Id { get; set; }
        public string BusinessName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public MerchantStatus Status { get; set; }
        public string Country { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public enum MerchantStatus
    {
        Pending,
        Active,
        Inactive
    }
}
