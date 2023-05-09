using AutoMapper.Configuration;
using Domino.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Domino.Infrastructure.Data
{
    public class DominoContext: DbContext

    {
        public IConfiguration Configuration { get; }
        public DominoContext()
        {
        }
        public DominoContext(DbContextOptions<DominoContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        public DbSet<User> Users { get; set; }

        public DbSet<DominoFullGame> dominoFullGames { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = localhost; Integrated Security = false; Initial Catalog = DominoDb; User ID = sa; Password = Administrador2023");
                //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=Domino;Integrated Security =true");

            }
        }
        public DominoContext(DbContextOptions<DominoContext> options) :base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            List<User> usersInit = new List<User>();
            usersInit.Add(new User() { Id=1, Name = "Sebastian ", Lastname = "Lopera", Email = "jsloperagdv@gmail.com", Password = "holamundo" });
            usersInit.Add(new User() { Id=2, Name = "Soledad ", Lastname = "Gallego", Email = "soledadgallegodm@gmail.com", Password = "holamundo2" });

            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("User");
                user.HasKey(x => x.Id);
                user.Property<string>(x => x.Name).IsRequired().HasMaxLength(200);
                user.Property<string>(x => x.Lastname).IsRequired().HasMaxLength(200);
                user.Property<string>(x => x.Email).IsRequired().HasMaxLength(140);
                user.Property<string>(x => x.Password).IsRequired();

                user.HasData(usersInit);
            });



            modelBuilder.Entity<DominoFullGame>(dominoFullGame =>
            {
                dominoFullGame.ToTable("DominoFullGame");
                dominoFullGame.HasKey(x => x.Id);
                dominoFullGame.Property<string>(x => x.DominoGame).IsRequired();
                dominoFullGame.Property<bool>(x => x.isValid).IsRequired();
                dominoFullGame.HasOne(x => x.user).WithMany(x => x.DominoFullGame).HasForeignKey(x => x.UserId);

            });
        }
    }
}
