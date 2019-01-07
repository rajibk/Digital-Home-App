using DigitalHome.API;
using DigitalHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DigitalHome.App
{
    public class DigitalApplianceController : Controller
    {
        //For temp purpose, I have hardcoded email ID..But it has to be retrived based on login
        string strEmail = "<youremailid>"; 
        // GET: DigitalAppliance
        public ActionResult Index()
        {
            DigitalApplianceAPIController _api = new DigitalApplianceAPIController();
            IEnumerable<ApplianceMaster> _ApplianceList = _api.GetAllAppliances(strEmail);

            return View(_ApplianceList);

        }
        public ActionResult Appliance(long AppKey)
        {
            DigitalApplianceAPIController _api = new DigitalApplianceAPIController();
            ApplianceMaster _Appliance = _api.GetAppliance(strEmail, AppKey);

            return View(_Appliance);

        }
        public ActionResult EditAppliance(long AppKey)
        {
            DigitalApplianceAPIController _api = new DigitalApplianceAPIController();
            ApplianceMaster _Appliance = _api.GetAppliance(strEmail, AppKey);

            return View(_Appliance);

        }

        public ActionResult DeleteAppliance(long AppKey)
        {
            DigitalApplianceAPIController _api = new DigitalApplianceAPIController();
            ApplianceMaster _Appliance = _api.GetAppliance(strEmail, AppKey);

            return View(_Appliance);

        }

        [HttpPost]
        public async Task<ActionResult> DeleteAppliance(ApplianceMaster Model)
        {
            DigitalApplianceAPIController _api = new DigitalApplianceAPIController();
            _api.UpdateAppliance(strEmail, Model.ApplianceKey, "", "", "N");

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<ActionResult> EditAppliance (ApplianceMaster Model)
        {
            DigitalApplianceAPIController _api = new DigitalApplianceAPIController();
            _api.UpdateAppliance(strEmail, Model.ApplianceKey, Model.ApplianceName, Model.ApplianceDesc, "Y");

            return RedirectToAction("Index");

        }

       // [HttpPost]
        public ActionResult SwitchOnOff(long AppKey,string Status)
        {
            DigitalApplianceAPIController _api = new DigitalApplianceAPIController();
            _api.UpdateApplianceStatus(AppKey,strEmail, Status);

            return RedirectToAction("Index");

        }

        public ActionResult CreateAppliance()
        {
           
            return View();

        }

        [HttpPost]
        public async Task<ActionResult> CreateAppliance(ApplianceMaster Model)
        {

            DigitalApplianceAPIController _api = new DigitalApplianceAPIController();
            _api.CreateAppliance(strEmail,Model.ApplianceName,Model.ApplianceDesc);

            return RedirectToAction("Index");

        }

    }
}
