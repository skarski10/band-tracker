using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
    public class Venue
    {
        private int _id;
        private string _name;

        public Venue(string name, int id = 0)
        {
            _id = id;
            _name = name;
        }

        public static List<Venue> GetAllVenues()
        {
            List<Venue> allVenues = new List<Venue>{};
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                int venueId = rdr.GetInt32(0);
                string venueName = rdr.GetString(1);
                Venue newVenue = new Venue(venueName, venueId);
                allVenues.Add(newVenue);
            }

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return allVenues;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@VenueName);", conn);
            SqlParameter nameParameter = new SqlParameter("@VenueName", this.GetVenueName());
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

        public static Venue Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
            SqlParameter VenueIdParameter = new SqlParameter("@VenueId", id.ToString());
            cmd.Parameters.Add(VenueIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();
            int foundVenueId = 0;
            string foundVenueName = null;

            while(rdr.Read())
            {
                foundVenueId = rdr.GetInt32(0);
                foundVenueName = rdr.GetString(1);
            }
            Venue foundVenue = new Venue(foundVenueName, foundVenueId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundVenue;
        }

        public void AddBand(Band newBand)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);
            SqlParameter bandIdParameter = new SqlParameter("@BandId", newBand.GetBandId());
            SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetVenueId());
            cmd.Parameters.Add(bandIdParameter);
            cmd.Parameters.Add(venueIdParameter);

            cmd.ExecuteNonQuery();
            if(conn != null)
            {
                conn.Close();
            }
        }

        public List<Band> GetBands()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands_venues.band_id = bands.id) WHERE venues.id = @VenueId;", conn);
            SqlParameter bandVenue = new SqlParameter("@VenueId", this.GetVenueId().ToString());
            cmd.Parameters.Add(bandVenue);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Band> newList = new List<Band>{};
            while(rdr.Read())
            {
                int bandId = rdr.GetInt32(0);
                string bandName = rdr.GetString(1);

                Band newBand = new Band(bandName, bandId);
                newList.Add(newBand);
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

        public void Update(string newName)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewName OUTPUT INSERTED.name WHERE id = @VenueId;", conn);

            SqlParameter newNameParameter = new SqlParameter("@NewName", newName);
            cmd.Parameters.Add(newNameParameter);

            SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetVenueId());
            cmd.Parameters.Add(venueIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                this._name = rdr.GetString(0);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id=@VenueId", conn);
            SqlParameter venueIdParameter = new SqlParameter("@VenueId", this.GetVenueId());
            cmd.Parameters.Add(venueIdParameter);
            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }







        public int GetVenueId()
        {
            return _id;
        }
        public string GetVenueName()
        {
            return _name;
        }

        public override bool Equals(System.Object otherVenue)
        {
            if (!(otherVenue is Venue))
            {
                return false;
            }
            else
            {
                Venue newVenue = (Venue) otherVenue;
                bool nameEquality = this.GetVenueName() == newVenue.GetVenueName();
                return (nameEquality);
            }
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
