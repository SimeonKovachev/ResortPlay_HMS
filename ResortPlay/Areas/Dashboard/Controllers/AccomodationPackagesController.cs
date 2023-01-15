using ResortPlay.Areas.Dashboard.ViewModels;
using ResortPlay.Entity;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResortPlay.Areas.Dashboard.Controllers
{
    public class AccomodationPackagesController : Controller
    {
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        public ActionResult Index(string searchTerm)
        {
            AccomodationPackagesListingModel model = new AccomodationPackagesListingModel();

            model.SearchTerm = searchTerm;

            model.AccomodationPackages = accomodationPackagesService.SearchAccomodationPackages(searchTerm);

            return View(model);
        }

        //Here to Read from the model the chosen accomodation
        [HttpGet]
        public ActionResult Action(int? Id)
        {
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            if (Id.HasValue) // View the records
            {
                var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(Id.Value);

                model.Id = accomodationPackage.Id;
                model.Name = accomodationPackage.Name;
                model.NoOfRooms = accomodationPackage.NoOfRooms;
                model.FeePerNight = accomodationPackage.FeePerNight;

            }


            return PartialView("_Action", model);


        }
        //Here to make an action on the chosen accomodation
        [HttpPost]
        public JsonResult Action(AccomodationPackageActionModel model)
        {

            JsonResult json = new JsonResult();
            var result = false;

            if (model.Id > 0) //Edit a record
            {
                var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(model.Id);

                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRooms = model.NoOfRooms;
                accomodationPackage.FeePerNight = model.FeePerNight;


                result = accomodationPackagesService.EditAccomodationPackage(accomodationPackage);

            }
            else //Create a record
            {

                AccomodationPackage accomodationPackage = new AccomodationPackage();

                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRooms = model.NoOfRooms;
                accomodationPackage.FeePerNight = model.FeePerNight;

                result = accomodationPackagesService.SaveAccomodationPackage(accomodationPackage);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Type." };
            }


            return json;
        }
        //Here is to Delete the accomodation
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(Id);

            model.Id = accomodationPackage.Id;

            return PartialView("_Delete", model);

        }
        [HttpPost]
        public JsonResult Delete(AccomodationPackageActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;

            var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(model.Id);
            result = accomodationPackagesService.DeleteAccomodationPackage(accomodationPackage);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Type." };
            }
            return json;
        }

    }
}