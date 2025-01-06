using DBZapTend.Models;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Repository
{
    public class PaymentRepository : IPaymentRepository
    {

        private readonly DbzapContext _context;

        public PaymentRepository(DbzapContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Payment>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }
        public async Task<Payment> GetPayment(int id)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.IdPayments == id);
        }
        public async Task<Payment> CreatePayment(Payment payment)
        {
            if (payment is null)
                throw new ArgumentNullException(nameof(payment));


            _context.Payments.Add(payment);


            await _context.SaveChangesAsync();


            return payment;
        }
        public async Task<Payment> UpdatePayment(Payment payment)
        {
            if (payment is null)
                throw new ArgumentNullException(nameof(payment));

            _context.Payments.Entry(payment).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return payment;
        }
        public async Task<Payment> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment is null)
                throw new ArgumentNullException(nameof(payment));


            _context.Payments.Remove(payment);

            await _context.SaveChangesAsync();

            return payment;
        }
    }
}
