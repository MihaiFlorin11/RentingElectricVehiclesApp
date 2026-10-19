namespace REVA.Domain.Entities
{
    public class VehicleType
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public decimal PricePerMinute { get; set; }

        // Un tip poate fi asociat cu mai multe vehicule.
        public ICollection<Vehicle> Vehicles { get; set; }
            = new List<Vehicle>();
    }
}
