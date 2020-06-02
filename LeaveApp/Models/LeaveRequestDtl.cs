using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveApp.Models
{
    public class LeaveRequestDtl
    {
        public int RequestId { get; set; }
        public int EmpId { get; set; }
        public int ManagerId { get; set; }
        public string EmpName { get; set; }
        public string ManagerName { get; set; }
        public int LeaveTypeId { get; set; }
        public string LeaveType { get; set; }
        public string RequestDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public float TotalDays { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string AttachmentPath { get; set; }
    }
}