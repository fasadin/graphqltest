using graphqltest.Common.Data;
using Microsoft.EntityFrameworkCore;
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable 8618

namespace graphqltest.Data
{
    /*
     * Right now, as migration is set to this project, every table has to be configured here
     * if you are adding new table please udpate here dbset so migration can be created
     * TODO: Seperate this in another abtractions, but when it's needed
     */
    public class graphqltestContext : DbContext
    {
        public graphqltestContext(DbContextOptions<graphqltestContext> options) : base(options)         
        {         
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Language> Languages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Database.db");
    }
}