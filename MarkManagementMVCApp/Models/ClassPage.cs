using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarkManagementMVCApp.Models
{
    public class ClassPage
    {
        //create table ClassInfo(ClassID int IDENTITY(1, 1)  primary key   ,ClassName varchar(10) unique);
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        
    }
}