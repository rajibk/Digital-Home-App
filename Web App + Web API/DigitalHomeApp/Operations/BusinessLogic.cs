
using DigitalHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DigitalHome
{
    public class BusinessLogic
    {

        public IEnumerable<ApplianceMaster> GetAllAppliances(string ConnString, string Email, long ApplianceKey, out string ReturnMessage)
        {

            try
            {
                string SQLQuery = "select appliance_key, appliance_name, appliance_desc,user_email,	status_on_off, last_status_change , active_yn " +
                              " from digital_appliance " +
                              " where upper(user_email) = '" + Email.ToUpper() + "' ";

                if (ApplianceKey > 0)
                {
                    SQLQuery = SQLQuery + " and appliance_key = " + ApplianceKey;
                }

                DataTable _result = DataAccess.GetData(ConnString, SQLQuery);

                List<ApplianceMaster> lstAppMaster = new List<ApplianceMaster>();
                ApplianceMaster lappMaster;


                foreach (DataRow dtr in _result.Rows)
                {
                    lappMaster = new ApplianceMaster();
                    lappMaster.ApplianceKey = dtr.Field<long>("appliance_key");
                    lappMaster.ApplianceName = dtr.Field<string>("appliance_name");
                    lappMaster.ApplianceDesc = dtr.Field<string>("appliance_desc");
                    lappMaster.UserEmail = dtr.Field<string>("user_email");
                    lappMaster.StatusOnOff = dtr.Field<string>("status_on_off");
                    lappMaster.LastStatusChanged = dtr.Field<DateTime?>("last_status_change").ToString();
                    lappMaster.ActiveYN = dtr.Field<string>("active_yn");
                    lstAppMaster.Add(lappMaster);
                }

                ReturnMessage = "SUCCESS";
                return lstAppMaster;
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                throw;
            }


        }


        public ApplianceMaster GetAppliances(long ApplianceKey, string ConnString, string Email, out string ReturnMessage)
        {

            try
            {
                IEnumerable<ApplianceMaster> _lstAppliances = GetAllAppliances(ConnString, Email, ApplianceKey, out ReturnMessage);
                ReturnMessage = "SUCCESS";
                return _lstAppliances.First<ApplianceMaster>();
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                throw;
            }
        }




        public DataTable CreateAppliance(string ConnString, string Email, string appliance_name, string appliance_desc, string activeYN, out string ReturnMessage)
        {
            try
            {
                string SQLInsert = " insert into  digital_appliance " +
                                " ( appliance_name, appliance_desc,  user_email, status_on_off, active_yn ) " +
                                " values " +
                                " ( '" + appliance_name + "','" + appliance_desc + "','" + Email + "','F','Y')";

                int retValue = DataAccess.ExecuteProcedure(ConnString, SQLInsert);

                string SQLQuery = " select max(appliance_key) as appliance_key from dbo.digital_appliance where upper(user_email) = '" + Email.ToUpper() + "'";

                ReturnMessage = "SUCCESS";
                return DataAccess.GetData(ConnString, SQLQuery);

            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                throw;
            }

        }

        public void UpdateAppliance(string ConnString, string Email, long applianceKey, string appliance_name, string appliance_desc, string activeYN, out string ReturnMessage)
        {
            try
            {
                string SQLInsert = "";

                if (activeYN.ToUpper() == "Y")
                {
                    SQLInsert = " update  digital_appliance " +
                                        " set appliance_name = '" + appliance_name + "'" +
                                        " , appliance_desc = '" + appliance_desc + "'" +
                                        " where appliance_key = " + applianceKey +
                                        " and upper(user_email) = '" + Email.ToUpper() + "'";
                }
                else
                {
                    SQLInsert = "delete from digital_appliance where appliance_key = " + applianceKey +
                                        " and upper(user_email) = '" + Email.ToUpper() + "'";
                }

                DataAccess.ExecuteProcedure(ConnString, SQLInsert);
                ReturnMessage = "SUCCESS";

            }

            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                throw;
            }
        }

        public void UpdateApplianceStatus(string ConnString, string Email, long applianceKey, string status, out string ReturnMessage)
        {
            try
            {
                string SQLInsert = "";

                if ((status.ToUpper() == "O") || ((status.ToUpper() == "F")))
                {
                    SQLInsert = " update  digital_appliance " +
                                        " set status_on_off = '" + status + "'," +
                                        " last_status_change = GETDATE() "  +
                                        " where appliance_key = " + applianceKey +
                                        " and upper(user_email) = '" + Email.ToUpper() + "'";

                    DataAccess.ExecuteProcedure(ConnString, SQLInsert);
                    ReturnMessage = "SUCCESS";

                }
                else
                {
                    ReturnMessage = "INVALID APPLIANCE STATUS";
                    throw (new Exception(ReturnMessage));
                }


            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                throw;
            }

        }
    }
}