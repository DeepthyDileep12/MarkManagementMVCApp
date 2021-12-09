using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace MarkManagementMVCApp.Models
{
    public class aDMIN
    {
        SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
        public bool Add(ClassDemo c)
        {
            bool b = false;
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand("insert into ClassInfo(ClassName)values(@cname)", sqlcon);
                cmd.Parameters.AddWithValue("@cname", c.ClassName);
                cmd.ExecuteNonQuery();
                b= true;
            }
            catch(Exception e)
            {
                b = false;
            }
            return b;
        }
        public bool Update(ClassPage c)
        {
            bool b = false;
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("update ClassInfo set ClassName='" + c.ClassName + "'where ClassID='" + c.ClassID + "'", sqlcon);
                DataSet ds = new DataSet();
                adpt.Fill(ds, "bk");
                b = true;
            }
            catch (Exception ex)
            {
                b = false;
            }
            return b;
        }
        public bool Edit(int id, ClassDemo cd)
        {
            ClassPage c = new ClassPage();
            c.ClassID = id;
            c.ClassName = cd.ClassName;
            bool b =Update(c);
            return b;
        }
        public bool map(Mapping m)
        {
            bool b = false;
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                sqlcon.Open();
                SqlCommand mcmd = new SqlCommand("insert into ClassFaculty(ClassID,Fid) values (@cid, @fid)", sqlcon);
                mcmd.Parameters.AddWithValue("@cid", m.ClassID);
                mcmd.Parameters.AddWithValue("fid", m.Fid);
                mcmd.ExecuteNonQuery();
                b = true;
            }
            catch(Exception e)
            {
                b = false;
            }
            return b;
        }
        public bool Addstudent(StudentPage s)
        {
            bool b = false;
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                sqlcon.Open();
                SqlCommand cmdAddStd = new SqlCommand("Insert into StudentReg(Rollno,StdName,Gender,Address,Location,Phone,Email,Password,ClassID) values(@rno,@sname,@gen,@add,@loc,@ph,@mail,@pwd,@class)", sqlcon);
                cmdAddStd.Parameters.AddWithValue("@rno", s.Rollno);
                cmdAddStd.Parameters.AddWithValue("@sname", s.StdName);
                cmdAddStd.Parameters.AddWithValue("@gen", s.Gender);
                cmdAddStd.Parameters.AddWithValue("@add", s.Address);
                cmdAddStd.Parameters.AddWithValue("@loc", s.Location);
                cmdAddStd.Parameters.AddWithValue("@ph", s.Phone);
                cmdAddStd.Parameters.AddWithValue("@mail", s.Email);
                cmdAddStd.Parameters.AddWithValue("@pwd", s.Password);
                cmdAddStd.Parameters.AddWithValue("@Class", s.ClassID);
                cmdAddStd.ExecuteNonQuery();
                b = true;
            }
            catch(Exception e)
            {
                b = false;
            }
            return b;
        }
        public bool Addfaculty(FacultyPage f )
        {
            bool b = false;
            try
            {
                SqlConnection sqlcon = new SqlConnection("server=DESKTOP-2U7MC5K;database=studentdb;Integrated Security=true");
                sqlcon.Open();
                SqlCommand cmdAddFct = new SqlCommand("Insert into FacultyInfo(Fid,FactName,Gender,Address,Location,Phone,Email,Password) values(@fno,@fname,@gen,@add,@loc,@ph,@mail,@pwd)", sqlcon);
                cmdAddFct.Parameters.AddWithValue("@fno", f.Fid);
                cmdAddFct.Parameters.AddWithValue("@fname", f.FactName);
                cmdAddFct.Parameters.AddWithValue("@gen", f.Gender);
                cmdAddFct.Parameters.AddWithValue("@add", f.Address);
                cmdAddFct.Parameters.AddWithValue("@loc", f.Location);
                cmdAddFct.Parameters.AddWithValue("@ph", f.Phone);
                cmdAddFct.Parameters.AddWithValue("@mail", f.Email);
                cmdAddFct.Parameters.AddWithValue("@pwd", f.Password);
                cmdAddFct.ExecuteNonQuery();
                b = true;
            }
            catch(Exception e)
            {
                b = false;
            }
            return b;
        }
        public IEnumerable<FacultyPage> GetData()
        {
            List<FacultyPage> lst = new List<FacultyPage>();
            SqlDataAdapter data = new SqlDataAdapter("Select * from FacultyInfo", sqlcon);
            DataSet ds = new DataSet();
            data.Fill(ds, "fact");


            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                FacultyPage fp = new FacultyPage();
                fp.Fid = ds.Tables[0].Rows[i]["Fid"].ToString();
                fp.FactName = ds.Tables[0].Rows[i]["FactName"].ToString();
                fp.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                fp.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                fp.Location = ds.Tables[0].Rows[i]["Location"].ToString();
                fp.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                fp.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                fp.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                lst.Add(fp);
            }
            return lst;
        }
        public IEnumerable<ClassPage> GetClassData()
        {
            List<ClassPage> lst = new List<ClassPage>();
            SqlDataAdapter data = new SqlDataAdapter("Select * from ClassInfo order by ClassID", sqlcon);
            DataSet dsCls = new DataSet();
            data.Fill(dsCls, "cls");
            for (int i = 0; i < dsCls.Tables[0].Rows.Count; i++)
            {
                ClassPage c = new ClassPage();
                c.ClassID = int.Parse(dsCls.Tables[0].Rows[i]["ClassID"].ToString());
                c.ClassName = dsCls.Tables[0].Rows[i]["ClassName"].ToString();
                lst.Add(c);
            }
            return lst;
        }

    }

}