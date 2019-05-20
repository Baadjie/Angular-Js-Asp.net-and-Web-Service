using AngularJs_Aplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;

namespace AngularJs_Aplication
{
    /// <summary>
    /// Summary description for TrainingService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    [System.Web.Script.Services.ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TrainingService : System.Web.Services.WebService
    {

        //Retrive Training Information
        [WebMethod]

        public List<ProductTraining> GetList()
        {
            List<ProductTraining> productTrainingList = new List<ProductTraining>();
            DataSet ds = new DataSet();

            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "usp_GetTraningInfo";
                cmd.Connection = conn2;
                conn2.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    ProductTraining productTraining = new ProductTraining();

                    productTraining.Id = Convert.ToInt32(read["Id"]);
                    productTraining.Name = Convert.ToString(read["Name"]);

                    productTraining.DateCompleted = Convert.ToDateTime(read["DateCompleted"]);
                    productTraining.Verified = Convert.ToDateTime(read["Verified"]);
                    productTraining.EmployeeID = Convert.ToInt32(read["EmployeeID"]);
                    productTraining.ProductId = Convert.ToInt32(read["ProductId"]);
                    productTraining.ProductName = Convert.ToString(read["ProductName"]);
                    productTraining.TrainingProvidedBy = Convert.ToString(read["TrainingProvidedBy"]);
                    productTraining.TypeOfAssessment = Convert.ToString(read["TypeOfAssessment"]);
                    productTraining.ExpectationForCompetence = Convert.ToString(read["ExpectationForCompetence"]);
                    productTraining.OutcomeStatus = Convert.ToString(read["OutcomeStatus"]);
                    productTraining.Comment = Convert.ToString(read["Comment"]);

                    productTrainingList.Add(productTraining);

                }


            }

