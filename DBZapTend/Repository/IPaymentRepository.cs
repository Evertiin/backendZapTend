using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPayments();

        Task<Payment> GetPayment(int id);

        Task<Payment> CreatePayment(Payment payment);

        Task<Payment> UpdatePayment(Payment payment);

        Task<Payment> DeletePayment(int id);
    }
}
