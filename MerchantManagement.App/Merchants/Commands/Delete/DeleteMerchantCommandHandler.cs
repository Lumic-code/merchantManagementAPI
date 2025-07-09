using MediatR;
using MerchantManagement.Infra;

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
            if (merchant == null)
            {
                throw new KeyNotFoundException("Merchant not found.");
            }

            _context.Merchants.Remove(merchant);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        } 
    }
}
