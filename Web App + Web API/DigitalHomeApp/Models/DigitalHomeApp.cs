using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalHomeApp.Models
{
    public class ApplianceMaster
    {
        public ApplianceMaster()
        {

        }
        public long ApplianceKey { get; set; }
        [Display(Name = "Appliance Name")]
        public string ApplianceName { get; set; }
        [Display(Name = "Appliance Description")]
        public string ApplianceDesc { get; set; }
        [Display(Name = "User's Email")]
        public string UserEmail { get; set; }
        [Display(Name = "Status - On/Off")]
        public string StatusOnOff { get; set; }
        [Display(Name = "Status Last Changed")]
        public string LastStatusChanged { get; set; }
        [Display(Name = "Active Status")]
        public string ActiveYN { get; set; }
        public string DisplayStatusOnOff {
            get
            {
                if (StatusOnOff == "O")
                    return "On";
                else
                    return "Off";
            }
        }
    }

    //This model is for Arduino UNO. Arduino UNO has 13 output PINS, that mean, max 13 devices can be 
    //switched on or off using 1 arduino UNO
    public class AppStatusARDUNO
    {
        public string A1 { get; set; }
        public string A2 { get; set; }
        public string A3 { get; set; }
        public string A4 { get; set; }
        public string A5 { get; set; }
        public string A6 { get; set; }
        public string A7 { get; set; }
        public string A8 { get; set; }
        public string A9 { get; set; }
        public string A10 { get; set; }
        public string A11 { get; set; }
        public string A12 { get; set; }
        public string A13 { get; set; }
    }


}