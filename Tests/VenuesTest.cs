using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class VenueTest : IDisposable
    {
        public static Venue firstVenue = new Venue("The Pentagon");
        public static Venue secondVenue = new Venue("The Pentagon");
        public static Band firstBand = new Band("Tiny Rick");

        public void RecipeTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void VenueDatabaseEmpty()
        {
            //Arrange, act
            int result = Venue.GetAllVenues().Count;

            //Assert
            Assert.Equal(0,result);
        }

        [Fact]
        public void Test_EqualOverrideTrueForSameVenueName()
        {
            // Arragne, act, Assert
            Assert.Equal(firstVenue, secondVenue);
        }

        [Fact]
        public void Test_Save_SavesVenue()
        {
            // Arrange
            firstVenue.Save();

            // Act
            List<Venue> result = Venue.GetAllVenues();
            List<Venue> testList = new List<Venue>{firstVenue};

            // Assert
            Assert.Equal(testList, result);
        }



        public void Dispose()
        {
            Band.DeleteAll();
            Venue.DeleteAll();
        }
    }
}
