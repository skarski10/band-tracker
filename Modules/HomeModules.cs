using System.Collections.Generic;
using Nancy;
using System;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
    public class HomeModule: NancyModule
    {
        public HomeModule()
        {
            // Take you to the homepage
            Get["/"] = _ => {
                return View["index.cshtml"];
            };

            // Take you to the venues page
            Get["/venues"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                List<Venue> allVenues = Venue.GetAllVenues();
                model.Add("venue", allVenues);
                return View["venues.cshtml", model];
            };
            // Post for adding a new venue
            Post["/venues"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Venue newVenue = new Venue(Request.Form["venue"]);
                newVenue.Save();
                List<Venue> allVenues = Venue.GetAllVenues();
                model.Add("venue", allVenues);
                return View["venues.cshtml", model];
            };
            // Take you to an individual venue page
            Get["/venues/{id}/{name}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Venue selectedVenue = Venue.Find(parameters.id);
                List<Band> allBands = Band.GetAllBands();
                model.Add("venue", selectedVenue);
                model.Add("bands", allBands);
                return View["venue.cshtml", model];
            };
            // Post a new band to a venue
            Post["/venues/{id}/{name}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Band newBand = new Band(Request.Form["add-band"]);
                Venue currentVenue = Venue.Find(Request.Form["venue-id"]);
                newBand.Save();
                List<Venue> allVenues = Venue.GetAllVenues();
                currentVenue.AddBand(newBand);
                List<Band> allBands = Band.GetAllBands();
                model.Add("venue", currentVenue);
                model.Add("bands", allBands);
                return View["venue.cshtml", model];
            };

            // Take you to the bands page
            Get["/bands"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                List<Band> allBands = Band.GetAllBands();
                model.Add("bands", allBands);
                return View["bands.cshtml", model];
            };
            // Take you to an individual band page
            Get["/bands/{id}/{name}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Band selectedBand = Band.Find(parameters.id);
                List<Venue> allVenues = Venue.GetAllVenues();
                model.Add("band", selectedBand);
                model.Add("venues", allVenues);
                return View["band.cshtml", model];
            };

            // Take you to the page to edit a venue
            Get["/venues/{id}/{name}/edit"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);
                return View["edit_venue.cshtml", selectedVenue];
            };

            // Edit a venue
            Patch["/venue/{id}/updated"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);
                selectedVenue.Update(Request.Form["edit-venue"]);
                return View["venue_updated.cshtml"];
            };

            // take you to page to delete a venue
            Get["venue/{id}/{name}/delete"] = parameters => {
                Venue selectedVenue = Venue.Find(parameters.id);
                return View["delete_venue.cshtml", selectedVenue];
            };
            // Delete a venue
            Delete["/venue/{id}/{name}/deleted"] = parameters => {
                Venue specificVenue = Venue.Find(parameters.id);
                specificVenue.Delete();
                return View["deleted.cshtml"];
            };

            // take you to page to delete a band
            Get["band/{id}/delete"] = parameters => {
                Band selectedBand = Band.Find(parameters.id);
                return View["delete_band.cshtml", selectedBand];
            };
            // Delete a band
            Delete["/band/{id}/deleted"] = parameters => {
                Band specificBand = Band.Find(parameters.id);
                specificBand.Delete();
                return View["deleted.cshtml"];
            };
        }
    }
}
