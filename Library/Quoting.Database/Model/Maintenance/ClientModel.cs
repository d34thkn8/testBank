using Quoting.Database.Model.Maintenance;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quoting.Database.Model
{
    public class ClientModel: PersonModel
    {
        [Key]
        public int ClientId { get; set; }   
        [Column(TypeName ="varchar(30)")]
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}
