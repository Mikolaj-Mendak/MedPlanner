using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options): base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClinicDoctor>()
                .HasKey(cd => new { cd.ClinicId, cd.DoctorId });

            modelBuilder.Entity<ClinicDoctor>()
                .HasOne(cd => cd.Clinic)
                .WithMany(c => c.ClinicDoctors)
                .HasForeignKey(cd => cd.ClinicId);

            modelBuilder.Entity<ClinicDoctor>()
                .HasOne(cd => cd.Doctor)
                .WithMany(d => d.ClinicDoctors)
                .HasForeignKey(cd => cd.DoctorId);
        }



        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<ClinicOwner> ClinicOwners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<DoctorAdmissionConditions> DoctorAdmissionConditions { get; set; }
        public DbSet<ClinicDoctor> ClinicDoctors { get; set; }
    }
}