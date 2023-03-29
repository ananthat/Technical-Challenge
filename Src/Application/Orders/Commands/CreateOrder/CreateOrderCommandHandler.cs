using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly INorthwindDbContext _context;

        public CreateOrderCommandHandler(INorthwindDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newOrderDetails = new List<OrderDetail>();
            foreach (var orderDetail in request.orderDetails)
            {
                newOrderDetails.Add(new OrderDetail
                {
                    ProductId = orderDetail.ProductId,
                    Discount = orderDetail.Discount,
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.UnitPrice,
                });
            }

            var entity = new Order
            {
                CustomerId = request.CustomerId,
                EmployeeId = request.EmployeeId,
                OrderDate = request.OrderDate,
                ShipVia = request.ShipVia,
                Freight = request.Freight,
                ShipName = request.ShipName,
                ShipAddress = request.ShipAddress,
                ShipCity = request.ShipCity,
                ShipRegion = request.ShipRegion,
                ShipPostalCode = request.ShipPostalCode,
                ShipCountry = request.ShipCountry,
                OrderDetails = newOrderDetails
            };
            _context.Orders.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.OrderId;
        }
    }
}
