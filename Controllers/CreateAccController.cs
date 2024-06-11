using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace EPMS1.Controllers
{
    public class CreateAccController : Controller
    {
        Utilise obj = new Utilise();
        string MainCon = ConfigurationManager.ConnectionStrings["CBMELKSConn"].ToString();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string signup(User1 LoginDetails)
        {
            string result = "";
            string phone = LoginDetails.Phonenumber;
            string hashpassword = obj.Encrypt(LoginDetails.regpassword);

            try {
                using (SqlConnection con = new SqlConnection(MainCon)) {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("[AddUser]", con)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Firstname", LoginDetails.firstname);
                        cmd.Parameters.AddWithValue("@Lastname", LoginDetails.lastname);
                        cmd.Parameters.AddWithValue("@Gender", LoginDetails.Gender);
                        cmd.Parameters.AddWithValue("@Email", LoginDetails.Email);
                        cmd.Parameters.AddWithValue("@Age", LoginDetails.age);
                        cmd.Parameters.AddWithValue("@Phone", LoginDetails.Phonenumber);
                        cmd.Parameters.AddWithValue("@RegPassword", hashpassword);

                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.Read()) {
                                result = reader["Message"].ToString();
                            }
                        }
                    }
                }

                if (result == "User inserted successfully.") {
                    // Send email if the email address is provided
                    if (!string.IsNullOrEmpty(LoginDetails.Email)) {
                        string emailBody = $"<p>Hi {LoginDetails.firstname},</p><p>Your email is verified and registered successfully. To login, please visit our login page.</p><p><a href='https://localhost:44337/Login/index'>Click here to login</a><br><br></p> <p>Regards,<br> Manikandan N</p>";
                        var emailResult = SendEmail(LoginDetails.Email, emailBody, "Email Verification");
                        if (emailResult != "Success") {
                            result += " However, there was an issue sending the email: " + emailResult;
                        }
                    }
                }
            }
            catch (Exception ex) {
                result = ex.Message;
            }

            return result;
        }

        private string SendEmail(string toEmail, string body, string subject)
        {
            string result = "";
            try {
                string fromEmail = ConfigurationManager.AppSettings["FromEmail"];
                string password = ConfigurationManager.AppSettings["EmailPassword"];
                string host = "smtp.gmail.com";
                int port = 587;

                using (MailMessage mm = new MailMessage(fromEmail, toEmail)) {
                    mm.Subject = subject;
                    mm.Body = body;
                    mm.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient {
                        Host = host,
                        EnableSsl = true, // Ensure SSL is enabled
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromEmail, password),
                        Port = port
                    };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Ensure TLS 1.2 or higher

                    smtp.Send(mm);
                    result = "Success";
                }
            }
            catch (SmtpException smtpEx) {
                result = smtpEx.Message;
            }
            catch (Exception ex) {
                result = ex.Message;
            }
            return result;
        }

        public class User1
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
