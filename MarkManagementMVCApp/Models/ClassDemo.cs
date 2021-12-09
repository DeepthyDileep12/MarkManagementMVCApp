using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MarkManagementMVCApp.Models
{
    public class ClassDemo
    {
        [Required(ErrorMessage ="Enter Class Id")]
        public int ClassID { get; set; }
        [Required(ErrorMessage = "Enter Class Name")]
        public string ClassName { get; set; }
    }
}