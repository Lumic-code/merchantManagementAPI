using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantManagement.App.Merchants.Commands.Delete
{
    public class DeleteMerchantCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
