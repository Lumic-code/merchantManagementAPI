using MediatR;
using MerchantManagement.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManagement.App.Merchants.Commands.Delete
{
    public class DeleteMerchantCommandHandler : IRequestHandler<DeleteMerchantCommand, Unit>
    {
        private readonly AppDbContext _context;

        public DeleteMerchantCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMerchantCommand request, CancellationToken cancellationToken)
        {
            var merchant = await _context.Merchants.FindAsync(new object[] { request.Id }, cancellationToken);
            if (merchant == null) return Unit.Value;

            _context.Merchants.Remove(merchant);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        } 
    }
}
