using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Application.Orders.Queries.GetOrderList
{
    public class OrderListVm
    {
        public IList<OrderLookupDto> Orders { get; set; }
    }
}
