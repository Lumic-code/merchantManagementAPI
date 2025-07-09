namespace MerchantManagement.API.Requests
{
    public class UpdateMerchantRequest
    {
        public string BusinessName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
