using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restul_Web_Assessment.Repository.PostModels
{
    public class UserDTO
    {

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

        [StringLength(50)]
        [Unicode(false)]
        public string Password { get; set; }
    }

}

