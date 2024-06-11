using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

public class homepageController : Controller
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CBMELKSConn"].ConnectionString);
    string MainCon = ConfigurationManager.ConnectionStrings["CBMELKSConn"].ToString();

    // GET: homepage
    public ActionResult homepage()
    {
        string email = Session["UserEmail"] as string;
        ViewBag.Email = email;
        return View();
    }

    [HttpGet]
    public JsonResult GetUserDetails(string email)
    {
        var user = new User2();
        try {
            using (SqlConnection con = new SqlConnection(MainCon)) {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("homeuser", con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows) {
                        reader.Read();
                        user = new User2 {
                            Username = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            useremail = reader["Email"].ToString(),
                            Age = int.Parse(reader["Age"].ToString()),
                            PhoneNumber = reader["Phonenumber"].ToString(),
                            UserPassword = reader["Password!"].ToString(),
                        };
                    }
                }
            }
        }
        catch (Exception ex) {
            return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        }
        return Json(new { success = true, userDetails = user }, JsonRequestBehavior.AllowGet);
    }

    public class User2
    {
        public string Username { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string useremail { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string UserPassword { get; set; }
    }
}
