using DigitalHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Json;
using System.Web.Http;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Text;

namespace DigitalHome.API
{
    public class DigitalApplianceAPIController : ApiController
    {
        //I am hardcoding the connection string here but it is highly recommended to use either configuration manager or Azure connection string utility to read dynamically
        string _connStr = "Server=tcp:<DB Server>,1433;Initial Catalog=<DB Name>;Persist Security Info=False;User ID=<UID>;Password=<pwd>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        [HttpGet]
        [ActionName("GetAllAppliances")]
        public IEnumerable<ApplianceMaster> GetAllAppliances(string Email)
        {
            try
            {
                string _outMessage = "";

                BusinessLogic objBusinessLogic = new BusinessLogic();
                IEnumerable<ApplianceMaster> lstResult = objBusinessLogic.GetAllAppliances(_connStr, Email, 0, out _outMessage);

                if (_outMessage == "SUCCESS")

                    return lstResult;
                else
                    return null;
            }
            catch
            {
                return null;
            }

        }

        [ActionName("GetAppStatusARDUNO")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//Specify return format.
        public HttpResponseMessage GetAppliancesStatusARDUNO(string Email)
        {
            string _outMessage = "";

            try
            {

                BusinessLogic objBusinessLogic = new BusinessLogic();
                IEnumerable<ApplianceMaster> lstResult = objBusinessLogic.GetAllAppliances(_connStr, Email, 0, out _outMessage);

                AppStatusARDUNO _appIOT = new AppStatusARDUNO();
                int _index = 0;
                foreach (ApplianceMaster app in lstResult)
                {
                    if (_index == 0) _appIOT.A1 = app.StatusOnOff;
                    if (_index == 1) _appIOT.A2 = app.StatusOnOff;
                    if (_index == 2) _appIOT.A3 = app.StatusOnOff;
                    if (_index == 3) _appIOT.A4 = app.StatusOnOff;
                    if (_index == 4) _appIOT.A5 = app.StatusOnOff;
                    if (_index == 5) _appIOT.A6 = app.StatusOnOff;
                    if (_index == 6) _appIOT.A7 = app.StatusOnOff;
                    if (_index == 7) _appIOT.A8 = app.StatusOnOff;
                    if (_index == 8) _appIOT.A9= app.StatusOnOff;
                    if (_index == 9) _appIOT.A10 = app.StatusOnOff;
                    if (_index == 10) _appIOT.A11 = app.StatusOnOff;
                    if (_index == 11) _appIOT.A12 = app.StatusOnOff;
                    if (_index == 12) _appIOT.A13 = app.StatusOnOff;
                    _index++;
                }


                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jSON = serializer.Serialize(_appIOT);

                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(jSON, Encoding.UTF8, "application/json");
                return response;

                
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
               // return "ERROR";
            }


        }

        [ActionName("GetAppliance")]
        public ApplianceMaster GetAppliance(string Email, long ApplianceKey)
        {
            string _outMessage = "";

            try
            {
                ApplianceMaster _app = new ApplianceMaster();
                BusinessLogic objBusinessLogic = new BusinessLogic();
                ApplianceMaster lstResult = objBusinessLogic.GetAppliances(ApplianceKey, _connStr, Email, out _outMessage);

                if (_outMessage == "SUCCESS")

                    return lstResult;
                else
                    return null;
            }
            catch
            {
                return null;
            }

        }

        [ActionName("UpdateStatus")]
        [AcceptVerbs("GET", "POST")]
        public string UpdateApplianceStatus(long ApplianceKey, string Email, string status)
        {
            string _outMessage = "";

            try
            {
                BusinessLogic objBusinessLogic = new BusinessLogic();
                objBusinessLogic.UpdateApplianceStatus(_connStr, Email, ApplianceKey, status, out _outMessage);
                return _outMessage;
            }
            catch
            {
                return _outMessage;
            }


        }

        [ActionName("CreateAppliance")]
        [HttpPost]
        [AcceptVerbs("GET", "POST")]
        public string CreateAppliance(string Email, string AppName, string AppDesc)
        {
            string _outMessage = "";
            try
            {
                BusinessLogic objBusinessLogic = new BusinessLogic();
                DataTable _dt = objBusinessLogic.CreateAppliance(_connStr, Email, AppName, AppDesc, "Y", out _outMessage);
                return _outMessage;

            }
            catch
            {
                return _outMessage;
            }
        }

        [ActionName("UpdateAppliance")]
        [HttpPost]
        [AcceptVerbs("GET", "POST")]
        public  string UpdateAppliance(string Email, long ApplianceKey, string AppName, string AppDesc, string activeYN)
        {
            string _outMessage = "";
            try
            {
                BusinessLogic objBusinessLogic = new BusinessLogic();
                objBusinessLogic.UpdateAppliance(_connStr, Email, ApplianceKey, AppName, AppDesc, activeYN, out _outMessage);
                return _outMessage;
            }
            catch
            {
                return _outMessage;
            }
        }
    }
}