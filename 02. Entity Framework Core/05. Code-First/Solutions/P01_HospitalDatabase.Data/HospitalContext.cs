using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
            
        }
        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options) 
        {
            
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> PatientMedicament { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Hospital;Integrated Security=true;TrustServerCertificate=true;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity
                    .Property(e => e.FirstName)
                    .IsUnicode(true);
                entity
                    .Property(e => e.LastName)
                    .IsUnicode(true);
                entity
                    .Property(e => e.Address)
                    .IsUnicode(true);
                entity
                    .Property(e => e.Email)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Visitation>(entity =>
            {
                entity
                    .Property(e => e.Comments)
                    .IsUnicode(true);
                
            });
            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity
                    .Property(e => e.Name)
                    .IsUnicode(true);
                entity
                    .Property(e => e.Comments)
                    .IsUnicode(true);
            });
            modelBuilder.Entity<Medicament>(entity =>
            {
                entity
                    .Property(e => e.Name)
                    .IsUnicode(true);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
    
}