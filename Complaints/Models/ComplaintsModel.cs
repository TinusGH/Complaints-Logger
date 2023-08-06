using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Complaints.Models
{
    public class ComplaintsModel

    {
        public int ID { get; set; }

        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Mobile Number")]
        public int MobileNumber { get; set; }

        [Required]
        [DisplayName("Complaint Details")]
        public string ComplaintDetails { get; set; }
        
        public ComplaintsModel()
        {
            ID = 1;
            FullName = "Nameless";
            Email = "Nope@Gmail.com";
            MobileNumber = 01234566789;
            ComplaintDetails = "None";
        }

        public ComplaintsModel(int iD, string fullName, string email, int mobileNumber, string complaintDetails)
        {
            ID = iD;
            FullName = fullName;
            Email = email;
            MobileNumber = mobileNumber;
            ComplaintDetails = complaintDetails;
        }
    }




}