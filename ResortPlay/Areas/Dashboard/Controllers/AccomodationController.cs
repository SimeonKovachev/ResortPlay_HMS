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
    public class AccomodationController : Controller
    {
        AccomodationsService accomodationsService = new AccomodationsService();
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();

        public ActionResult Index(string searchTerm, int? accomodationPackageId, int? page) //search by seartchterm and accomodation package by its id. Only gets int.  // The int page is for the pages each with 3 packages
        {
            //how much accomodations will show at a time
            int recordSize = 3;
            page = page ?? 1;

            AccomodationListingModel model = new AccomodationListingModel();
            //Word search
            model.SearchTerm = searchTerm;
            //Type search
            model.AccomodationPackageId = accomodationPackageId;

            model.Accomodations = accomodationsService.SearchAccomodations(searchTerm, accomodationPackageId, page.Value, recordSize);
            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();

            //The pager controller
            var totalRecords = accomodationsService.SearchAccomodationsCount(searchTerm, accomodationPackageId);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }

        //Here to Read from the model the chosen accomodation
        [HttpGet]
        public ActionResult Action(int? Id)
        {
            AccomodationActionModel model = new AccomodationActionModel();

            if (Id.HasValue) // View the records
            {
                var accomodation = accomodationsService.GetAccomodationById(Id.Value);

                model.Id = accomodation.Id;
                model.AccomodationPackageId = accomodation.AccomodationPackageId;
                model.Name = accomodation.Name;
                model.Description = accomodation.Description;
            }

            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();

            return PartialView("_Action", model);
        }
        //Here to make an action on the chosen accomodation
        [HttpPost]
        public JsonResult Action(AccomodationActionModel model)
        {

            JsonResult json = new JsonResult();
            var result = false;

            if (model.Id > 0) //Edit a record
            {
                var accomodation = accomodationsService.GetAccomodationById(model.Id);

                accomodation.AccomodationPackageId = model.AccomodationPackageId;
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;

                result = accomodationsService.EditAccomodation(accomodation);

            }
            else //Create a record
            {

                Accomodation accomodation = new Accomodation();

                accomodation.AccomodationPackageId = model.AccomodationPackageId;
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;

                result = accomodationsService.SaveAccomodation(accomodation);
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
            AccomodationActionModel model = new AccomodationActionModel();

            var accomodation = accomodationsService.GetAccomodationById(Id);

            model.Id = accomodation.Id;

            return PartialView("_Delete", model);

        }
        [HttpPost]
        public JsonResult Delete(AccomodationActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;

            var accomodation = accomodationsService.GetAccomodationById(model.Id);
            result = accomodationsService.DeleteAccomodation(accomodation);

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