using MediatR;
using System.Collections.Generic;

namespace Northwind.Application.Orders.Commands.UpdateOrder
{
    public class AddProductsToOrderCommand: IRequest
    {
        public int OrderId { get; set; }

        public IEnumerable<ProductDetailRequest> productDetails { get; set; }
    }

    public class ProductDetailRequest
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
    }
}
