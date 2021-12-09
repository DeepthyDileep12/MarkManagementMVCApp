using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MarkManagementMVCApp.Models
{
    public class login
    {
       [Required(ErrorMessage = "User Name Must Enter")] 
       public string username { get; set; }
        [Required(ErrorMessage = "Password Must Enter")]

        public string password { get; set; }
        [Required(ErrorMessage = "User Type Must Select")]

        public string usertype { get; set; }

    }
    public enum utype
    {
        Admin,
        Faculty,
        Student
    }
}