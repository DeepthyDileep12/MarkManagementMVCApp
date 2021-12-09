using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarkManagementMVCApp.Models;
using System.Data;
using System.Data.SqlClient;
namespace MarkManagementMVCApp.Controllers
{
    public class FacultyController : Controller
    {       
        Faculty f = new Faculty();
        Student s = new Student();
        List<FacultyPage> flst = new List<FacultyPage>();
        List<StudentPage> slst = new List<StudentPage>();
        List<Mark> mlst = new List<Mark>();

        // GET: Faculty
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(login l)
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
            return View();
        }


        public ActionResult FacultyHome(FacultyPage fp)
        {
            // "select f.Fid,f.FactName,f.Gender,f.Address,f.Location,f.Phone,f.Email,f.Password,c.ClassName from FacultyInfo as f inner join ClassFaculty as cf on cf.Fid=f.Fid inner join ClassInfo as c on c.ClassID=cf.ClassID where f.Email='" + uname + "' and f.Password='" + pwd + "'", sqlcon);
            string name = Session["Email"].ToString();
            string pwd = Session["Password"].ToString();
            DataSet ds = f.logincheck(name, pwd);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                fp.Fid= ds.Tables[0].Rows[i]["Fid"].ToString();
                fp.FactName = ds.Tables[0].Rows[i]["FactName"].ToString();
                fp.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                fp.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                fp.Location = ds.Tables[0].Rows[i]["Location"].ToString();
                fp.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                fp.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                fp.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                fp.ClassName = ds.Tables[0].Rows[i]["ClassName"].ToString();
                flst.Add(fp);
            }
            return View(flst);
        }
        public ActionResult FacultyStudent()
        {
            string id = Session["Fid"].ToString();
            DataSet ds = new DataSet();
            ds = f.studentInfo(id);
            if ((ds.Tables["can"].Rows.Count > 0))
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StudentPage sp = new StudentPage();
                    sp.Rollno = ds.Tables[0].Rows[i]["Rollno"].ToString();
                    sp.StdName = ds.Tables[0].Rows[i]["StdName"].ToString();
                    sp.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    sp.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    sp.Location = ds.Tables[0].Rows[i]["Location"].ToString();
                    sp.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                    sp.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                    sp.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                    slst.Add(sp);
                }
                return View(slst);
            }
            else
            {
                ViewBag.info = "No Student Added Yet!";
                return View(slst);
            }

        }
        public ActionResult FacultyPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FacultyPassword(FacultyPage fp)
        {
            //if (ModelState.IsValid)
            //{
                string id = Session["Fid"].ToString();
            if (fp.CPassword == fp.NPassword)
            {
                bool k = f.changePassword(id, fp.CPassword);
                if (k == true)
                {
                    ViewBag.info = "Changed SuccessFully";
                    Session["Password"] = fp.CPassword;
                }
                return View();
            }
            else
            {
                return View();
            }
            //}
            //return View();
        }
        public ActionResult FacultyMark()
        {
            string id = Session["Fid"].ToString();
            Mark m = new Mark();
            m.ClassDropdown = new SelectList(f.ddlclassId(id), "ClassID", "ClassName");
            m.StdDropdown = new SelectList(f.studentdata(id), "Rollno", "StdName");
            return View(m);
        }
        [HttpPost]
        public ActionResult FacultyMark(Mark m)
        {
            string id = Session["Fid"].ToString();

            //if (ModelState.IsValid)
            //{
                DataSet ds = f.Addmark(id);
                if ((ds.Tables["fac"].Rows.Count == 1))
                {
                    Session["Sname"] = ds.Tables["fac"].Rows[0]["StdName"].ToString();

                }
                if (f.Check(m.RollNo, m.ExamType) == true)
                {
                    bool b = f.add(m, id);
                    if (b == true)
                    {
                        m.ClassDropdown = new SelectList(f.ddlclassId(id), "ClassID", "ClassName");
                        m.StdDropdown = new SelectList(f.studentdata(id), "Rollno", "StdName");
                        ViewBag.info = "Mark Updated Successfully";
                        return View(m);
                    }
                    else
                    {
                        m.ClassDropdown = new SelectList(f.ddlclassId(id), "ClassID", "ClassName");
                        m.StdDropdown = new SelectList(f.studentdata(id), "Rollno", "StdName");
                        ViewBag.info = "Mark is already added";
                        return View(m);
                    }


                }
                ViewBag.info = "Mark is already added";
                return View();
            //}
            //m.ClassDropdown = new SelectList(f.ddlclassId(id), "ClassID", "ClassName");
            //m.StdDropdown = new SelectList(f.studentdata(id), "Rollno", "StdName");
            //return View(m);
        }
        public ActionResult FacultyShowMark()
        {
            string id = Session["Fid"].ToString();       
            DataSet ds = f.showMark(id);
            if ((ds.Tables["mk"].Rows.Count >0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Mark sp = new Mark();
                    sp.ExamType = ds.Tables[0].Rows[i]["ExamType"].ToString();
                    sp.StdName = ds.Tables[0].Rows[i]["StdName"].ToString();
                    sp.Science = int.Parse(ds.Tables[0].Rows[i]["Science"].ToString());
                    sp.Maths = int.Parse(ds.Tables[0].Rows[i]["Maths"].ToString());
                    sp.Computer = int.Parse(ds.Tables[0].Rows[i]["Computer"].ToString());
                    sp.Total = int.Parse(ds.Tables[0].Rows[i]["Total"].ToString());
                    sp.Average = int.Parse(ds.Tables[0].Rows[i]["Average"].ToString());
                    sp.Grade = ds.Tables[0].Rows[i]["Grade"].ToString();
                    mlst.Add(sp);
                }
                return View(mlst);

            }
            else
            {
                ViewBag.info = "No Mark Added Yet!";
                return View(mlst);
            }

        }
    }
}