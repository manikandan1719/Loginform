using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace EPMS1.Controllers
{
    public class updateController : Controller
    {
        // GET: update
        Utilise obj = new Utilise();
        string MainCon = ConfigurationManager.ConnectionStrings["CBMELKSConn"].ToString();

        public ActionResult update()
        {
            return View();
        }

        [HttpPost]
        public string userdetails(User3 LoginDetails)
        {
            string result = "";
            string hashpassword = obj.Encrypt(LoginDetails.regpassword);

            try {
                using (SqlConnection con = new SqlConnection(MainCon)) {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("[UpdateUserDetails]", con)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Firstname", LoginDetails.firstname);
                        cmd.Parameters.AddWithValue("@Lastname", LoginDetails.lastname);
                        cmd.Parameters.AddWithValue("@Gender", LoginDetails.Gender);
                        cmd.Parameters.AddWithValue("@Email", LoginDetails.Email);
                        cmd.Parameters.AddWithValue("@Age", LoginDetails.age);
                        cmd.Parameters.AddWithValue("@Phone", LoginDetails.Phonenumber);
                        cmd.Parameters.AddWithValue("@RegPassword", hashpassword);

                        cmd.ExecuteNonQuery();
                    }
                }
                result = "User details updated successfully.";
            }
            catch (Exception ex) {
                result = ex.Message;
            }

            return result;
        }

        public class User3
        {
            public int id { get; set; }
            public string Username { get; set; }
            public string UserPassword { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string Gender { get; set; }
            public int age { get; set; }
            public string regpassword { get; set; }
            public string Email { get; set; }
            public string Phonenumber { get; set; }
        }
    }
}
