using Microsoft.EntityFrameworkCore;
using PetShelter.Api.Models;

namespace PetShelter.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Adopter> Adopters { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Adopter
            modelBuilder.Entity<Adopter>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Adopter>()
                .Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Adopter>()
                .Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Adopter>()
                .Property(a => a.Phone)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<Adopter>()
                .Property(a => a.RegistrationDate)
                .IsRequired()
                .HasColumnType("date");

            modelBuilder.Entity<Adopter>()
                .Property(a => a.BudgetContribution)
                .HasColumnType("decimal(10,2)");


            //Animal
            modelBuilder.Entity<Animal>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Animal>()
                .Property(a => a.Name)
                .HasMaxLength(15);

            modelBuilder.Entity<Animal>()
                .Property(a => a.Species)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Animal>()
                .Property(a => a.BirthDate)
                .HasColumnType("date");

            modelBuilder.Entity<Animal>()
                .Property(a => a.Weight)
                .HasColumnType("decimal(5, 2)");

            modelBuilder.Entity<Animal>()
                .Property(a => a.IsVaccinated)
                .IsRequired();

            modelBuilder.Entity<Animal>()
                .Property(a => a.ArrivalDate)
                .IsRequired()
                .HasColumnType("date");

            //AdoptionApplication
            modelBuilder.Entity<AdoptionApplication>()
                .HasKey(aa => aa.Id);

            modelBuilder.Entity<AdoptionApplication>()
                .Property(aa => aa.ApplicationDate)
                .IsRequired()
                .HasColumnType("date");

            modelBuilder.Entity<AdoptionApplication>()
                .Property(aa => aa.Notes)
                .HasMaxLength(500);

            modelBuilder.Entity<AdoptionApplication>()
                .Property(aa => aa.ProcessingFee)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(aa => aa.Adopter)
                .WithMany()
                .HasForeignKey(aa => aa.AdopterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AdoptionApplication>()
                .HasOne(aa => aa.Animal)
                .WithMany()
                .HasForeignKey(aa => aa.AnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            // User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordSalt)
                .IsRequired();
        }
    }
}

