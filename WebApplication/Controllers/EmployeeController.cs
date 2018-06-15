using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Repository;
using WebApplication.ViewModel;

namespace WebApplication.Controllers
{
    [Authorize]
    public class EmployeeController : UserController
    {
        // GET: Employee
        CommonRepository _repo = new CommonRepository();
        public ActionResult Index()
        {
            return View();

        }
        [HttpGet]
        public JsonResult getCountries()
        {
            var lstCountry = _repo.GetCountries().Select(s => new SelectListItem { Text = s.CountryName, Value = s.CountryId.ToString() }).ToList();

            return Json(lstCountry, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getStates()
        {
            var lstStates = new CommonRepository().GetStates();
            return Json(lstStates, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getCities()
        {
            var lstCities = new CommonRepository().GetCities().Select(d => new SelectListItem { Text = d.CityName, Value = d.CityId.ToString() });
            return Json(lstCities, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getStates(int id)
        {

            var GetStates = _repo.GetStates(id);

            return Json(GetStates, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getCities(int id)
        {

            var getCities = _repo.GetCities(id);

            return Json(getCities, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveEmployee(UserVM userVM)
        {
            var user = new UserRepository().SaveProfile(userVM);
            return Json("ok");
        }
        public ActionResult EditProfile()
        {
            return View(new UserRepository().GetEmployees());
        }
        public ActionResult GetEmployee(int id)
        {
            Session["empId"] = id;
            return View("Index");
        }
        [HttpPost]
        public JsonResult getUserProfile(int id)
        {
            return Json(new UserRepository().GetEmployeesById(id), JsonRequestBehavior.AllowGet);
        }
    }

}