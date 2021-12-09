using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace MarkManagementMVCApp.Models
{
    public class Faculty
    {
        public DataSet logincheck(string uname, string pwd)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                SqlDataAdapter madpt = new SqlDataAdapter("select f.Fid,f.FactName,f.Gender,f.Address,f.Location,f.Phone,f.Email,f.Password,c.ClassName from FacultyInfo as f inner join ClassFaculty as cf on cf.Fid=f.Fid inner join ClassInfo as c on c.ClassID=cf.ClassID  where f.Email='" + uname + "' and f.Password='" + pwd + "'", sqlcon);
                madpt.Fill(ds, "fac");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return ds;
        }
        public DataSet studentInfo(string id)

        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                SqlDataAdapter madpt = new SqlDataAdapter("select s.Rollno,s.StdName,s.Gender,s.Address,s.Location,s.Phone,s.Email,s.Password from StudentReg as s inner join ClassFaculty as c on s.ClassID=c.ClassID where Fid='" + id + "'", sqlcon);
                madpt.Fill(ds, "can");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return ds;
        }
        public bool changePassword(string id, string pwd)
        {
            bool b = false;
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                sqlcon.Open();
                SqlCommand cmdAddCls = new SqlCommand("Update FacultyInfo set Password=@pwd where Fid=@fid ", sqlcon);
                cmdAddCls.Parameters.AddWithValue("@pwd", pwd);
                cmdAddCls.Parameters.AddWithValue("@fid", id);
                cmdAddCls.ExecuteNonQuery();
                b = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                b = false;
            }
            return b;
        }
        public bool Check(string rno, string type)
        {
            bool chk = true;
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                SqlDataAdapter Check = new SqlDataAdapter("Select * from MarksInfo", sqlcon);
                DataSet dsCk = new DataSet();
                Check.Fill(dsCk, "check");

                foreach (DataRow dr in dsCk.Tables["check"].Rows)
                {
                    if (dr["RollNo"].ToString().Equals(rno) == true && dr["ExamType"].ToString().Equals(type) == true)
                    {
                        chk = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return chk;
        }
        public DataSet Addmark(string id)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                SqlDataAdapter madpt = new SqlDataAdapter("select s.StdName from StudentReg  as s inner join MarksInfo as m on m.Rollno=s.Rollno inner join FacultyInfo as f on f.Fid=m.Fid where f.Fid='" + id + "'", sqlcon);
                madpt.Fill(ds, "fac");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        public DataSet showMark(string id)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                SqlDataAdapter adpt = new SqlDataAdapter("select s.StdName,m.ExamType,m.Science,m.Maths,m.Computer,m.Total,m.Average,m.Grade from StudentReg as s inner join MarksInfo as m on s.RollNo=m.RollNo where m.Fid='" + id  + "'",sqlcon);
                adpt.Fill(ds, "mk");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return ds;

        }
        public bool add(Mark m,string id)
        {
            bool b = false;
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                sqlcon.Open();
                SqlCommand mcmd = new SqlCommand("insert into MarksInfo (ExamType,Science,Maths,Computer,Total,Average,Grade,RollNo ,ClassID,Fid) values(@etype, @sci, @mts, @cs, @total, @avg, @grd, @rno, @cid, @fid)", sqlcon);
                mcmd.Parameters.AddWithValue("@etype", m.ExamType);
                mcmd.Parameters.AddWithValue("@sci", m.Science);
                mcmd.Parameters.AddWithValue("@mts", m.Maths);
                mcmd.Parameters.AddWithValue("@cs", m.Computer);
                m.Total = m.Science + m.Maths + m.Computer;
                mcmd.Parameters.AddWithValue("@total", m.Total);
                m.Average = m.Total / 3;
                mcmd.Parameters.AddWithValue("@avg", m.Average);
                if (m.Average > 48 && m.Average <= 60)
                    m.Grade = "A";
                else if (m.Average > 36 && m.Average <= 48)
                    m.Grade = "B";
                else if (m.Average > 24 && m.Average <= 36)
                    m.Grade = "C";
                else if (m.Average >= 12 && m.Average <= 24)
                    m.Grade = "D";
                else
                    m.Grade = "E";
                mcmd.Parameters.AddWithValue("@grd", m.Grade);
                mcmd.Parameters.AddWithValue("@rno", m.RollNo);
                mcmd.Parameters.AddWithValue("@cid", m.ClassID);
                m.Fid = id;
                mcmd.Parameters.AddWithValue("@fid", m.Fid);
                mcmd.ExecuteNonQuery();
                b = true;
            }
            catch(Exception e)
            {
                b = false;
            }
            return b;
        }
        public IEnumerable<ClassPage> ddlclassId(string id)
        {
            List<ClassPage> lst = new List<ClassPage>();
            DataSet ds = new DataSet();
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                SqlDataAdapter adpt = new SqlDataAdapter("select cf.ClassID,c.ClassName from ClassInfo as c inner join ClassFaculty as cf on cf.ClassID=c.ClassID where cf.Fid='" + id + "'", sqlcon);
                adpt.Fill(ds, "cf");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ClassPage c = new ClassPage();
                    c.ClassID = int.Parse(ds.Tables[0].Rows[i]["ClassID"].ToString());
                    c.ClassName = ds.Tables[0].Rows[i]["ClassName"].ToString();
                    lst.Add(c);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lst;
        }
        public IEnumerable<StudentPage> studentdata(string id)
        {
            List<StudentPage> slst = new List<StudentPage>();
            DataSet ds = new DataSet();
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                SqlDataAdapter madpt = new SqlDataAdapter("select s.Rollno,s.StdName,s.Gender,s.Address,s.Location,s.Phone,s.Email,s.Password from StudentReg as s inner join ClassFaculty as c on s.ClassID=c.ClassID where Fid='" + id + "'", sqlcon);
                madpt.Fill(ds, "can");
                for (int i = 0; i < ds.Tables["can"].Rows.Count; i++)
                {
                    StudentPage s = new StudentPage();
                    s.Rollno = (ds.Tables["can"].Rows[i]["Rollno"].ToString());
                    s.StdName = ds.Tables["can"].Rows[i]["StdName"].ToString();
                    s.Gender = ds.Tables["can"].Rows[i]["Gender"].ToString();
                    s.Address = ds.Tables["can"].Rows[i]["Address"].ToString();
                    s.Location = ds.Tables["can"].Rows[i]["Location"].ToString();
                    s.Phone = ds.Tables["can"].Rows[i]["Phone"].ToString();
                    s.Email = ds.Tables["can"].Rows[i]["Email"].ToString();
                    s.Password = ds.Tables["can"].Rows[i]["Password"].ToString();
                    //s.ClassID = int.Parse(ds.Tables["can"].Rows[i]["ClassID"].ToString());
                    slst.Add(s);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return slst;
        }
    }
}