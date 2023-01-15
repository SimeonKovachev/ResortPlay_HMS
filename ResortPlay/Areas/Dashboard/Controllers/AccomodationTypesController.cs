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
    public class AccomodationTypesController : Controller
    {
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        public ActionResult Index(string searchTerm)
        {
            AccomodationTypesListingModel model = new AccomodationTypesListingModel();

            model.SearchTerm = searchTerm;

            model.AccomodationTypes = accomodationTypesService.SearchAccomodationTypes(searchTerm);

            return View(model);
        }

        //Here to Read from the model the chosen accomodation
        [HttpGet]
        public ActionResult Action(int? Id)
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();

            if (Id.HasValue) //Edit a record
            {
                var accomodationType = accomodationTypesService.GetAccomodationTypeById(Id.Value);

                model.Id = accomodationType.Id;
                model.Name= accomodationType.Name;
                model.Description= accomodationType.Description;

            }
            

            return PartialView("_Action", model);


        }

        //Here to make an action on the chosen accomodation
        [HttpPost]
        public JsonResult Action(AccomodationTypeActionModel model)
        {

            JsonResult json = new JsonResult();
            var result = false;

            if (model.Id > 0) //Edit a record
            {
                var accomodationType = accomodationTypesService.GetAccomodationTypeById(model.Id);

                accomodationType.Name= model.Name;
                accomodationType.Description = model.Description;

                result = accomodationTypesService.EditAccomodationTypes(accomodationType);

            }
            else //Create a record
            {

                AccomodationType accomodationType = new AccomodationType();

                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                result = accomodationTypesService.SaveAccomodationTypes(accomodationType);
            }

            if (result)
            {
                json.Data = new {Success = true};
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
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();

            var accomodationType = accomodationTypesService.GetAccomodationTypeById(Id);

            model.Id = accomodationType.Id;
         
            return PartialView("_Delete", model);

        }
        [HttpPost]
        public JsonResult Delete(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();
            var result = false;

            var accomodationType = accomodationTypesService.GetAccomodationTypeById(model.Id);
            result = accomodationTypesService.DeleteAccomodationTypes(accomodationType);

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