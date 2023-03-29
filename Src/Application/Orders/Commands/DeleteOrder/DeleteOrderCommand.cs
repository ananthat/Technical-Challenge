using MediatR;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
        {
            private readonly INorthwindDbContext _context;
            private readonly IUserManager _userManager;
            private readonly ICurrentUserService _currentUser;

            public DeleteOrderCommandHandler(INorthwindDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                var orderDetailsEntity = _context.OrderDetails.Where(o => o.OrderId == request.Id).ToArray();
                var entity = await _context.Orders.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Order), request.Id);
                }

                _context.OrderDetails.RemoveRange(orderDetailsEntity);
                _context.Orders.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
