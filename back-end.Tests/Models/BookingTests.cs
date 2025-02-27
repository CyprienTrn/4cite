using back_end.Models;
using back_end.Enums;

namespace back_end.Tests.Models
{
    public class BookingTests
    {
        /**
        * Test de la création d'une réservation avec des valeurs valides
        */
        [Fact]
        public void TestCreateBooking()
        {
            Booking booking = new()
            {
                Date = new DateTime(2025, 2, 27),
                CustomerEmail = "customer@example.com",
                Breakfast = true,
                RoomType = RoomTypeEnum.Deluxe
            };

            Assert.Equal(new DateTime(2025, 2, 27), booking.Date);
            Assert.Equal("customer@example.com", booking.CustomerEmail);
            Assert.True(booking.Breakfast);
            Assert.Equal(RoomTypeEnum.Deluxe, booking.RoomType);
        }

        /**
        * Test de la création d'une réservation sans spécifier de petit-déjeuner
        */
        [Fact]
        public void TestCreateBookingWithoutBreakfast()
        {
            Booking booking = new()
            {
                Date = new DateTime(2025, 2, 27),
                CustomerEmail = "customer@example.com",
                RoomType = RoomTypeEnum.Deluxe
            };

            Assert.Equal(new DateTime(2025, 2, 27), booking.Date);
            Assert.Equal("customer@example.com", booking.CustomerEmail);
            Assert.Equal(RoomTypeEnum.Deluxe, booking.RoomType);
            Assert.False(booking.Breakfast);
        }
    }
}