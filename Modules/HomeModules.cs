using Nancy;
using HairSalonApp;
using System.Collections.Generic;

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
            Get["/"] = _ => {
                return View["venues.cshtml"];
            };

            // Take you to the bands page
            Get["/"] = _ => {
                return View["bands.cshtml"];
            };
        }
    }
}
