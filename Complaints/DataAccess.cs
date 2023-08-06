using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Complaints.Models;
using Dapper;

namespace Complaints
{
    public class DataAccess
    {
        //public List<ComplaintsModel> GetComplaintItems(int ID) 
        //{
        //    using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("COMPLAINTS")))
        //    {
        //        var output = connection.Query<ComplaintItemModel>($"select * from ComplaintItem where ID = '{ID}'").ToList();
        //        return output;   
        //    }
        //}
    }
}