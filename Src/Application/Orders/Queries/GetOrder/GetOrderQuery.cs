using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Orders.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<OrderVm>
    {
        public int Id { get; set; }
        public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderVm>
        {
            private readonly INorthwindDbContext _context;
            private readonly IMapper _mapper;

            public GetOrderQueryHandler(INorthwindDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<OrderVm> Handle(GetOrderQuery request, CancellationToken cancellationToken)
            {
                //var vm = await _context.Orders
                //    .Where(e => e.OrderId == request.Id)
                //    .ProjectTo<OrderVm>(_mapper.ConfigurationProvider)
                //    .SingleOrDefaultAsync(cancellationToken);
                //return vm;

                var entity = await _context.Orders
                .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Customer), request.Id);
                }

                return _mapper.Map<OrderVm>(entity);
            }

            
        }
    }
}
