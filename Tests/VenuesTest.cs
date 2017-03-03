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

        public VenueTest()
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
        public void Test_SaveAssignsIdToObject()
        {
            // Arrange
            firstVenue.Save();

            // Act
            Venue testVenue = Venue.GetAllVenues()[0];
            int result = firstVenue.GetVenueId();
            int testId = testVenue.GetVenueId();

            // Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindsVenueInDatablase()
        {
            //Arrange
            firstVenue.Save();
            //Act
            Venue foundVenue = Venue.Find(firstVenue.GetVenueId());

            //Asswert
            Assert.Equal(firstVenue, foundVenue);
        }

        [Fact]
        public void Test_GetBands_ReturnAllBandsInVenue()
        {
            // Arragne
            firstVenue.Save();
            firstBand.Save();

            // Act
            firstVenue.AddBand(firstBand);
            List<Band> savedBand = firstVenue.GetBands();
            List<Band> testList = new List<Band> {firstBand};

            // Assert
            Assert.Equal(testList, savedBand);
        }






        public void Dispose()
        {
            Venue.DeleteAll();
            Band.DeleteAll();
        }
    }
}
