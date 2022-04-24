using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quoting.Database.Model
{
    public class UserCustom : IdentityUser
    {
        [Column(TypeName = "varchar(300)")]

        public string FirstName { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }

    }
}