            return productTrainingList;


        }

        [WebMethod]
        public void GetOutstandingList()
        {
            List<OutstandiningTraining> OutstandingTraining = new List<OutstandiningTraining>();
            DataSet ds = new DataSet();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "get_OutstandingTraining";
                cmd.Connection = con;
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    OutstandiningTraining outstandingTraining = new OutstandiningTraining();


                    outstandingTraining.Name = Convert.ToString(read["Name"]);
                    outstandingTraining.ProductType = Convert.ToString(read["ProductType"]);
                    outstandingTraining.ProductSubType = Convert.ToString(read["ProductSubType"]);
                    outstandingTraining.TrainingType = Convert.ToString(read["TrainingType"]);
                    outstandingTraining.TrainingCheckAll = Convert.ToString(read["TrainingCheckAll"]);
                    outstandingTraining.Position = Convert.ToString(read["Position"]);
                    outstandingTraining.Department = Convert.ToString(read["Department"]);

                    OutstandingTraining.Add(outstandingTraining);

                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(OutstandingTraining));
            }
        }
        //Get Products
        [WebMethod]

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            DataSet ds = new DataSet();

            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "usp_GetProduct";
                cmd.Connection = conn2;
                conn2.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {


                    Product product = new Product();

                    product.Id = Convert.ToInt32(read["Id"]);
                    product.ProductType = Convert.ToString(read["ProductType"]);

                    product.ProductSubType = Convert.ToString(read["ProductSubType"]);
                    product.TrainingType = Convert.ToString(read["TrainingType"]);
                    product.LogUserId = Convert.ToInt32(read["LogUserId"]);
                    //product.LogDate = Convert.ToDateTime(read["LogDate"]);


                    products.Add(product);

                }


            }

            return products;


        }


        [WebMethod]
        //Deleting Training Info
        public void Delete(int Id)
        {
            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn2;
                    //cmd.CommandText = "delete from TrainingInfo where Id=@Id;";
                    // cmd.CommandText = "update TrainingInfo set IsDeleted=1 where Id=@Id;";     WORKING
                    cmd.CommandText = "usp_DTrainingInfo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);


                    conn2.Open();
                    cmd.ExecuteNonQuery();
                    conn2.Close();
                }
            }
        }


        [WebMethod]
        //Deleting Product Info
        public void DeleteProduct(int Id)
        {
            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn2;

                    cmd.CommandText = "usp_DProduct";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);


                    conn2.Open();
                    cmd.ExecuteNonQuery();
                    conn2.Close();
                }
            }
        }


        [WebMethod]
        //Update Training Info

        public void Edit(int Id, int EmployeeID, int ProductId, string TrainingProvidedBy, string TypeOfAssessment, string ExpectationForCompetence,/* string OutcomeStatus,*/ string Comment)
        {


            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn2;



                    cmd.CommandText = "usp_UTrainingInfo";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    cmd.Parameters.AddWithValue("@ProductId", ProductId);
                    cmd.Parameters.AddWithValue("@Id", Id);

                    cmd.Parameters.AddWithValue("@TrainingProvidedBy", TrainingProvidedBy);
                    cmd.Parameters.AddWithValue("@TypeOfAssessment", TypeOfAssessment);

                    cmd.Parameters.AddWithValue("@ExpectationForCompetence", ExpectationForCompetence);
                    //cmd.Parameters.AddWithValue("@OutcomeStatus", OutcomeStatus);
                    cmd.Parameters.AddWithValue("@Comment", Comment);

                    conn2.Open();
                    cmd.ExecuteNonQuery();
                    conn2.Close();
                }
            }
        }
        //Insert Product Information
        [WebMethod]
        public void Save(int EmployeeID, int ProductId, DateTime DateCompleted, DateTime VerifiedDate, string TrainingProvidedBy, string TypeOfAssessment, string ExpectationForCompetence, string OutcomeStatus, string Comment, FileData fileData)
        {
            if (fileData.FileName != "")
            {

                //if (!string.IsNullOrEmpty(fileData.FileName))
                //{


                byte[] imageBytes = Convert.FromBase64String(fileData.Data);
                string filePath = "~/Images/" + fileData.FileName;
                System.IO.File.WriteAllBytes(Server.MapPath(filePath), imageBytes);

                SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn2;
                        
                        cmd.CommandText = "usp_ITrainingInfo";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        cmd.Parameters.AddWithValue("@ProductId", ProductId);
                        cmd.Parameters.AddWithValue("@DateCompleted", DateCompleted);
                        cmd.Parameters.AddWithValue("@VerifiedDate", VerifiedDate);
                        cmd.Parameters.AddWithValue("@TrainingProvidedBy", TrainingProvidedBy);
                        cmd.Parameters.AddWithValue("@TypeOfAssessment", TypeOfAssessment);

                        cmd.Parameters.AddWithValue("@ExpectationForCompetence", ExpectationForCompetence);
                        cmd.Parameters.AddWithValue("@OutcomeStatus", OutcomeStatus);
                        cmd.Parameters.AddWithValue("@Comment", Comment);
                        cmd.Parameters.AddWithValue("@FileName", fileData.FileName);
                        cmd.Parameters.AddWithValue("@Path", filePath);
                     
                        conn2.Open();
                        cmd.ExecuteNonQuery();
                        conn2.Close();
                    }
                }
            }
            else
            {

                SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn2;

                        //cmd.CommandText = "insert into TrainingInfo (EmpId,ProductId,DateCompleted,Verified,TrainingProviderBy,TypeOfAssessment,Expectation,OutcomeStatus,Comment)" +
                        //    " values (@EmpId,@ProductId,@DateCompleted,@Verified,@TrainingProviderBy,@TypeOfAssessment,@Expectation,@OutcomeStatus,@Comment);";

                        cmd.CommandText = "usp_ITrainingInfo";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        cmd.Parameters.AddWithValue("@ProductId", ProductId);
                        cmd.Parameters.AddWithValue("@DateCompleted", DateCompleted);
                        cmd.Parameters.AddWithValue("@VerifiedDate", VerifiedDate);
                        cmd.Parameters.AddWithValue("@TrainingProvidedBy", TrainingProvidedBy);
                        cmd.Parameters.AddWithValue("@TypeOfAssessment", TypeOfAssessment);

                        cmd.Parameters.AddWithValue("@ExpectationForCompetence", ExpectationForCompetence);
                        cmd.Parameters.AddWithValue("@OutcomeStatus", OutcomeStatus);
                        cmd.Parameters.AddWithValue("@Comment", Comment);
                        cmd.Parameters.AddWithValue("@FileName", "");
                        cmd.Parameters.AddWithValue("@Path", "");
                        conn2.Open();
                        cmd.ExecuteNonQuery();
                        conn2.Close();


                    }


                }
            }
           

        }



        [WebMethod]
        //Update Product Info

        public void EditProduct(int Id, string ProductType, string ProductSubType, string TrainingType, int DepartmentId)
        {


            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn2;



                    cmd.CommandText = "usp_UProductInfo";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@ProductType", ProductType);
                    cmd.Parameters.AddWithValue("@ProductSubType", ProductSubType);
                    cmd.Parameters.AddWithValue("@TrainingType", TrainingType);
                    cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);


                    conn2.Open();
                    cmd.ExecuteNonQuery();
                    conn2.Close();
                }
            }
        }




        //Employee Drop down
        [WebMethod]
        public List<Names> GetDropList()
        {
            List<Names> names = new List<Names>();
            //DataSet ds = new DataSet();

            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);



            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = "sp_GetEmpDropList";
                cmd.Connection = conn2;
                cmd.CommandType = CommandType.StoredProcedure;
                conn2.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    Names name = new Names();

                    name.EmployeeID = Convert.ToInt32(read["EmployeeID"]);
                    name.FirstName = Convert.ToString(read["FirstName"]);
                    name.LastName = Convert.ToString(read["LastName"]);




                    names.Add(name);

                }


            }

            return names;


        }

        //Product Drop down
        [WebMethod]

        public List<Name> GetProduct()
        {
            List<Name> name = new List<Name>();
            DataSet ds = new DataSet();

            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn2;
                    cmd.CommandText = "select Id,ProductType,ProductSubType from tblProduct ;";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)

                    // names.Add(new Names(int.Parse(dr["Id"].ToString()), dr["FirstName"].ToString(), dr["LastName"].ToString(), dr["Gender"].ToString(), dr["Title"].ToString(), dr["Address"].ToString(), dr["ContactNumber"].ToString()));
                    name.Add(new Name(int.Parse(dr["Id"].ToString()), dr["ProductType"].ToString(), dr["ProductSubType"].ToString()));



            }

            return name;
        }



        //Get department Id
        [WebMethod]
        public List<Department> GetDepartmentList()
        {
            List<Department> departments = new List<Department>();
            //DataSet ds = new DataSet();

            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);



            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = "usp_GetDepartment";
                cmd.Connection = conn2;
                cmd.CommandType = CommandType.StoredProcedure;
                conn2.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    Department department = new Department();

                    department.Id = Convert.ToInt32(read["Id"]);
                    department.Name = Convert.ToString(read["Name"]);

                    departments.Add(department);
                }
            }
            return departments;
        }





        //Add product
        [WebMethod]
        public void AddProduct(string ProductType, string ProductSubType, string TrainingType, int DepartmentId/*, DateTime LogDate*/)
        {
            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn2;

                    //cmd.CommandText = "insert into TrainingInfo (EmpId,ProductId,DateCompleted,Verified,TrainingProviderBy,TypeOfAssessment,Expectation,OutcomeStatus,Comment)" +
                    //    " values (@EmpId,@ProductId,@DateCompleted,@Verified,@TrainingProviderBy,@TypeOfAssessment,@Expectation,@OutcomeStatus,@Comment);";

                    cmd.CommandText = "usp_IProduct";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductType", ProductType);
                    cmd.Parameters.AddWithValue("@ProductSubType", ProductSubType);
                    cmd.Parameters.AddWithValue("@TrainingType", TrainingType);
                    cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                    //cmd.Parameters.AddWithValue("@LogDate", LogDate);

                    conn2.Open();
                    cmd.ExecuteNonQuery();
                    conn2.Close();


                }


            }

        }




        [WebMethod]
        public List<Name> GetProductType()
        {
            List<Name> name = new List<Name>();
            DataSet ds = new DataSet();

            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn2;
                    cmd.CommandText = "select Id,ProductType from tblProduct ;";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)

                    // names.Add(new Names(int.Parse(dr["Id"].ToString()), dr["FirstName"].ToString(), dr["LastName"].ToString(), dr["Gender"].ToString(), dr["Title"].ToString(), dr["Address"].ToString(), dr["ContactNumber"].ToString()));
                    name.Add(new Name(int.Parse(dr["Id"].ToString()), dr["ProductType"].ToString()));



            }

            return name;


        }


        //[WebMethod]
        //public void SaveImage(FileData fileData/*,int TrainingID*/)
        //{
        //    if (!string.IsNullOrEmpty(fileData.FileName))
        //    {

        //        byte[] imageBytes = Convert.FromBase64String(fileData.Data);
        //        string filePath = "~/Images/" + fileData.FileName;
        //        System.IO.File.WriteAllBytes(Server.MapPath(filePath), imageBytes);

        //        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        //        {

        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = conn;



        //                cmd.CommandText = "usp_IFile";
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@FileName", fileData.FileName);
        //                cmd.Parameters.AddWithValue("@Path", filePath);
        //                //cmd.Parameters.AddWithValue("@TrainingID", TrainingID);
        //                conn.Open();
        //                cmd.ExecuteNonQuery();
        //                conn.Close();
        //            }
        //        }
        //    }
        //}

        public class FileData
        {
            public string FileName { get; set; }
            public string Data { get; set; }
        }




    }
}