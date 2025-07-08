using MediatR;
using MerchantManagement.Domain.Entities;
using MerchantManagement.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManagement.App.Merchants.Queries.List
{
    public class GetAllMerchantsQueryHandler : IRequestHandler<GetAllMerchantsQuery, List<Merchant>>
    {
        private readonly AppDbContext _context;

        public GetAllMerchantsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Merchant>> Handle(GetAllMerchantsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Merchants.ToListAsync(cancellationToken);
        }
    }
}
