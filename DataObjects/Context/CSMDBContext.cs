using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Context
{
    public class CSMDBContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Cruise> Cruises { get; set; }
        // DbSet<TicketCruise> TicketCruises { get; set; }

        public CSMDBContext()
        {
            this.People = this.Set<Person>();
            this.Tickets = this.Set<Ticket>();
            this.Cruises = this.Set<Cruise>();
            //this.TicketCruises = this.Set<TicketCruise>();
        }
        public CSMDBContext(DbContextOptions options) : base(options)
        {
            this.People = this.Set<Person>();
            this.Tickets = this.Set<Ticket>();
            this.Cruises = this.Set<Cruise>();
            //this.TicketCruises = this.Set<TicketCruise>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Yoan\OneDrive\Desktop\School\Uni\distributedapps\CruiseShipManagement\Database\CruiseShipDB.mdf;Integrated Security=True;Connect Timeout=30", b => b.MigrationsAssembly("CSMAPI"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("People");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<Cruise>().ToTable("Cruises");
            //modelBuilder.Entity<TicketCruise>().ToTable("TicketCruises");

            modelBuilder.Entity<Person>().HasData(
            new Person
            {
                Id = 1,
                FirstName = "Yoan",
                LastName = "Kimanov",
                DateOfBirth = DateTime.Parse("25-09-2003"),
                Phone = "+359 897 8595 58",
                Email = "stu2201321037@uni-plovdiv.bg",
                Address = "st. Saedinenie 2a"
            },
            new Person
            {
                Id = 2,
                FirstName = "Ivano",
                LastName = "Ivanov",
                DateOfBirth = DateTime.Parse("12-12-2006"),
                Phone = "+359 895 6363 42",
                Email = "i.ivanov@gmail.com",
                Address = "st. Snejanka 54"
            },
            new Person
            {
                Id = 3,
                FirstName = "Yan",
                LastName = "Bibiyan",
                DateOfBirth = DateTime.Parse("15-06-2000"),
                Phone = "+359 888 8888 88",
                Email = "yan.bi@bi.yan",
                Address = "st. Asmokorev 3"
            },
            new Person
            {
                Id = 4,
                FirstName = "John",
                LastName = "Nobody",
                DateOfBirth = DateTime.Parse("01-01-2001"),
                Phone = "+1 202 555 0118",
                Email = "jobody@yahoo.com",
                Address = "st. Yorkshire 15"
            });
            modelBuilder.Entity<Ticket>().HasData(
            new Ticket
            {
                Id = 1,
                Price = 1000.00,
                Description = "Access to Cruise - Fetorna (From France to Japan), Medium-sized room, Free Breakfast and Lunch",
                BuyDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(365),
                PersonId = 1
            },
            new Ticket
            {
                Id = 2,
                Price = 5200.00,
                Description = "Access to Cruise - Remoria (World Cruise), King-sized room, Free Breakfast, Lunch and Alchohol",
                BuyDate = DateTime.Now.AddDays(-20),
                ExpireDate = DateTime.Now.AddDays(365),
                PersonId = 2
            });            
            modelBuilder.Entity<Cruise>().HasData(
            new Cruise
            {
                Id = 1,
                Name = "Fetorna",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(90),
                StarRating = 4.3,
                Seating = 2000
            },
            new Cruise
            {
                Id = 2,
                Name = "Remoria",
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddDays(100),
                StarRating = 5.0,
                Seating = 250
            },
            new Cruise
            {
                Id = 3,
                Name = "Extasy",
                StartDate = DateTime.Now.AddDays(14),
                EndDate = DateTime.Now.AddDays(126),
                StarRating = 4.8,
                Seating = 4000
            });
            //modelBuilder.Entity<TicketCruise>().HasData(
            //new TicketCruise
            //{
            //    Id = 1,
            //    TicketId = 1,
            //    CruiseId = 1
            //});
        }
    }
}
