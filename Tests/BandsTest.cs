using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class BandTest
    {
        public static Band firstBand = new Band("Tiny Rick");
        public static Band secondBand = new Band("Tiny Rick");
        // public static Venue firstVenue = new Venue("The Pentagon");


        public void RecipeTest()
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
    }
}
