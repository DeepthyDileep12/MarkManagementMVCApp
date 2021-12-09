using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarkManagementMVCApp.Models
{
    public class Mapping
    {
        //create table ClassFaculty(Sno int primary key IDENTITY(1, 1),ClassID int unique,foreign key(ClassID) references ClassInfo(ClassID),Fid varchar(5) unique,
        //foreign key(Fid) references FacultyInfo(Fid));
        [Required(ErrorMessage = "Enter Class Id")]
        public int ClassID { get; set; }
        [Required(ErrorMessage = "Enter Class Name")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Enter Faculty ID")]
        public string Fid { get; set; }
        [Required(ErrorMessage = "Enter Class Faculty Name")]
        public string FactName { get; set; }
        [NotMapped]
        public SelectList ClassDropdown { get; set; }
        [NotMapped]
        public SelectList FactDropdown { get; set; }


    }
}