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





        public void Dispose()
        {
            Band.DeleteAll();
            Venue.DeleteAll();
        }
    }
}
