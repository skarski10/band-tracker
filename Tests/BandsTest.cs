using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class BandTest : IDisposable
    {
        public static Band firstBand = new Band("Tiny Rick");
        public static Band secondBand = new Band("Tiny Rick");
        public static Venue firstVenue = new Venue("The Pentagon");


        public BandTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void BandDatabaseEmpty()
        {
            //Arrange, act
            int result = Band.GetAllBands().Count;

            //Assert
            Assert.Equal(0,result);
        }

        [Fact]
        public void Test_EqualOverrideTrueForSameBandName()
        {
            // Arragne, act, Assert
            Assert.Equal(firstBand, secondBand);
        }

        [Fact]
        public void Test_SaveToDatabase()
        {
            // Arrange
            firstBand.Save();

            // act
            List<Band> result = Band.GetAllBands();
            List<Band> testList = new List<Band>{firstBand};

            // Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_SaveAssignsIdToObject()
        {
            // Arrange
            firstBand.Save();

            // Act
            Band testBand = Band.GetAllBands()[0];
            int result = firstBand.GetBandId();
            int testId = testBand.GetBandId();

            // Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindsBandInDatablase()
        {
            //Arrange
            firstBand.Save();
            //Act
            Band foundBand = Band.Find(firstBand.GetBandId());

            //Asswert
            Assert.Equal(firstBand, foundBand);
        }

        [Fact]
        public void Test_GetVenues_ReturnAllVenuesInBand()
        {
            // Arragne
            firstVenue.Save();
            firstBand.Save();

            // Act
            firstBand.AddVenue(firstVenue);
            List<Venue> savedVenue = firstBand.GetVenues();
            List<Venue> testList = new List<Venue> {firstVenue};

            // Assert
            Assert.Equal(testList, savedVenue);
        }






        public void Dispose()
        {
            Band.DeleteAll();
            Venue.DeleteAll();
        }
    }
}
