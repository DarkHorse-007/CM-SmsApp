using MessageApp.Models;
using System.Data.Entity;

namespace MessageApp.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    class MessageDataContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<CountryCode> CountryDialingCodes { get; set; }

        public MessageDataContext():base("name=SQLiteConnectionstring")
        {
            Database.SetInitializer<MessageDataContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().HasRequired(e => e.Status).WithRequiredDependent();
        }
    }
}
