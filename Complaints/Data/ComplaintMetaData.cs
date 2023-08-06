using Complaints.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;

namespace Complaints.Data
{
    public class ComplaintMetaData
    {
        [Required]
        public int Id { get; set; }

        [Required]    
        public int ComplaintId { get; set; }

        [Required]
        [DisplayName("IP Address")]
        public string IP { get; set; }

        [Required]
        [DisplayName("Date Logged")]
        public DateTime DateLogged { get; set; }
    }
}