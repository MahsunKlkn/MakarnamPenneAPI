using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstarct
{
    public interface IPaymentDal : IGenericDal<Payment>
    {
        Payment? GetByConversationId(string conversationId);
        Payment? GetByPaymentId(string paymentId);
        List<Payment> GetByOrderId(int orderId);
    }
}
