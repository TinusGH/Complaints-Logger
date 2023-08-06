using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Complaints.Data;
using Complaints.Models;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace Complaints.Controllers
{
    public class ComplaintsView
    {
        public ComplaintsModel ComplaintsModel { get; set; }
        public ComplaintMetaData ComplaintMetaData { get; set; }
    }
    public class ComplaintsController : Controller
    {
        // GET: Complaints

        public ActionResult Complaints()
        {
            return View("Complaints", GetComplaintsView());
        }

        // GET: One Complaint
        public ActionResult Complaint(int id)
        {
            return View("Complaint", GetComplaintsView(id));
        }

        // POST: Create Complaint
        public ActionResult CreateForm()
        {
            return View("CreateForm");
        }

        [HttpPost]
        public ActionResult Create(ComplaintsModel complaintsModel)
        {
            //save to db
            ComplaintsDAO complaintsDAO = new ComplaintsDAO();
            var complaintId = complaintsDAO.Create(complaintsModel);

            var metadata = new ComplaintMetaData()
            {
                ComplaintId = complaintId,
                DateLogged = DateTime.Now,
                IP = System.Web.HttpContext.Current.Request.UserHostAddress.ToString()
                //(Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim()
            };          

            complaintsDAO.CreateMetaData(metadata);

            return View("Complaints", GetComplaintsView());
        }

        //PUT Update Complaint
        [HttpPost]
        public ActionResult Edit(ComplaintsModel complaintsModel, int id)
        {
            //save to db
            ComplaintsDAO complaintsDAO = new ComplaintsDAO();
            ComplaintsModel complaint = complaintsDAO.FetchOne(id);
            complaintsDAO.Update(complaintsModel);

            return View("EditForm", complaint);
        }

        //DELETE
        public ActionResult Delete(int id)
        {
            //save to db
            ComplaintsDAO complaintsDAO = new ComplaintsDAO();
            complaintsDAO.Delete(id);

            return View("Complaints", GetComplaintsView());
        }

        //Search
        public ActionResult SearchForm()
        {
            return View("SearchForm");
        }

        public ActionResult SearchForName(string searchPhrase)
        {
            return View("Complaints", GetSearchView(searchPhrase));
        }

        private List<ComplaintsView> GetSearchView(string searchPhrase)
        {
            List<ComplaintsView> result = new List<ComplaintsView>();
            ComplaintsDAO complaintsDAO = new ComplaintsDAO();
            var searchResults = complaintsDAO.SearchForName(searchPhrase);
            foreach (var complaint in searchResults)
            {
                var view = new ComplaintsView();
                view.ComplaintsModel = complaint;
                view.ComplaintMetaData = complaintsDAO.FetchMetaData(complaint.ID);
                result.Add(view);
            }        
            return (result);
        }

        private List<ComplaintsView> GetComplaintsView()
        {
            List<ComplaintsView> result = new List<ComplaintsView>();
            {
                ComplaintsDAO complaintsDAO = new ComplaintsDAO();

                var complaints = complaintsDAO.FetchAll();

                foreach (var complaint in complaints)
                {
                    var view = new ComplaintsView();
                    view.ComplaintsModel = complaint;
                    view.ComplaintMetaData = complaintsDAO.FetchMetaData(complaint.ID);
                    result.Add(view);
                }
            };
            return (result);
        }

        private ComplaintsView GetComplaintsView(int id)
        {
            ComplaintsDAO complaintsDAO = new ComplaintsDAO();

            var complaint = complaintsDAO.FetchOne(id);

            var view = new ComplaintsView();
            view.ComplaintsModel = complaint;
            view.ComplaintMetaData = complaintsDAO.FetchMetaData(complaint.ID);

            return view;
        }
        //public string GetIp()
        //{
        //    string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (string.IsNullOrEmpty(ip))
        //    {
        //        ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    return ip;
        //}

        //private void GetIpValue(out string ipAdd)
        //{
        //    ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        //    if (string.IsNullOrEmpty(ipAdd))
        //    {
        //        ipAdd = Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //}

    }
}