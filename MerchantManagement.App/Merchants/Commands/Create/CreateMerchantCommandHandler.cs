using MediatR;
using MerchantManagement.App.Merchants.DTOs;
using MerchantManagement.Domain.Entities;
using MerchantManagement.Infra;

namespace MerchantManagement.App.Merchants.Commands.Create
{
    public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, MerchantDto>
    {

        private readonly AppDbContext _context;

        public CreateMerchantCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MerchantDto> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
        {
       
            var merchant = new Merchant
            {
                Id = Guid.NewGuid(),
                BusinessName = request.BusinessName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Status = Enum.Parse<MerchantStatus>(request.Status, true),
                Country = request.Country,
                CreatedAt = DateTime.UtcNow
            };

            _context.Merchants.Add(merchant);
            await _context.SaveChangesAsync(cancellationToken);

            return new MerchantDto
            {
                Id = merchant.Id,
                BusinessName = merchant.BusinessName,
                Email = merchant.Email,
                PhoneNumber = merchant.PhoneNumber,
                Status = merchant.Status.ToString(),
                Country = merchant.Country,
                CreatedAt = merchant.CreatedAt
            };
        }
    }
}
