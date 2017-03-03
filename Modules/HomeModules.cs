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

            Post["/venues"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Venue newVenue = new Venue(Request.Form["venue"]);
                newVenue.Save();
                List<Venue> allVenues = Venue.GetAllVenues();
                model.Add("venue", allVenues);
                return View["venues.cshtml", model];
            };

            Get["/venues/{id}/{name}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Venue selectedVenue = Venue.Find(parameters.id);
                List<Band> allBands = Band.GetAllBands();
                model.Add("venue", selectedVenue);
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

            Get["/bands/{id}/{name}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Band selectedBand = Band.Find(parameters.id);
                List<Venue> allVenues = Venue.GetAllVenues();
                model.Add("band", selectedBand);
                model.Add("venues", allVenues);
                return View["band.cshtml", model];
            };
        }
    }
}
