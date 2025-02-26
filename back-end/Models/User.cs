using System.Net.Mail;
using System.Text.RegularExpressions;
using back_end.Enums;

namespace back_end.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Mail { get; set; }
        public required string Pseudo { get; set; }
        public required string Password { get; set; }
        private RolesEnum _role = RolesEnum.User; // Définit "User" par défaut

        public RolesEnum Role
        {
            get => _role;
            set
            {
                // Vérifie que la valeur donnée est bien un rôle valide
                if (Enum.IsDefined(typeof(RolesEnum), value))
                {
                    _role = value;
                }
                else
                {
                    _role = RolesEnum.User; // Valeur par défaut si invalide
                }
            }
        }

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

                // Vérifie que l'email respecte un format correct avec un TLD
                string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(mail, emailRegex);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}