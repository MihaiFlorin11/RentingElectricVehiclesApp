using Domain.Enum;

namespace Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        // Foreign key către Rental.
        public int RentalId { get; set; }

        public Rental Rental { get; set; } = null!;

        public decimal TotalPrice { get; set; }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;

        public DateTime IssuedAt { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}
