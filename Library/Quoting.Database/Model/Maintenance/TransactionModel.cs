using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoting.Database.Model.Maintenance
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        public int TypeId { get; set; } 
        [Column(TypeName = "varchar(20)")]
        public string Type { get; set; } 
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public bool Status { get; set; }
    }
}
