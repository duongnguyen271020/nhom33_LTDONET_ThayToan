//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookShopOnline_ProjectSem3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class About
    {
        public int AboutID { get; set; }
        [Required(ErrorMessage = "Title can't be empty")]
        public string Title { get; set; }
        //[Required(ErrorMessage = "Image can't be empty")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Content can't be empty")]
        public string Content { get; set; }
    }
}
