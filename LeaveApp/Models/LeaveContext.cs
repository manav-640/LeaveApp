using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.ModelBinding;

namespace LeaveApp.Models
{
    public class LeaveContext
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DELL-PC\SQLEXPRESS;Initial Catalog=Employee;User ID=sa; pwd=espl123");
        string szUserDateFormat = "dd/MM/yyyy";

        public LeaveDetails GetLeaveDetails(int LoginEmpId)
        {
            LeaveDetails Lst_LeaveDtl = new LeaveDetails();

            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_GetLeaveRequestDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", LoginEmpId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            for (int iTblIndex = 0; iTblIndex < ds.Tables.Count; iTblIndex++)
            {
                switch (iTblIndex)
                {
                    case 0:
                        {
                            #region ... Leave Type Master ...

                            foreach (DataRow dtRow in ds.Tables[iTblIndex].Rows)
                            {
                                LeaveTypeMst objLType = new LeaveTypeMst();

                                objLType.LeaveTypeId = (dtRow["LeaveTypeId"] is DBNull ? 0 : Convert.ToInt32(dtRow["LeaveTypeId"]));
                                objLType.LeaveType = (dtRow["LeaveType"] is DBNull ? "" : dtRow["LeaveType"].ToString());
                                objLType.NoOfApplicableLeaves = (dtRow["NoOfApplicableLeaves"] is DBNull ? 0 : Convert.ToInt32(dtRow["NoOfApplicableLeaves"]));
                                objLType.LeaveRequested = (dtRow["LeaveRequested"] is DBNull ? 0 : Convert.ToInt32(dtRow["LeaveRequested"]));

                                Lst_LeaveDtl.Lst_LeaveTypeMst.Add(objLType);
                            }

                            #endregion
                        }
                        break;
                    case 1:
                        {
                            #region ... Leave Request Details ...

                            foreach (DataRow dtRow in ds.Tables[iTblIndex].Rows)
                            {
                                LeaveRequestDtl objLReq = new LeaveRequestDtl();

                                objLReq.RequestId = (dtRow["RequestId"] is DBNull ? 0 : Convert.ToInt32(dtRow["RequestId"]));
                                objLReq.EmpId = (dtRow["EmpId"] is DBNull ? 0 : Convert.ToInt32(dtRow["EmpId"]));
                                objLReq.EmpName = (dtRow["EmpName"] is DBNull ? "" : dtRow["EmpName"].ToString());
                                objLReq.ManagerName = (dtRow["Manager"] is DBNull ? "" : dtRow["Manager"].ToString());
                                objLReq.LeaveType = (dtRow["LeaveType"] is DBNull ? "" : dtRow["LeaveType"].ToString());

                                if (!(dtRow["RequestDate"] is DBNull))
                                    objLReq.RequestDate = Convert.ToDateTime(dtRow["RequestDate"]).ToString(szUserDateFormat + " HH:mm:ss tt");

                                if (!(dtRow["StartDate"] is DBNull))
                                    objLReq.StartDate = Convert.ToDateTime(dtRow["StartDate"]).ToString(szUserDateFormat);

                                if (!(dtRow["EndDate"] is DBNull))
                                    objLReq.EndDate = Convert.ToDateTime(dtRow["EndDate"]).ToString(szUserDateFormat);

                                objLReq.TotalDays = (dtRow["TotalDays"] is DBNull ? 0 : Convert.ToInt32(dtRow["TotalDays"]));

                                if (dtRow["Status"] is DBNull)
                                {
                                    objLReq.Status = "Pending";
                                }
                                else
                                {
                                    switch (Convert.ToInt32(dtRow["Status"]))
                                    {
                                        case 0: { objLReq.Status = "Pending"; } break;
                                        case 1: { objLReq.Status = "Approved"; } break;
                                        case 2: { objLReq.Status = "Rejected"; } break;
                                        case 3: { objLReq.Status = "Cancelled"; } break;
                                    }
                                }

                                objLReq.Reason = (dtRow["Reason"] is DBNull ? "" : dtRow["Reason"].ToString());
                                objLReq.AttachmentPath = (dtRow["AttachmentPath"] is DBNull ? "" : dtRow["AttachmentPath"].ToString());

                                Lst_LeaveDtl.Lst_LeaveRequestDtl.Add(objLReq);
                            }

                            #endregion
                        }
                        break;
                    case 2:
                        {
                            #region ... Leave Pending Details ...

                            foreach (DataRow dtRow in ds.Tables[iTblIndex].Rows)
                            {
                                LeaveRequestDtl objLReq = new LeaveRequestDtl();

                                objLReq.RequestId = (dtRow["RequestId"] is DBNull ? 0 : Convert.ToInt32(dtRow["RequestId"]));
                                objLReq.EmpId = (dtRow["EmpId"] is DBNull ? 0 : Convert.ToInt32(dtRow["EmpId"]));
                                objLReq.EmpName = (dtRow["EmpName"] is DBNull ? "" : dtRow["EmpName"].ToString());
                                objLReq.ManagerName = (dtRow["Manager"] is DBNull ? "" : dtRow["Manager"].ToString());
                                objLReq.LeaveType = (dtRow["LeaveType"] is DBNull ? "" : dtRow["LeaveType"].ToString());

                                if (!(dtRow["RequestDate"] is DBNull))
                                    objLReq.RequestDate = Convert.ToDateTime(dtRow["RequestDate"]).ToString(szUserDateFormat + " HH:mm:ss tt");

                                if (!(dtRow["StartDate"] is DBNull))
                                    objLReq.StartDate = Convert.ToDateTime(dtRow["StartDate"]).ToString(szUserDateFormat);

                                if (!(dtRow["EndDate"] is DBNull))
                                    objLReq.EndDate = Convert.ToDateTime(dtRow["EndDate"]).ToString(szUserDateFormat);

                                objLReq.TotalDays = (dtRow["TotalDays"] is DBNull ? 0 : Convert.ToInt32(dtRow["TotalDays"]));

                                if (dtRow["Status"] is DBNull)
                                {
                                    objLReq.Status = "Pending";
                                }
                                else
                                {
                                    switch (Convert.ToInt32(dtRow["Status"]))
                                    {
                                        case 0: { objLReq.Status = "Pending"; } break;
                                        case 1: { objLReq.Status = "Approved"; } break;
                                        case 2: { objLReq.Status = "Rejected"; } break;
                                        case 3: { objLReq.Status = "Cancelled"; } break;
                                    }
                                }

                                objLReq.Reason = (dtRow["Reason"] is DBNull ? "" : dtRow["Reason"].ToString());
                                objLReq.AttachmentPath = (dtRow["AttachmentPath"] is DBNull ? "" : dtRow["AttachmentPath"].ToString());

                                Lst_LeaveDtl.Lst_LeavePendingDtl.Add(objLReq);
                            }

                            #endregion
                        }
                        break;
                }
            }

            return Lst_LeaveDtl;
        }

