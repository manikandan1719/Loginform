using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace EPMS1.Controllers
{
    public class LoginController : Controller
    {
        Utilise obj = new Utilise();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CBMELKSConn"].ConnectionString);
        string MainCon = ConfigurationManager.ConnectionStrings["CBMELKSConn"].ToString();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult checkLoginInfo(User2 LoginDetails)
        {
            var result = new { success = false, message = "" };
            string hashpassword = obj.Encrypt(LoginDetails.UserPassword);

            try {
                using (SqlConnection con = new SqlConnection(MainCon)) {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("[CheckOrInsertUser]", con)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", LoginDetails.Username);

                      
                       
                        cmd.Parameters.AddWithValue("@Password", hashpassword);
                        cmd.Parameters.AddWithValue("@Email", LoginDetails.useremail);

                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows) {
                            reader.Read();
                            string status = reader["Status"].ToString();

                            if (status == "User exists") {
                                result = new { success = true, message = "WELCOME USER!!" };
                            }
                            else {
                                result = new { success = false, message = "Account does not exist. Please register." };
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                result = new { success = false, message = ex.Message };
            }

            return Json(result);
        }

       

        public class User2
        {
            public int id { get; set; }
            public string Username { get; set; }
            public string UserPassword { get; set; }
            public string useremail { get; set; }
        }
    }
}
