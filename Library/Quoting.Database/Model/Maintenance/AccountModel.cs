using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoting.Database.Model.Maintenance
{
    public class AccountModel
    {
        [Key]
        public int AccountId { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string AccountNumber { get; set; }
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string AccountType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public bool Status { get; set; }
    }
}
