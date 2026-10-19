using REVA.Domain.Enum;

namespace REVA.Domain.Entities
{
    public class Rental
    {
        public int Id { get; set; }

        // Foreign key către Vehicle.
        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; } = null!;

        // Foreign key către User.
        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public DateTime StartDateTime { get; set; }

        // Este null până când se termină cursa.
        public DateTime? EndDateTime { get; set; }

        public RentalStatus Status { get; set; } = RentalStatus.Active;

        // O închiriere poate avea încă zero facturi.
        public Invoice? Invoice { get; set; }
    }
}
