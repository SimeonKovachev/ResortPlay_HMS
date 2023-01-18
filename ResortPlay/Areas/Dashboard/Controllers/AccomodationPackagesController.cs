using ResortPlay.Areas.Dashboard.ViewModels;
using ResortPlay.Entity;
using ResortPlay.ViewModels;
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
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();

        public ActionResult Index(string searchTerm, int? accomodationTypeId, int? page) //search by seartchterm and accomodation type by its id. Only gets int.  // The int page is for the pages each with 3 packages
        {
            //how much packages will show at a time
            int recordSize = 3;
            page = page ?? 1;

            AccomodationPackagesListingModel model = new AccomodationPackagesListingModel();
            //Word search
            model.SearchTerm = searchTerm;
            //Type search
            model.AccomodationTypeId = accomodationTypeId;

            model.AccomodationPackages = accomodationPackagesService.SearchAccomodationPackages(searchTerm, accomodationTypeId, page.Value, recordSize);
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();

            //The pager controller
            var totalRecords = accomodationPackagesService.SearchAccomodationPackagesCount(searchTerm, accomodationTypeId);
            model.Pager = new Pager(totalRecords, page, recordSize);
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
                model.AccomodationTypeId = accomodationPackage.AccomodationTypeId;
                model.Name = accomodationPackage.Name;
                model.NoOfRooms = accomodationPackage.NoOfRooms;
                model.FeePerNight = accomodationPackage.FeePerNight;

            }

            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();

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

                accomodationPackage.AccomodationTypeId= model.AccomodationTypeId;
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRooms = model.NoOfRooms;
                accomodationPackage.FeePerNight = model.FeePerNight;


                result = accomodationPackagesService.EditAccomodationPackage(accomodationPackage);

            }
            else //Create a record
            {

                AccomodationPackage accomodationPackage = new AccomodationPackage();

                accomodationPackage.AccomodationTypeId = model.AccomodationTypeId;
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