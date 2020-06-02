using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveApp.Models
{
    public class LeaveTypeMst
    {
        public int LeaveTypeId { get; set; }

        public string LeaveType { get; set; }

        public int NoOfApplicableLeaves { get; set; }

        public int LeaveRequested { get; set; }
    }
}