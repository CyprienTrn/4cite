using System.Net.Mail;

namespace Back_end.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Mail { get; set; }
        public required string Pseudo { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }

        /**
         * Vérifie si l'email est valide
         */
        public static bool IsMailValid(string mail)
        {
            // Vérifie si l'email est vide ou null
            if (string.IsNullOrWhiteSpace(mail))
                return false;

            // Vérifie si l'email est valide
            try
            {
                var mailAddress = new MailAddress(mail);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}