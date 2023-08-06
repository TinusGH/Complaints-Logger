using Complaints.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Helpers;

namespace Complaints.Data
{
    internal class ComplaintsDAO
    {
        private readonly string connectionString = @"Data Source=TINUS\SQLEXPRESS;Initial Catalog=COMPLAINTS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //performs all operations on the database. get all, create, delete, get one, search etc.

        //FetchAll
        public List<ComplaintsModel> FetchAll()
        {
            List<ComplaintsModel> returnList = new List<ComplaintsModel>();

            //access the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from dbo.Complaint";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //create new complaint object. Add it to the list to return.
                        ComplaintsModel complaints = new ComplaintsModel();

                        complaints.ID = reader.GetInt32(0);
                        complaints.FullName = reader.GetString(1);
                        complaints.Email = reader.GetString(2);
                        complaints.MobileNumber = reader.GetInt32(3);
                        complaints.ComplaintDetails = reader.GetString(4);

                        returnList.Add(complaints);
                    }
                }

            }
            return returnList;
        }

        //FetchOne
        public ComplaintsModel FetchOne(int id)
        {

            //access the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from dbo.Complaint WHERE Id = @id";

                //associate @id with Id parameter

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                ComplaintsModel complaint = new ComplaintsModel();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //create new complaint object. Add it to the list to return.


                        complaint.ID = reader.GetInt32(0);
                        complaint.FullName = reader.GetString(1);
                        complaint.Email = reader.GetString(2);
                        complaint.MobileNumber = reader.GetInt32(3);
                        complaint.ComplaintDetails = reader.GetString(4);
                    }
                }
                return complaint;
            }
        }

        //CreateNew
        public int Create(ComplaintsModel complaintsModel)
        {
            //access the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sqlQuery = "INSERT INTO dbo.Complaint Values(@FullName, @Email, @MobileNumber, @ComplaintDetails); SELECT CAST(scope_identity() AS int);";

                //associate @id with Id parameter

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@FullName", System.Data.SqlDbType.VarChar, 1000).Value = complaintsModel.FullName;
                command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 1000).Value = complaintsModel.Email;
                command.Parameters.Add("@MobileNumber", System.Data.SqlDbType.Int, 1000).Value = complaintsModel.MobileNumber;
                command.Parameters.Add("@ComplaintDetails", System.Data.SqlDbType.VarChar, 1000).Value = complaintsModel.ComplaintDetails;

                connection.Open();

                int newID = (int)command.ExecuteScalar();

                return newID;
            }

        }

        //update
        public int Update(ComplaintsModel complaintsModel)
        {
            //access the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //if complaintsmodel.id > 1 then Update
                string sqlQuery = "";

                if (complaintsModel.ID > 0)
                {
                    sqlQuery = "UPDATE dbo.Complaint SET FullName = @FullName, Email = @Email, MobileNumber = @MobileNumber, ComplaintDetails = @ComplaintDetails WHERE Id = @Id";

                }
                //associate @id with Id parameter

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.VarChar, 1000).Value = complaintsModel.ID;
                command.Parameters.Add("@FullName", System.Data.SqlDbType.VarChar, 1000).Value = complaintsModel.FullName;
                command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 1000).Value = complaintsModel.Email;
                command.Parameters.Add("@MobileNumber", System.Data.SqlDbType.Int, 1000).Value = complaintsModel.MobileNumber;
                command.Parameters.Add("@ComplaintDetails", System.Data.SqlDbType.VarChar, 1000).Value = complaintsModel.ComplaintDetails;

                connection.Open();

                int newID = command.ExecuteNonQuery();

                return newID;

            }

        }
        // delete
        internal int Delete(int id)
        {
            //access the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM dbo.Complaint WHERE Id = @Id";


                //associate @id with Id parameter

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.VarChar, 1000).Value = id;

                connection.Open();

                int DeletedID = command.ExecuteNonQuery();

                return DeletedID;
            }
        }

        // search
        internal List<ComplaintsModel> SearchForName(string searchPhrase)
        {
            List<ComplaintsModel> returnList = new List<ComplaintsModel>();

            //access the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from dbo.Complaint WHERE FullName LIKE @searchPhrase";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@searchPhrase", System.Data.SqlDbType.NVarChar).Value = "%" + searchPhrase + "%";

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //create new complaint object. Add it to the list to return.
                        ComplaintsModel complaints = new ComplaintsModel();

                        complaints.ID = reader.GetInt32(0);
                        complaints.FullName = reader.GetString(1);
                        complaints.Email = reader.GetString(2);
                        complaints.MobileNumber = reader.GetInt32(3);
                        complaints.ComplaintDetails = reader.GetString(4);

                        returnList.Add(complaints);
                    }
                }
                return returnList;
            }
        }
        public int CreateMetaData(ComplaintMetaData complaintMetaData)
        {
            //access the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO dbo.ComplaintMetaData Values(@ComplaintId, @IP, @DateLogged); SELECT CAST(scope_identity() AS int);";

                //associate @id with Id parameter

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@ComplaintId", System.Data.SqlDbType.Int, 1000).Value = complaintMetaData.ComplaintId;
                command.Parameters.Add("@IP", System.Data.SqlDbType.NVarChar, 15).Value = complaintMetaData.IP;
                command.Parameters.Add("@DateLogged", System.Data.SqlDbType.DateTime, 1000).Value = complaintMetaData.DateLogged;

                connection.Open();

                int newID = (int)command.ExecuteScalar();

                return newID;

            }
        }

        public ComplaintMetaData FetchMetaData(int complaintId)
        {
            ComplaintMetaData metaData = new ComplaintMetaData();
            //access the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from dbo.ComplaintMetaData WHERE ComplaintId = @id";

                //associate @id with Id parameter

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = complaintId;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        metaData.Id = reader.GetInt32(0);
                        metaData.ComplaintId = reader.GetInt32(1);
                        metaData.IP = reader.GetString(2);
                        metaData.DateLogged = reader.GetDateTime(3);
                    }

                }
            }
            return metaData;
        }
    }
}