using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models
{  
   public class wpContext : DbContext
   {
       public wpContext(DbContextOptions<wpContext> options) : base(options){}
       public DbSet<Users> Users {get;set;}
       public DbSet<Weddings> Weddings {get;set;}
       public DbSet<Reservations> Reservations {get;set;}
   }
}