        public EmployeeLeaveDetails GetEmpLeaveDetails(int EmpId, int @LeaveTypeId)
        {
            EmployeeLeaveDetails objEmpLeaveDtl = new EmployeeLeaveDetails();
            EmpDetails objEmp = new EmpDetails();
            LeaveTypeMst objLType = new LeaveTypeMst();

            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_GetEmployeeLeaveDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", EmpId);
            cmd.Parameters.AddWithValue("@LeaveTypeId", @LeaveTypeId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            for (int iTblIndex = 0; iTblIndex < ds.Tables.Count; iTblIndex++)
            {
                switch (iTblIndex)
                {
                    case 0:
                        {
                            #region ... Leave Request Details ...

                            foreach (DataRow dtRow in ds.Tables[iTblIndex].Rows)
                            {
                                objEmp.EmpId = (dtRow["EmpId"] is DBNull ? 0 : Convert.ToInt32(dtRow["EmpId"]));
                                objEmp.EmpName = (dtRow["EmpName"] is DBNull ? "" : dtRow["EmpName"].ToString());
                                objEmp.EmpType = (dtRow["EmpType"] is DBNull ? "" : dtRow["EmpType"].ToString());
                                objEmp.Dept = (dtRow["DeptId"] is DBNull ? "" : dtRow["DeptId"].ToString());
                                objEmp.Email = (dtRow["Email"] is DBNull ? "" : dtRow["Email"].ToString());
                                objEmp.PhoneNo = (dtRow["PhoneNo"] is DBNull ? "" : dtRow["PhoneNo"].ToString());
                                objEmp.Address = (dtRow["Address"] is DBNull ? "" : dtRow["Address"].ToString());
                                objEmp.ManagerId = (dtRow["ManagerId"] is DBNull ? 0 : Convert.ToInt32(dtRow["ManagerId"]));
                                objEmp.ManagerName = (dtRow["ManagerName"] is DBNull ? "" : dtRow["ManagerName"].ToString());
                                objEmp.ManagerEmpType = (dtRow["MEmpType"] is DBNull ? "" : dtRow["MEmpType"].ToString());
                            }

                            #endregion
                        }
                        break;
                    case 1:
                        {
                            #region ... Leave Type Master ...

                            foreach (DataRow dtRow in ds.Tables[iTblIndex].Rows)
                            {
                                objLType.LeaveTypeId = (dtRow["LeaveTypeId"] is DBNull ? 0 : Convert.ToInt32(dtRow["LeaveTypeId"]));
                                objLType.LeaveType = (dtRow["LeaveType"] is DBNull ? "" : dtRow["LeaveType"].ToString());
                                objLType.NoOfApplicableLeaves = (dtRow["NoOfApplicableLeaves"] is DBNull ? 0 : Convert.ToInt32(dtRow["NoOfApplicableLeaves"]));
                                objLType.LeaveRequested = (dtRow["LeaveRequested"] is DBNull ? 0 : Convert.ToInt32(dtRow["LeaveRequested"]));
                            }

                            #endregion
                        }
                        break;
                }
            }

            objEmpLeaveDtl.EmpDetails = objEmp;
            objEmpLeaveDtl.LeaveTypeMst = objLType;
            return objEmpLeaveDtl;
        }

        public int MakeLeaveRequest(LeaveRequestDtl objReq)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_MakeLeaveRequest", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpId", objReq.EmpId);
            cmd.Parameters.AddWithValue("@ManagerId", objReq.ManagerId);
            cmd.Parameters.AddWithValue("@LeaveTypeId", objReq.LeaveTypeId);
            cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(objReq.StartDate).ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(objReq.EndDate).ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@TotalDays", objReq.TotalDays);
            cmd.Parameters.AddWithValue("@Status", objReq.Status);
            cmd.Parameters.AddWithValue("@Reason", objReq.Reason);
            cmd.Parameters.AddWithValue("@AttachmentPath", objReq.AttachmentPath);
            int iNoOfRows = cmd.ExecuteNonQuery();
            conn.Close();

