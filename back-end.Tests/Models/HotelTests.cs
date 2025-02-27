using back_end.Models;
using back_end.Tests.Models;

namespace back_end.Tests.Models
{
    public class HotelTests
    {
        /**
        * Test de la création d'un hôtel avec des valeurs valides
        */
        public void TestCreateHotel()
        {
            Hotel hotel = new()
            {
                Name = "Hotel California",
                Location = "California",
                Description = "A lovely place",
                PictureList = new List<string> {ImageSampleForTest.base64Picture1, ImageSampleForTest.base64Picture2}
            };

            Assert.Equal("Hotel California", hotel.Name);
            Assert.Equal("California", hotel.Location);
            Assert.Equal("A lovely place", hotel.Description);
            Assert.Equal(2, hotel.PictureList.Count);
            Assert.Contains(base64Picture1, hotel.PictureList);
            Assert.Contains(base64Picture2, hotel.PictureList);
        }
        

    }
}