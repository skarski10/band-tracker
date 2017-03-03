using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
    public class Band
    {
        private int _id;
        private string _name;

        public Band(string name, int id = 0)
        {
            _id = id;
            _name = name;
        }

        public static List<Band> GetAllBands()
        {
            List<Band> allBands = new List<Band>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int bandId = rdr.GetInt32(0);
                string bandName = rdr.GetString(1);
                Band newBand = new Band(bandName, bandId);
                allBands.Add(newBand);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return allBands;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@BandName);", conn);
            SqlParameter nameParameter = new SqlParameter("@BandName", this.GetBandName());
            cmd.Parameters.Add(nameParameter);

            SqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
        }

        public static Band Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);
            SqlParameter BandIdParameter = new SqlParameter("@BandId", id.ToString());
            cmd.Parameters.Add(BandIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();
            int foundBandId = 0;
            string foundBandName = null;

            while(rdr.Read())
            {
                foundBandId = rdr.GetInt32(0);
                foundBandName = rdr.GetString(1);
            }
            Band foundBand = new Band(foundBandName, foundBandId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundBand;
        }

        public void AddVenue(Venue newVenue)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (venue_id, band_id) VALUES (@VenueId, @BandId);", conn);
            SqlParameter venueIdParameter = new SqlParameter("@VenueId", newVenue.GetVenueId());
            SqlParameter bandIdParameter = new SqlParameter("@BandId", this.GetBandId());
            cmd.Parameters.Add(venueIdParameter);
            cmd.Parameters.Add(bandIdParameter);

            cmd.ExecuteNonQuery();
            if(conn != null)
            {
                conn.Close();
            }
        }

        public List<Venue> GetVenues()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (bands_venues.venue_id = venues.id) WHERE bands.id = @BandId;", conn);
            SqlParameter venueBand = new SqlParameter("@BandId", this.GetBandId().ToString());
            cmd.Parameters.Add(venueBand);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Venue> newList = new List<Venue>{};
            while(rdr.Read())
            {
                int venueId = rdr.GetInt32(0);
                string venueName = rdr.GetString(1);

                Venue newVenue = new Venue(venueName, venueId);
                newList.Add(newVenue);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
             return newList;
        }








        public int GetBandId()
        {
            return _id;
        }
        public string GetBandName()
        {
            return _name;
        }

        public override bool Equals(System.Object otherBand)
        {
            if (!(otherBand is Band))
            {
                return false;
            }
            else
            {
                Band newBand = (Band) otherBand;
                bool nameEquality = this.GetBandName() == newBand.GetBandName();
                return (nameEquality);
            }
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
