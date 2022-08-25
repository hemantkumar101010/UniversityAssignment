using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UniversityManagementWebApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the UniversityAppUser class
public class UniversityAppUser : IdentityUser
{
    [Required]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Pan Number must be 10 characters")]
    [Column(TypeName = "char(10)")]
    public string PanNumber { get; set; }

    [Column(TypeName = "char(11)")]
    public string Status { get; set; } = "NotApproved";
}

