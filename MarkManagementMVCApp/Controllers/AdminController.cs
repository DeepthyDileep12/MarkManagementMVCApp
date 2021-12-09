using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using MarkManagementMVCApp.Models;
namespace MarkManagementMVCApp.Controllers
{
    public class AdminController : Controller
    {
        Faculty f = new Faculty();
        Student s = new Student();
        private List<ClassPage> lst = new List<ClassPage>();
        private List<StudentPage> slst = new List<StudentPage>();
        private List<FacultyPage> flst = new List<FacultyPage>();
        private List<Mapping> mlst = new List<Mapping>();
        aDMIN ad = new aDMIN();
        // GET: Admin
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
            //else
            //{
            //    ViewBag.info = "";
            //    return View();
            //}
        }


        public ActionResult AdminHome()
        {
            return View();
        }
        public ActionResult AdminClass()
        {

            SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
            SqlDataAdapter adpt = new SqlDataAdapter("select * from ClassInfo order by ClassID asc", sqlcon);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "cls");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ClassPage c = new ClassPage();
                c.ClassID = int.Parse(ds.Tables[0].Rows[i]["ClassID"].ToString());
                c.ClassName = ds.Tables[0].Rows[i]["ClassName"].ToString();
                lst.Add(c);
            }
            return View(lst);
        }
        public ActionResult addclass()
        {

            return View();

        }
        [HttpPost]
        public ActionResult addclass(ClassDemo c)
        {
            //if (ModelState.IsValid)
            //{
                bool b = ad.Add(c);
                if (b == true)
                {
                    return RedirectToAction("AdminClass");
                }
                else
                {
                    ViewBag.info = "Already Present";
                    return View();
                }
                
            //}
            //return View();


        }
        public ActionResult delete(int id)
        {

            SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
            SqlDataAdapter adpt = new SqlDataAdapter("delete from ClassInfo where ClassID='" + id + "'", sqlcon);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "cls");
            return RedirectToAction("AdminClass");
        }
        public ActionResult AdminEdit(int id)
        {
            ClassDemo c = new ClassDemo();
            SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
            SqlDataAdapter adpt = new SqlDataAdapter("select * from ClassInfo where  ClassID='" + id + "'", sqlcon);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "cls");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                c.ClassID = int.Parse(ds.Tables[0].Rows[i]["ClassID"].ToString());
                c.ClassName = ds.Tables[0].Rows[i]["ClassName"].ToString();

            }

            return View(c);
        }
        [HttpPost]
        public ActionResult AdminEdit(int id,ClassDemo c)
        {
            //if (ModelState.IsValid)
            //{
                bool b = ad.Edit(id, c);
                if (b == true)
                    return RedirectToAction("AdminClass");
                else
                {
                    ViewBag.info = "Already Present";
                    return View();
                }
            //}
            //return View();
           
        }
        public ActionResult AdminStudent()
        {
            StudentPage sp = new StudentPage();
            sp.ClassDropdown = new SelectList(ad.GetClassData(), "ClassID", "ClassName");

            return View(sp);
        }
        [HttpPost]
        public ActionResult AdminStudent(StudentPage s)
        {
           
                bool b = ad.Addstudent(s);
                if (b == true)
                {
                    s.ClassDropdown = new SelectList(ad.GetClassData(), "ClassID", "ClassName");
                    ViewBag.info = "Added Successfully";
                    return RedirectToAction("ShowStudents");
                }
                else
                {
                    s.ClassDropdown = new SelectList(ad.GetClassData(), "ClassID", "ClassName");
                    ViewBag.info = "There is some errors!!Please try again.";
                    return View(s);
                }
            
            //else
            //{
            //    ViewBag.info = "";
            //    s.ClassDropdown = new SelectList(ad.GetClassData(), "ClassID", "ClassName");
            //    return View(s);
            //}
        }
        public ActionResult ShowStudents()
        {
            SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
            SqlDataAdapter adpt = new SqlDataAdapter("Select s.Rollno,s.StdName,s.Gender,s.Address,s.Location,s.Phone,s.Email,s.Password,c.ClassName from StudentReg as s inner join ClassInfo as c on c.ClassID=s.ClassID ", sqlcon);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "cls");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                StudentPage s = new StudentPage();
                s.Rollno = (ds.Tables[0].Rows[i]["Rollno"].ToString());
                s.StdName = ds.Tables[0].Rows[i]["StdName"].ToString();
                s.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                s.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                s.Location = ds.Tables[0].Rows[i]["Location"].ToString();
                s.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                s.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                s.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                s.ClassName = (ds.Tables[0].Rows[i]["ClassName"].ToString());
                slst.Add(s);
            }
            return View(slst);
        }
        public ActionResult AdminFaculty()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminFaculty(FacultyPage f)
        {
            //if (ModelState.IsValid)
            //{

                bool b = ad.Addfaculty(f);
                if (b == true)
                {
                    ViewBag.info = "Added Successfully";
                    return RedirectToAction("ShowFaculty");
                }
                else
                {
                    ViewBag.info = "There is some errors!!Please try again.";
                    return View(f);
                }
            //}
            //return View();
            
        }
        public ActionResult ShowFaculty()
        {
            SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
            SqlDataAdapter adpt = new SqlDataAdapter("select * from FacultyInfo", sqlcon);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "cls");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                FacultyPage s = new FacultyPage();
                s.Fid = (ds.Tables[0].Rows[i]["Fid"].ToString());
                s.FactName = ds.Tables[0].Rows[i]["FactName"].ToString();
                s.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                s.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                s.Location = ds.Tables[0].Rows[i]["Location"].ToString();
                s.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                s.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                s.Password = ds.Tables[0].Rows[i]["Password"].ToString();

                flst.Add(s);
            }
            return View(flst);
        }
        public ActionResult AdminMap()
        {
            Mapping f = new Mapping();
            f.ClassDropdown = new SelectList(ad.GetData(), "Fid", "FactName");
            f.FactDropdown = new SelectList(ad.GetClassData(), "ClassID", "ClassName");
            return View(f);
        }
        [HttpPost]
        public ActionResult AdminMap(Mapping m)
        {
            //if (ModelState.IsValid==true)
            //{
                bool b = ad.map(m);

                if (b == true)
                {
                    m.ClassDropdown = new SelectList(ad.GetData(), "Fid", "FactName");
                    m.FactDropdown = new SelectList(ad.GetClassData(), "ClassID", "ClassName");
                    ViewBag.info = "Faculty Mapped Successfully";
                    return RedirectToAction("ShowMap");
                }

                else
                {
                    m.ClassDropdown = new SelectList(ad.GetData(), "Fid", "FactName");
                    m.FactDropdown = new SelectList(ad.GetClassData(), "ClassID", "ClassName");
                    ViewBag.info = "Faculty Already Mapped";
                    return View(m);
                }
            //}
            //m.ClassDropdown = new SelectList(ad.GetData(), "Fid", "FactName");
            //m.FactDropdown = new SelectList(ad.GetClassData(), "ClassID", "ClassName");
            //return View(m);
            
        }
        public ActionResult ShowMap()
        {
            SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
            SqlCommand mcmd = new SqlCommand("Select c.ClassName,f.FactName from ClassInfo as c inner join ClassFaculty as cf on cf.ClassID=c.ClassID inner join FacultyInfo as f on cf.Fid=f.Fid", sqlcon);
            SqlDataAdapter adpt = new SqlDataAdapter(mcmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "cls");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Mapping s = new Mapping();
                s.FactName = (ds.Tables[0].Rows[i]["ClassName"].ToString());
                s.ClassName = (ds.Tables[0].Rows[i]["FactName"].ToString());


                mlst.Add(s);
            }
            ViewBag.info = "No Faculty Mapped Yet!!";
            return View(mlst);
        }
    }
}