            return iNoOfRows;
        }

        public RequestedLeaveDetails GetRequestedLeaveDetails(int @LeaveRequestId)
        {
            RequestedLeaveDetails objLdtl = new RequestedLeaveDetails();
            LeaveRequestDtl objlReq = new LeaveRequestDtl();
            LeaveTypeMst objLType = new LeaveTypeMst();

            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_GetRequestedLeaveDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeaveRequestId", @LeaveRequestId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            for (int iTblIndex = 0; iTblIndex < ds.Tables.Count; iTblIndex++)
            {
                switch (iTblIndex)
                {
                    case 0:
                        {
                            #region ... Leave Request Details ...

                            foreach (DataRow dtRow in ds.Tables[iTblIndex].Rows)
                            {
                                objlReq.RequestId = (dtRow["RequestId"] is DBNull ? 0 : Convert.ToInt32(dtRow["RequestId"]));
                                objlReq.EmpId = (dtRow["EmpId"] is DBNull ? 0 : Convert.ToInt32(dtRow["EmpId"]));
                                objlReq.EmpName = (dtRow["EmpName"] is DBNull ? "" : dtRow["EmpName"].ToString());
                                objlReq.LeaveType = (dtRow["LeaveType"] is DBNull ? "" : dtRow["LeaveType"].ToString());
                                objlReq.StartDate = (dtRow["StartDate"] is DBNull ? "" : Convert.ToDateTime(dtRow["StartDate"]).ToString(szUserDateFormat));
                                objlReq.EndDate = (dtRow["EndDate"] is DBNull ? "" : Convert.ToDateTime(dtRow["EndDate"]).ToString(szUserDateFormat));
                                objlReq.TotalDays = (dtRow["TotalDays"] is DBNull ? 0 : Convert.ToInt32(dtRow["TotalDays"]));
                            }

                            #endregion
                        }
                        break;
                    case 1:
                        {
                            #region ... Leave Type Master ...

                            foreach (DataRow dtRow in ds.Tables[iTblIndex].Rows)
                            {
                                objLType.LeaveTypeId = (dtRow["LeaveTypeId"] is DBNull ? 0 : Convert.ToInt32(dtRow["LeaveTypeId"]));
                                objLType.LeaveType = (dtRow["LeaveType"] is DBNull ? "" : dtRow["LeaveType"].ToString());
                                objLType.NoOfApplicableLeaves = (dtRow["NoOfApplicableLeaves"] is DBNull ? 0 : Convert.ToInt32(dtRow["NoOfApplicableLeaves"]));
                                objLType.LeaveRequested = (dtRow["LeaveRequested"] is DBNull ? 0 : Convert.ToInt32(dtRow["LeaveRequested"]));
                            }

                            #endregion
                        }
                        break;
                }
            }

            objLdtl.LeaveRequestDtl = objlReq;
            objLdtl.LeaveTypeMst = objLType;
            return objLdtl;
        }

        public int UpdateLeaveRequestStatus(int LeaveRequestId, int Status)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_UpdateLeaveStatus", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeaveRequestId", LeaveRequestId);
            cmd.Parameters.AddWithValue("@Status", Status);

            int iNoOfRows = cmd.ExecuteNonQuery();
            conn.Close();

            return iNoOfRows;
        }
    }

    public class LeaveDetails
    {
        public LeaveDetails()
        {
            Lst_LeaveTypeMst = new List<LeaveTypeMst>();
            Lst_LeaveRequestDtl = new List<LeaveRequestDtl>();
            Lst_LeavePendingDtl = new List<LeaveRequestDtl>();
        }

        public List<LeaveTypeMst> Lst_LeaveTypeMst { get; set; }
        public List<LeaveRequestDtl> Lst_LeaveRequestDtl { get; set; }
        public List<LeaveRequestDtl> Lst_LeavePendingDtl { get; set; }
    }

    public class EmployeeLeaveDetails
    {
        public LeaveTypeMst LeaveTypeMst { get; set; }
        public EmpDetails EmpDetails { get; set; }
    }

    public class RequestedLeaveDetails
    {
        public LeaveTypeMst LeaveTypeMst { get; set; }
        public LeaveRequestDtl LeaveRequestDtl { get; set; }
    }

}