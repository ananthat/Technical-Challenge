using MediatR;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Orders.Commands.UpdateOrder
{
    public class AddProductsToOrderCommandHandler : IRequestHandler<AddProductsToOrderCommand>
    {
        private readonly INorthwindDbContext _context;
        public AddProductsToOrderCommandHandler(INorthwindDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(AddProductsToOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Orders.FindAsync(request.OrderId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }
            var newOrderDetails = new List<OrderDetail>();
            foreach (var orderDetail in request.productDetails)
            {
                newOrderDetails.Add(new OrderDetail
                {
                    OrderId = request.OrderId,
                    ProductId = orderDetail.ProductId,
                    Discount = orderDetail.Discount,
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.UnitPrice,
                });
            }
            _context.OrderDetails.AddRange(newOrderDetails);

            _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
