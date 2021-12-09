using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace MarkManagementMVCApp.Models
{
    public class Mark
    {
        //,ExamType varchar(10),Science int,	Maths int,	Computer int,	Total int,Average int,
        //Grade char (2),RollNo varchar(5),foreign key(RollNo) references StudentReg(Rollno),ClassID int,foreign key(ClassID) references ClassInfo(ClassID),Fid varchar(5),
        //foreign key(Fid) references FacultyInfo(Fid))
        [Required(ErrorMessage = "Exam Type must be Select")]
        public string ExamType { get; set; }
        [Required(ErrorMessage = "Mark Must Enter")]
        public int Science { get; set; }
        [Required(ErrorMessage = "Mark Must Enter")]
        public int Maths { get; set; }
        [Required(ErrorMessage = "Mark Must Enter")]
        public int Computer { get; set; }
        [Required(ErrorMessage = "Password Must Enter")]
        public int Total { get; set; }
        [Required(ErrorMessage = "Password Must Enter")]
        public int Average { get; set; }
        [Required(ErrorMessage = "Password Must Enter")]
        public string Grade { get; set; }
        [Required(ErrorMessage = "RollNo Must Enter")]
        public string RollNo { get; set; }
        [Required(ErrorMessage = "Class ID Must Enter")]
        public int ClassID { get; set; }
        [Required(ErrorMessage = "Faculty ID Must Enter")]
        public string Fid { get; set; }
        [Required(ErrorMessage = "Student Name Must Enter")]
        public string StdName { get; set; }
        [Required(ErrorMessage = "Faculty Name Must Enter")]
        public string FactName { get; set; }
        [Required(ErrorMessage = "Class Name Must Enter")]
        public string ClassName { get; set; }
        [NotMapped]
        public SelectList ClassDropdown { get; set; }
        [NotMapped]
        public SelectList StdDropdown { get; set; }
    }
    public enum Etype
    {
        Quaterly,
        Harfly,
        Final
    }
}