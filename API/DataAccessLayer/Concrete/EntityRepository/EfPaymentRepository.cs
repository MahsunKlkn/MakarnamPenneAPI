using DataAccessLayer.Abstarct;
using DataAccessLayer.Concrete.EntityRepository.Context;
using DataAccessLayer.Repositories;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityRepository
{
    public class EfPaymentRepository : GenericRepository<Payment>, IPaymentDal
    {
        public EfPaymentRepository(Context.Context context) : base(context)
        {
        }

        public Payment? GetByConversationId(string conversationId)
        {
            return _context.Payments.FirstOrDefault(p => p.ConversationId == conversationId);
        }

        public Payment? GetByPaymentId(string paymentId)
        {
            return _context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
        }

        public List<Payment> GetByOrderId(int orderId)
        {
            return _context.Payments.Where(p => p.OrderId == orderId).ToList();
        }
    }
}
