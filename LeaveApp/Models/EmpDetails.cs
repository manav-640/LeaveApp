using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveApp.Models
{
    public class EmpDetails
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpType { get; set; }
        public string Dept { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmpType { get; set; }
    }
}