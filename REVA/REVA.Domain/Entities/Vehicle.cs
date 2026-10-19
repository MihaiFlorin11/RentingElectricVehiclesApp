using REVA.Domain.Enum;

namespace REVA.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        public DateTime RegisterDate { get; set; }

        // Foreign key către VehicleType.
        public int VehicleTypeId { get; set; }

        // Navigation property.
        public VehicleType VehicleType { get; set; } = null!;

        public VehicleStatus Status { get; set; } = VehicleStatus.Available;

        // Un vehicul poate avea mai multe închirieri în timp.
        public ICollection<Rental> Rentals { get; set; }
            = new List<Rental>();
    }
}
