using MediatR;
using MerchantManagement.Domain.Entities;
using MerchantManagement.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManagement.App.Merchants.Commands.Create
{
    public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, Merchant>
    {

        private readonly AppDbContext _context;

        public CreateMerchantCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Merchant> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
        {
            var merchant = new Merchant
            {
                Id = Guid.NewGuid(),
                BusinessName = request.BusinessName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Status = request.Status,
                Country = request.Country,
                CreatedAt = DateTime.UtcNow
            };

            _context.Merchants.Add(merchant);
            await _context.SaveChangesAsync(cancellationToken);
            return merchant;
        }
    }
}
