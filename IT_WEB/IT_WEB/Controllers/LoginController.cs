using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT_WEB.Models;
using IT_WEB.Models.User_Model;

namespace IT_WEB.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Validate(FormCollection collection)
        {
            UserModel user = new UserModel();

            user.Email = collection["username"];
            user.Password = collection["password"];

            using (IT_Assets_Entities data = new IT_Assets_Entities())
            {
                var list = from table in data.T_ADMINS
                           where table.EMAIL == user.Email && table.PASSWORD == user.Password
                           select table;

                if (list.Count() > 0)
                {
                    Session["User"] = list.First();
                    // User found
                    return Content("1");
                }
                else
                {
                    // User does not exist
                    return Content("2");
                }
            }
        }
    }
}