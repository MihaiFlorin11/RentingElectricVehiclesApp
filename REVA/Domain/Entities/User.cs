using Domain.Enum;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // Stocăm hash-ul parolei, nu parola în clar.
        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.User;

        // Un utilizator poate avea mai multe închirieri.
        public ICollection<Rental> Rentals { get; set; }
            = new List<Rental>();
    }
}
