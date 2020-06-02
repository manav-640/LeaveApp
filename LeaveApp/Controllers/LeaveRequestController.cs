using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveApp.Models;

namespace LeaveApp.Controllers
{
    public class LeaveRequestController : Controller
    {
        LeaveContext objLeaveCtxt = new LeaveContext();

        /* Name: Manav, Type: Employee */
        int iEmpId = 2;

        // GET: LeaveRequest
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewRequest()
        {
            return View(objLeaveCtxt.GetLeaveDetails(iEmpId));
        }

        [HttpGet]
        public ActionResult GetEmpLeaveDtl(int LeaveTypeId)
        {
            return Json(objLeaveCtxt.GetEmpLeaveDetails(iEmpId, LeaveTypeId), JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        public ActionResult RequestLeave(FormCollection fc)
        {
            LeaveRequestDtl objReq = new LeaveRequestDtl();
            objReq.EmpId = Convert.ToInt32(fc["hdnRegEmpId"]);
            objReq.ManagerId = Convert.ToInt32(fc["hdnRegManagerId"]);
            objReq.LeaveTypeId = Convert.ToInt32(fc["hdnRegLeaveTypeId"]);
            objReq.StartDate = fc["txtStartDate"]; // "27-02-2020";
            objReq.EndDate = fc["txtEndDate"]; // "29-02-2020";
            objReq.TotalDays = Convert.ToDateTime(objReq.EndDate).Subtract(Convert.ToDateTime(objReq.StartDate)).Days;
            //objReq.TotalDays = 2;
            objReq.Status = "0";
            objReq.Reason = fc["txtReason"];
            objReq.AttachmentPath = "";

            if (objLeaveCtxt.MakeLeaveRequest(objReq) > 0)
            {
                return RedirectToAction("NewRequest");
            }
            else
            {
                return Content("<script>alert('Record not inserted !!');</script>");
            }
        }


        [HttpGet]
        public ActionResult GetRequestLeaveDtl(int LeaveRequestId)
        {
            return Json(objLeaveCtxt.GetRequestedLeaveDetails(LeaveRequestId), JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        public ActionResult ApproveLeave(int LeaveRequestId, int Status)
        {
            if (objLeaveCtxt.UpdateLeaveRequestStatus(LeaveRequestId, Status) > 0)
            {
                return RedirectToAction("NewRequest");
            }
            else
            {
                return Content("<script>alert('Record not inserted !!');</script>");
            }
        }
    }
}