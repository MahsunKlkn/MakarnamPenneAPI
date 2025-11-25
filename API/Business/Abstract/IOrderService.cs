using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities.Dto;

namespace Business.Abstract
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetById(int id);
        List<Order> GetByKullaniciId(int kullaniciId);
        List<Order> GetByOrderStatus(string status);
        
        Order CreateOrder(OrderCreateDto orderDto);
        Order UpdateOrderStatus(int orderId, OrderUpdateStatusDto statusDto);
        Order CancelOrder(int orderId, OrderCancelDto cancelDto);
        Order MarkAsDelivered(int orderId);
        
        void DeleteOrder(int orderId);
    }
}
