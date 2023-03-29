using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Orders.Queries.GetOrderList
{
    public class GetOrderListQuery : IRequest<OrderListVm>
    {
        public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, OrderListVm>
        {
            private readonly INorthwindDbContext _context;
            private readonly IMapper _mapper;
            public GetOrderListQueryHandler(INorthwindDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<OrderListVm> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
            {
                var orders = await _context.Orders
                    .ProjectTo<OrderLookupDto>(_mapper.ConfigurationProvider)
                    .OrderBy(e => e.OrderId)
                    .ToListAsync(cancellationToken);

                var vm = new OrderListVm
                {
                    Orders = orders
                };

                return vm;
            }
        }
    }
}
