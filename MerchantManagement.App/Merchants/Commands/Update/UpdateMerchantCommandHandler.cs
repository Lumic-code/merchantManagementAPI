using MediatR;
using MerchantManagement.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManagement.App.Merchants.Commands.Update
{
    public class UpdateMerchantCommandHandler : IRequestHandler<UpdateMerchantCommand, Unit>
    {
        private readonly AppDbContext _context;

        public UpdateMerchantCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMerchantCommand request, CancellationToken cancellationToken)
        {
            var merchant = await _context.Merchants.FindAsync(new object[] { request.Id }, cancellationToken);
            if (merchant == null) return Unit.Value;

            merchant.BusinessName = request.BusinessName;
            merchant.Email = request.Email;
            merchant.PhoneNumber = request.PhoneNumber;
            merchant.Status = request.Status;
            merchant.Country = request.Country;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
