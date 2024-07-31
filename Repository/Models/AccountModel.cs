using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Restul_Web_Assessment.Repository.Models;

[Table("Account")]
public partial class Account
{
    [Key]
    public int AccountNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string AccountHolder { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string AccountType { get; set; } = null!;

    public string IsActive { get; set; }

    public int Balance { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateModified { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

}
