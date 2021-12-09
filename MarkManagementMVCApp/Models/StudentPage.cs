using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarkManagementMVCApp.Models
{
    public class StudentPage
    {
        //create table StudentReg(Rollno varchar(5) primary key,StdName varchar(20),Gender varchar(10),Address varchar(100),Location varchar(20),
        //Phone char (10),Email varchar(20),Password varchar(20),ClassID int,foreign key(ClassID) references ClassInfo(ClassID) );
       [Required(ErrorMessage = "Rollno Must Enter")]
        public string Rollno { get; set; }
        [Required(ErrorMessage = "Student Name Must Enter")]
        public string StdName { get; set; }
        [Required(ErrorMessage = "Gender Must Enter")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Address Must Enter")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Location Must Enter")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Phone Must Enter")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email Must Enter")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Must Enter")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password Must Enter")]
        public string NPassword { get; set; }
        [Required(ErrorMessage = "Password Must Enter")]
        [System.ComponentModel.DataAnnotations.Compare("NPassword", ErrorMessage = "Password does not match!!")]
        public string CPassword { get; set; }
        [Required(ErrorMessage = "Class ID Must Enter")]
        public int ClassID { get; set; }
        [Required(ErrorMessage = "Class Name Must Enter")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Faculty Name Must Enter")]
        public string FactName { get; set; }
        [NotMapped]
        public SelectList ClassDropdown { get; set; }
    }
    public enum gen
    {
        Male,
        Female
    }
    
}