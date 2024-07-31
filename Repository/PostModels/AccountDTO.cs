using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restul_Web_Assessment.Repository.PostModels
{
    public class AccountDTO
    {

        [StringLength(50)]
        [Unicode(false)]
        public string AccountHolder { get; set; } = null!;

        [StringLength(10)]
        [Unicode(false)]
        public string AccountType { get; set; } = null!;

        [Column("UserID")]
        public int UserId { get; set; }

    }
}
