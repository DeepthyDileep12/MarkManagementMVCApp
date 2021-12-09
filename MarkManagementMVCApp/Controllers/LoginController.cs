using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarkManagementMVCApp.Models;
using System.Data;
namespace MarkManagementMVCApp.Controllers
{
    public class LoginController : Controller
    {
        Faculty f = new Faculty();
        Student s = new Student();
        // GET: Login
        public ActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPage(login l)
        {
            if (ModelState.IsValid)
            {
                if (l.username == "admin" && l.password == "admin" && l.usertype == "Admin")
                {
                    return RedirectToAction("AdminHome", "Admin");
                }
                else if (l.usertype == "Faculty")
                {
                    DataSet ds = f.logincheck(l.username, l.password);
                    if ((ds.Tables["fac"].Rows.Count == 1))
                    {
                        Session["name"] = ds.Tables["fac"].Rows[0]["FactName"].ToString();
                        Session["Email"] = ds.Tables["fac"].Rows[0]["Email"].ToString();
                        Session["Password"] = ds.Tables["fac"].Rows[0]["Password"].ToString();
                        Session["Fid"] = ds.Tables["fac"].Rows[0]["Fid"].ToString();
                        return RedirectToAction("FacultyHome", "Faculty");
                    }
                    else
                    {
                        ViewBag.info = "Faculty Have not assigned any class";
                    }
                }
                else if (l.usertype == "Student")
                {
                    DataSet ds = s.logincheck(l.username, l.password);
                    if ((ds.Tables["can"].Rows.Count == 1))
                    {

                        Session["name"] = ds.Tables["can"].Rows[0]["ClassName"].ToString();
                        Session["Email"] = ds.Tables["can"].Rows[0]["Email"].ToString();
                        Session["Password"] = ds.Tables["can"].Rows[0]["Password"].ToString();
                        Session["rno"] = ds.Tables["can"].Rows[0]["Rollno"].ToString();
                        return RedirectToAction("StudentHome", "Student");
                    }
                    else
                    {
                        ViewBag.info = "Please Check the credentials";
                    }
                }
                else
                {
                    ViewBag.info = "Please Check the credentials";
                }

                return View();
            }
            else
            {
                ViewBag.info = "";
                return View();
            }
        }

    }
}