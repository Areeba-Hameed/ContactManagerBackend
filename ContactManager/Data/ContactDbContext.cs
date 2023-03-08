using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {

        }

        public DbSet<ContactManagerModel> contactManagerModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactManagerModel>()
              .Property(e => e.ID)
              .HasDefaultValueSql("NEXT VALUE FOR my_sequence")
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<ContactManagerModel>().ToTable("tbl_contact_manager");

        }
    } 
}
