using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace AngularJs_Aplication
{
    /// <summary>
    /// Summary description for EmployeeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService]
    public class EmployeeService : System.Web.Services.WebService
    {


        public string base64Encode(string sData)
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];

                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);

                string encodedData = Convert.ToBase64String(encData_byte);

                return encodedData;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        [WebMethod]
        public void Save(string LastName, string FirstName, Decimal IdentityNo, DateTime DateOfBirth,/* DateTime DateOfAppointment, string Status1, string SubStatus,*/ string Position, string Department, string Category, string GradeNo, string ExtensionNo, string MobilePhone, string OldMobilePhone, string EmailAddress1, string ConsultantName, string ConsultantAKA, string TeamName, string TeamLeader, string TeamLeaderExtensionNo, string IDBadge, string AccessLevel, /*DateTime LogDate,*/string UserName/*string Roles,string Password/*, string Spare1, string Spare2, string Spare3,string Spare4*/)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Connection = con;
                cmd.CommandText = "usp_IregistEmployee";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "INSERT INTO MasterFileEmployees(LastName, FirstName/*,IdentityNo*/,DateOfBirth, DateOfAppointment, Status1,  SubStatus,  Position, Department,Category,GradeNo,ExtensionNo, MobilePhone,OldMobilePhone,  EmailAddress1,ConsultantName, ConsultantAKA,TeamName,  TeamLeader,TeamLeaderExtensionNo,IDBadge, AccessLevel, LogDate, UserName, Spare1, Spare2,Spare3,Spare4) VALUES(@LastName, @FirstName/*, @IdentityNo*/, @DateOfBirth, @DateOfAppointment, @Status1, @SubStatus, @Position, @Department, @Category, @GradeNo, @ExtensionNo, @MobilePhone, @OldMobilePhone, @EmailAddress1, @ConsultantName, @ConsultantAKA, @TeamName, @TeamLeader, @TeamLeaderExtensionNo, @IDBadge, @AccessLevel, @LogDate, @UserName, @Spare1, @Spare2, @Spare3, @Spare4)";


                //string pass1 = LastName + FirstName + "@";
                //DateTime currDate1 = DateTime.Now;
                //string currDate = currDate1.ToString("ddyyyy");
                //string pass = pass1 + currDate;
                //string ePass = base64Encode(pass);


                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@IdentityNo", IdentityNo);
                cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                //cmd.Parameters.AddWithValue("@DateOfAppointment", DateOfAppointment);
                //cmd.Parameters.AddWithValue("@Status1", Status1);
                //cmd.Parameters.AddWithValue("@SubStatus", SubStatus);
                cmd.Parameters.AddWithValue("@Position", Position);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@GradeNo", GradeNo);
                cmd.Parameters.AddWithValue("@ExtensionNo", ExtensionNo);
                cmd.Parameters.AddWithValue("@MobilePhone", MobilePhone);
                cmd.Parameters.AddWithValue("@OldMobilePhone", OldMobilePhone);
                cmd.Parameters.AddWithValue("@EmailAddress1", EmailAddress1);
                cmd.Parameters.AddWithValue("@ConsultantName", ConsultantName);
                cmd.Parameters.AddWithValue("@ConsultantAKA", ConsultantAKA);
                cmd.Parameters.AddWithValue("@TeamName", TeamName);
                cmd.Parameters.AddWithValue("@TeamLeader", TeamLeader);
                cmd.Parameters.AddWithValue("@TeamLeaderExtensionNo", TeamLeaderExtensionNo);
                cmd.Parameters.AddWithValue("@IDBadge", IDBadge);
                cmd.Parameters.AddWithValue("@AccessLevel", AccessLevel);
                //cmd.Parameters.AddWithValue("@LogDate", LogDate);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                //cmd.Parameters.AddWithValue("@Roles", Roles);
                //cmd.Parameters.AddWithValue("@Password", pass);
                //cmd.Parameters.AddWithValue("@Spare1", Spare1);
                //cmd.Parameters.AddWithValue("@Spare2", Spare2);
                //cmd.Parameters.AddWithValue("@Spare3", Spare3);
                //cmd.Parameters.AddWithValue("@Spare4", Spare4);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        //Update employee details
        [WebMethod]
        public void EditEmpDetails(int EmployeeID, string LastName, string FirstName, Decimal IdentityNo, DateTime DateOfBirth,/* DateTime DateOfAppointment, string Status1, string SubStatus, */string Position, string Department, string Category, string GradeNo, string ExtensionNo, int MobilePhone, int OldMobilePhone, string EmailAddress1, string ConsultantName, string ConsultantAKA, string TeamName, string TeamLeader, string TeamLeaderExtensionNo, string IDBadge, string AccessLevel, /*DateTime LogDate,*/ string UserName)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Connection = con;
                cmd.CommandText = "usp_UMasterEmployeeInfo";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@IdentityNo", IdentityNo);

                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.DateTime));
                cmd.Parameters["@DateOfBirth"].Value = DateOfBirth;

                //cmd.Parameters.Add(new SqlParameter("@DateOfAppointment", SqlDbType.DateTime));
                //cmd.Parameters["@DateOfAppointment"].Value = DateOfAppointment;
                //cmd.Parameters.AddWithValue("@DateOfAppointment", DateOfAppointment);
                //cmd.Parameters.AddWithValue("@Status1", Status1);
                //cmd.Parameters.AddWithValue("@SubStatus", SubStatus);
                cmd.Parameters.AddWithValue("@Position", Position);
                cmd.Parameters.AddWithValue("@Department", Department);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@GradeNo", GradeNo);
                cmd.Parameters.AddWithValue("@ExtensionNo", ExtensionNo);
                cmd.Parameters.AddWithValue("@MobilePhone", MobilePhone);
                cmd.Parameters.AddWithValue("@OldMobilePhone", OldMobilePhone);
                cmd.Parameters.AddWithValue("@EmailAddress1", EmailAddress1);
                cmd.Parameters.AddWithValue("@ConsultantName", ConsultantName);
                cmd.Parameters.AddWithValue("@ConsultantAKA", ConsultantAKA);
                cmd.Parameters.AddWithValue("@TeamName", TeamName);
                cmd.Parameters.AddWithValue("@TeamLeader", TeamLeader);
                cmd.Parameters.AddWithValue("@TeamLeaderExtensionNo", TeamLeaderExtensionNo);
                cmd.Parameters.AddWithValue("@IDBadge", IDBadge);
                cmd.Parameters.AddWithValue("@AccessLevel", AccessLevel);
                // cmd.Parameters.AddWithValue("@LogDate", LogDate);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        //get Employee list
        [WebMethod]
        public List<AddEmployee> GetList()
        {
            List<AddEmployee> allEmployees = new List<AddEmployee>();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "usp_GetMasterEmployeeList";
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    AddEmployee masterEmployees = new AddEmployee();

                    masterEmployees.EmployeeID = Convert.ToInt32(read["EmployeeID"]);
                    masterEmployees.LastName = Convert.ToString(read["LastName"]);
                    masterEmployees.FirstName = Convert.ToString(read["FirstName"]);
                    masterEmployees.IdentityNo = Convert.ToDecimal(read["IdentityNo"]);
                    masterEmployees.DateOfBirth = Convert.ToDateTime(read["DateOfBirth"]);
                    masterEmployees.DateOfAppointment = Convert.ToDateTime(read["DateOfAppointment"]);
                    masterEmployees.Status1 = Convert.ToString(read["Status1"]);
                    masterEmployees.SubStatus = Convert.ToString(read["SubStatus"]);
                    masterEmployees.Position = Convert.ToString(read["Position"]);
                    masterEmployees.Department = Convert.ToString(read["Department"]);
                    masterEmployees.Category = Convert.ToString(read["Category"]);
                    masterEmployees.GradeNo = Convert.ToString(read["GradeNo"]);
                    masterEmployees.ExtensionNo = Convert.ToString(read["ExtensionNo"]);
                    masterEmployees.MobilePhone = Convert.ToInt32(read["MobilePhone"]);
                    masterEmployees.OldMobilePhone = Convert.ToInt32(read["OldMobilePhone"]);
                    masterEmployees.EmailAddress1 = Convert.ToString(read["EmailAddress1"]);
                    masterEmployees.ConsultantName = Convert.ToString(read["ConsultantName"]);
                    masterEmployees.ConsultantAKA = Convert.ToString(read["ConsultantAKA"]);
                    masterEmployees.TeamName = Convert.ToString(read["TeamName"]);
                    masterEmployees.TeamLeader = Convert.ToString(read["TeamLeader"]);
                    masterEmployees.TeamLeaderExtensionNo = Convert.ToString(read["TeamLeaderExtensionNo"]);
                    masterEmployees.IDBadge = Convert.ToString(read["IDBadge"]);
                    masterEmployees.AccessLevel = Convert.ToString(read["AccessLevel"]);
                    masterEmployees.UserName = Convert.ToString(read["UserName"]);


                    allEmployees.Add(masterEmployees);

                }

            }


            return allEmployees;
        }





        //Delete employee training
        [WebMethod]
        public void RemoveEmp(int EmployeeID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "usp_DMasterEmployee";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
    }
}
