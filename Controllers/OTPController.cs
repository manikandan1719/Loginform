using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPMS1.Controllers
{
    public class OTPController : Controller
    {
        // GET: OTP
        Utilise obj = new Utilise();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CBMELKSConn"].ConnectionString);
        string MainCon = ConfigurationManager.ConnectionStrings["CBMELKSConn"].ToString();

        public ActionResult otpIndex()
        {
            return View();
        }
    }
}