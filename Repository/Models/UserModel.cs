using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace Restul_Web_Assessment.Repository.Models;

[Index("Idnumber", Name = "UQ__Users__564DB08AFBF0E327", IsUnique = true)]
public partial class UserModel
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    [Column("IDNumber")]
    [StringLength(50)]
    [Unicode(false)]
    public string Idnumber { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ResidentialAddress { get; set; } = null!;

    public string MobileNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? EmailAddress { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Password { get; set; }
}
