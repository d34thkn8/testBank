using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quoting.Database.Model;
using Quoting.Database.Model.Maintenance;

namespace Quoting.Database.Database
{
    public class ChallengeDatabase : DbContext
    {
        public ChallengeDatabase(DbContextOptions<ChallengeDatabase> options)
            : base(options)
        {

        }


        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
       
    }
}
