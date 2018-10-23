using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPS.Data;
using TPS.Models;

namespace TPS.Controllers
{
    public class LocationController : Controller
    {
        private LocationRepository _locationRepository = null;

        public LocationController()
        {
            _locationRepository = new LocationRepository();
        }

        public ActionResult Index()
        {
            List<Location> locations = GetLocations();
            return View(locations);
        }

        public ActionResult Add()
        {
            var location = new Location();

            return View(location);
        }

        [HttpPost]
        public ActionResult Add(Location location)
        {
            if (ModelState.IsValid)
            {
                AddLocation(location);

                TempData["Message"] = "Your Staffing Request was succsesfully added!";

                return RedirectToAction("Index");
            }

            return View(location);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Location location = GetLocation((int)id);

            if (location == null)
            {
                return HttpNotFound();
            }

            return View(location);
        }

        [HttpPost]
        public ActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                UpdateLocation(location);
                TempData["Message"] = "The Location was successfully updated!";

                return RedirectToAction("Index");
            }

            return View(location);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Location location = _locationRepository.GetLocation((int)id);

            if (location == null)
            {
                return HttpNotFound();
            }

            return View(location);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _locationRepository.DeleteLocation(id);
            TempData["Message"] = "The Location was Succesfully deleted!";
            return RedirectToAction("Index");
        }

        private List<Location> GetLocations() => _locationRepository.GetLocations();

        private Location GetLocation(int id) => _locationRepository.GetLocation(id);

        private void AddLocation(Location location) => _locationRepository.AddLocation(location);

        private void UpdateLocation(Location location) => _locationRepository.UpdateLocation(location);

    }
}