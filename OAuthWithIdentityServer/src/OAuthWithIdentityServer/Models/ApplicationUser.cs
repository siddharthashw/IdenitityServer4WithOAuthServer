using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OAuthWithIdentityServer.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime JoiningDate { get; set; }


        [StringLength(255)]
        public string SlackUserId { get; set; }


        [Required]
        [StringLength(255)]
        public string SlackUserName { get; set; }

        [NotMapped]
        public virtual ICollection<Project> Projects { get; set; }


        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated Date")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDateTime { get; set; }
    }
}
