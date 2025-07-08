using MediatR;
using MerchantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManagement.App.Merchants.Queries.Get
{
    public class GetMerchantByIdQuery : IRequest<Merchant?>
    {
        public Guid Id { get; set; }
    }
}
