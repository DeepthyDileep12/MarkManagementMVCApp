using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MarkManagementMVCApp.Models;
namespace MarkManagementMVCApp.Controllers
{
    public class StudentController : Controller
    {
        List<StudentPage> slst = new List<StudentPage>();
        List<Mark> mlst = new List<Mark>();
        Faculty f = new Faculty();
        Student s = new Student();
        // GET: Student
        public ActionResult StudentHome()
        {
            //"select f.Rollno,f.StdName,f.Gender,f.Address,f.Location,f.Phone,f.Email,f.Password,c.ClassName,s.FactName from StudentReg as f inner join ClassFaculty as cf on cf.ClassID=f.ClassID inner join ClassInfo as c on c.ClassID=cf.ClassID inner join FacultyInfo as s on s.Fid=cf.Fid where f.Rollno='" + rno + "' ", sqlcon);

            string rno = Session["rno"].ToString();
            string name = Session["Email"].ToString();
            string pwd = Session["Password"].ToString();
            DataSet ds = new DataSet();
            ds = s.showStud(rno);
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
                sp.ClassName = ds.Tables[0].Rows[i]["Rollno"].ToString();
                sp.FactName = ds.Tables[0].Rows[i]["FactName"].ToString();
                slst.Add(sp);
            }
            return View(slst);
        }
        //select s.StdName,m.ExamType,m.Science,m.Maths,m.Computer,m.Total,m.Average,m.Grade from StudentReg as s inner join MarksInfo as m on s.RollNo=m.RollNo where m.RollNo='" + id + "' ", sqlcon);

        public ActionResult StudentMark()
        {
            string id = Session["rno"].ToString();
            DataSet ds = s.studentMark(id);
            if (ds.Tables["mk"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Mark mp = new Mark();
                    mp.StdName = ds.Tables[0].Rows[i]["StdName"].ToString();
                    mp.ExamType = ds.Tables[0].Rows[i]["ExamType"].ToString();
                    mp.Science = int.Parse(ds.Tables[0].Rows[i]["Science"].ToString());
                    mp.Maths = int.Parse(ds.Tables[0].Rows[i]["Maths"].ToString());
                    mp.Computer = int.Parse(ds.Tables[0].Rows[i]["Computer"].ToString());
                    mp.Total = int.Parse(ds.Tables[0].Rows[i]["Total"].ToString());
                    mp.Average = int.Parse(ds.Tables[0].Rows[i]["Average"].ToString());
                    mp.Grade = ds.Tables[0].Rows[i]["Grade"].ToString();
                    mlst.Add(mp);
                }
                return View(mlst);
            }
            else
            {
                ViewBag.info = "No Marks Added Yet!";
                return View(mlst);
            }
        }
        public ActionResult StudentPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentPassword(StudentPage sp)
        {
            string rno = Session["rno"].ToString();
            if (sp.NPassword == sp.CPassword)
            {
                bool k = s.changePassword(rno, sp.NPassword);
                if (k == true)
                {
                    ViewBag.info = "Changed SuccessFully";
                    Session["Password"] = sp.NPassword;
                }
                return View();
            }
            else
            {
                return View();                    
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(login l)
        {
            //if (ModelState.IsValid)
            //{
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
            //}
            //return View();
        }
    }
}