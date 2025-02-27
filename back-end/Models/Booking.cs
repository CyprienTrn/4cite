using System;
using back_end.Enums;

namespace back_end.Models
{
    public class Booking
    {
        public required DateTime Date { get; set; }
        public required string CustomerEmail { get; set; }
        public bool Breakfast { get; set; }
        private RoomTypeEnum _roomType = RoomTypeEnum.Standard; // Définit "User" par défaut
        public RoomTypeEnum RoomType
        {
            get => _roomType;
            set
            {
                // Vérifie que la valeur donnée est bien un rôle valide
                if (Enum.IsDefined(typeof(RoomTypeEnum), value))
                {
                    _roomType = value;
                }
                else
                {
                    _roomType = RoomTypeEnum.Standard; // Valeur par défaut si invalide
                }
            }
        }

        public bool ValidateRequiredFields()
        {
            return !string.IsNullOrWhiteSpace(CustomerEmail);
        }
    }
}