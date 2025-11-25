using Business.Abstract;
using DataAccessLayer.Abstarct;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public List<Order> GetAllOrders()
        {
            return _orderDal.GetListAll();
        }

        public Order GetById(int id)
        {
            return _orderDal.GetById(id);
        }

        public List<Order> GetByKullaniciId(int kullaniciId)
        {
            return _orderDal.GetByKullaniciId(kullaniciId);
        }

        public List<Order> GetByOrderStatus(string status)
        {
            return _orderDal.GetByOrderStatus(status);
        }

        public Order CreateOrder(OrderCreateDto orderDto)
        {
            var order = new Order
            {
                KullaniciId = orderDto.KullaniciId,
                OrderDate = DateTime.UtcNow,
                SubTotal = orderDto.SubTotal,
                ShippingCost = orderDto.ShippingCost,
                TotalAmount = orderDto.TotalAmount,
                OrderStatus = "Pending",
                PaymentStatus = "Pending",
                
                ShippingContactName = orderDto.ShippingContactName,
                ShippingPhone = orderDto.ShippingPhone,
                ShippingAddress = orderDto.ShippingAddress,
                ShippingCity = orderDto.ShippingCity,
                ShippingDistrict = orderDto.ShippingDistrict,
                ShippingZipCode = orderDto.ShippingZipCode,
                
                BillingContactName = orderDto.BillingContactName,
                BillingPhone = orderDto.BillingPhone,
                BillingAddress = orderDto.BillingAddress,
                BillingCity = orderDto.BillingCity,
                BillingDistrict = orderDto.BillingDistrict,
                BillingZipCode = orderDto.BillingZipCode,
                
                OrderItems = JsonSerializer.Serialize(orderDto.OrderItems),
                CustomerNote = orderDto.CustomerNote,
                
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            _orderDal.Insert(order);
            return order;
        }

        public Order UpdateOrderStatus(int orderId, OrderUpdateStatusDto statusDto)
        {
            var order = _orderDal.GetById(orderId);
            if (order == null)
                throw new Exception("Sipariş bulunamadı");

            order.OrderStatus = statusDto.OrderStatus;
            order.AdminNote = statusDto.AdminNote;
            order.CargoCompany = statusDto.CargoCompany;
            order.TrackingNumber = statusDto.TrackingNumber;
            order.DateUpdated = DateTime.UtcNow;

            // Durum değişikliklerine göre ek işlemler
            if (statusDto.OrderStatus == "Delivered")
            {
                order.DeliveredAt = DateTime.UtcNow;
            }

            _orderDal.Update(order);
            return order;
        }

        public Order CancelOrder(int orderId, OrderCancelDto cancelDto)
        {
            var order = _orderDal.GetById(orderId);
            if (order == null)
                throw new Exception("Sipariş bulunamadı");

            if (order.OrderStatus == "Delivered")
                throw new Exception("Teslim edilmiş sipariş iptal edilemez");

            order.OrderStatus = "Cancelled";
            order.CancellationReason = cancelDto.CancellationReason;
            order.CancelledAt = DateTime.UtcNow;
            order.DateUpdated = DateTime.UtcNow;

            _orderDal.Update(order);
            return order;
        }

        public Order MarkAsDelivered(int orderId)
        {
            var order = _orderDal.GetById(orderId);
            if (order == null)
                throw new Exception("Sipariş bulunamadı");

            order.OrderStatus = "Delivered";
            order.DeliveredAt = DateTime.UtcNow;
            order.DateUpdated = DateTime.UtcNow;

            _orderDal.Update(order);
            return order;
        }

        public void DeleteOrder(int orderId)
        {
            var order = _orderDal.GetById(orderId);
            if (order != null)
            {
                _orderDal.Delete(order);
            }
        }
    }
}
