using back_end.Models;
using back_end.Tests.Models;

namespace back_end.Tests.Models
{
    public class HotelTests
    {
        /**
        * Test de la création d'un hôtel avec des valeurs valides
        */
        [Fact]
        public void TestCreateHotel()
        {
            Hotel hotel = new()
            {
                Id = 1,
                Name = "Hotel California",
                Location = "California",
                Description = "A lovely place",
                PictureList = new List<string> {ImageSampleForTest.base64Picture1, ImageSampleForTest.base64Picture2}
            };

            Assert.Equal("Hotel California", hotel.Name);
            Assert.Equal("California", hotel.Location);
            Assert.Equal("A lovely place", hotel.Description);
            Assert.Equal(2, hotel.PictureList.Count);
            Assert.Contains(ImageSampleForTest.base64Picture1, hotel.PictureList);
            Assert.Contains(ImageSampleForTest.base64Picture2, hotel.PictureList);
        }

        [Fact]
        /**
        * Test de la création d'un hôtel sans spécifier de liste d'images
        */
        public void TestCreateHotelWithoutPictures()
        {
            Hotel hotel = new()
            {
                Id = 1,
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
                Id = 1,
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
            Hotel hotel = new()
                {
                    Id = 1,
                    Name = "",
                    Location = "California",
                    Description = "A lovely place",
                    PictureList = new List<string> {ImageSampleForTest.base64Picture1, ImageSampleForTest.base64Picture2}
                };
            Assert.False(hotel.Validate());            
        }

        /**
        * Test de la création d'un hôtel avec une localisation vide
        */
        [Fact]
        public void TestCreateHotelWithEmptyLocation()
        {
            Hotel hotel = new()
            {
                Id = 1,
                Name = "Hotel California",
                Location = "",
                Description = "A lovely place",
                PictureList = new List<string> {ImageSampleForTest.base64Picture1, ImageSampleForTest.base64Picture2}
            };
            Assert.False(hotel.Validate());
        }
    }
}