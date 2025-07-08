using MediatR;
using MerchantManagement.Domain.Entities;
using MerchantManagement.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManagement.App.Merchants.Queries.Get
{
    public class GetMerchantByIdQueryHandler : IRequestHandler<GetMerchantByIdQuery, Merchant?>
    {
        private readonly AppDbContext _context;

        public GetMerchantByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Merchant?> Handle(GetMerchantByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Merchants.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
