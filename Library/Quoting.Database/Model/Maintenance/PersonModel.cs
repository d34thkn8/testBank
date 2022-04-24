using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoting.Database.Model.Maintenance
{
    public class PersonModel
    {
        [Column(TypeName = "varchar(20)")]
        public string IdCard { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string Genre { get; set; }

        public int Age { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Direction { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }
    }
}
