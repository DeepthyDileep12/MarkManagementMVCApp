using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace MarkManagementMVCApp.Models
{
    public class Student
    {
        SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");

        public DataSet showStud(string rno)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter madpt = new SqlDataAdapter("select f.Rollno,f.StdName,f.Gender,f.Address,f.Location,f.Phone,f.Email,f.Password,c.ClassName,s.FactName from StudentReg as f inner join ClassFaculty as cf on cf.ClassID=f.ClassID inner join ClassInfo as c on c.ClassID=cf.ClassID inner join FacultyInfo as s on s.Fid=cf.Fid where f.Rollno='" + rno + "' ", sqlcon);
                madpt.Fill(ds, "can");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return ds;
        }
        public DataSet logincheck(string uname, string pwd)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter madpt = new SqlDataAdapter("select f.Rollno,f.StdName,f.Gender,f.Address,f.Location,f.Phone,f.Email,f.Password,cf.ClassName from StudentReg as f inner join ClassInfo as cf on cf.ClassID=f.ClassID  where f.Email='" + uname + "' and f.Password='" + pwd + "'", sqlcon);
                madpt.Fill(ds, "can");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return ds;
        }
        public DataSet studentMark(string id)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("select s.StdName,m.ExamType,m.Science,m.Maths,m.Computer,m.Total,m.Average,m.Grade from StudentReg as s inner join MarksInfo as m on s.RollNo=m.RollNo where m.RollNo='" + id + "' ", sqlcon);
                adpt.Fill(ds, "mk");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return ds;

        }
        public bool changePassword(string rno, string pwd)
        {
            bool b = false;
            try
            {
                sqlcon.Open();
                SqlCommand cmdAddCls = new SqlCommand("Update StudentReg set Password=@pwd where Rollno=@rno ", sqlcon);
                cmdAddCls.Parameters.AddWithValue("@pwd", pwd);
                cmdAddCls.Parameters.AddWithValue("@rno", rno);
                cmdAddCls.ExecuteNonQuery();
                b = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                b = false;
            }
            finally
            {
                sqlcon.Close();
            }
            return b;
        }
    }
}