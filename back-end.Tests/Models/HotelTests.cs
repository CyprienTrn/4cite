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

        [Fact]
        /**
        * Test de la création d'un hôtel sans spécifier de liste d'images
        */
        public void TestCreateHotelWithoutPictures()
        {
            Hotel hotel = new()
            {
                Name = "Hotel California",
                Location = "California",
                Description = "A lovely place"
            };

            Assert.Equal("Hotel California", hotel.Name);
            Assert.Equal("California", hotel.Location);
            Assert.Equal("A lovely place", hotel.Description);
            Assert.Empty(hotel.PictureList);
        }

        [Fact]
        /**
        * Test de la modification de la liste d'images d'un hôtel
        */
        public void TestModifyHotelPictures()
        {
            Hotel hotel = new()
            {
                Name = "Hotel California",
                Location = "California",
                Description = "A lovely place",
                PictureList = new List<string> {ImageSampleForTest.base64Picture1}
            };

            hotel.PictureList.Add(ImageSampleForTest.base64Picture2);

            Assert.Equal(2, hotel.PictureList.Count);
            Assert.Contains(ImageSampleForTest.base64Picture1, hotel.PictureList);
            Assert.Contains(ImageSampleForTest.base64Picture2, hotel.PictureList);
        }

        [Fact]
        /**
        * Test de la création d'un hôtel avec un nom vide
        */
        public void TestCreateHotelWithEmptyName()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Hotel hotel = new()
                {
                    Name = "",
                    Location = "California",
                    Description = "A lovely place"
                    PictureList = new List<string> {ImageSampleForTest.base64Picture1, ImageSampleForTest.base64Picture2}
                };
            });
        }
    }
}