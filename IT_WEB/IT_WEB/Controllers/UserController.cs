using IT_WEB.Data;
using IT_WEB.Models;
using IT_WEB.Models.Combo_Model;
using IT_WEB.Models.User_Model;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT_WEB.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index()
        {
            try
            {
                var user = (T_ADMINS)Session["User"];
                List<UserModel> list = null;

                using (IT_Assets_Entities dataBase = new IT_Assets_Entities())
                    list = (from data in dataBase.T_ADMINS
                            where user.EMAIL == data.EMAIL
                            select new UserModel
                            {
                                Name = data.NAME,
                                Lastname = data.LAST_NAME,
                                Email = data.EMAIL,
                                Password = data.PASSWORD,
                                Office = data.OFFICE
                            }).ToList();

                return View(list);
            }
            catch (Exception ex) 
            {
                return View(ex.Message);    
            }
            
        }

        [HttpPost]
        public ActionResult UpdateInformation(FormCollection collection)
        {
            C_Data data = new C_Data(); 
            UserModel model = new UserModel();

            model.Name = collection["name"];
            model.Lastname = collection["last_name"];
            model.Email = collection["email"];
            model.Password = collection["password"];

            if(model.Name.IsNullOrWhiteSpace() || model.Lastname.IsNullOrWhiteSpace() || model.Email.IsNullOrWhiteSpace() ||
                model.Password.IsNullOrWhiteSpace())
            {
                return Content("3");

            }
            else
            {
                string response = data.updateUser(model.Email, model.Name, model.Lastname, model.Password);
                return Content(response);
            }
            
        }

    }
}