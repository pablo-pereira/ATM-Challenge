using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ATM.Models
{
    public partial class ATMContext : DbContext
    {
        public ATMContext()
        {
        }

        public ATMContext(DbContextOptions<ATMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cards> Cards { get; set; }
        public virtual DbSet<Operations> Operations { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Initial Catalog=ATM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cards>(entity =>
            {
                entity.HasKey(e => e.CardId);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ValidDate).HasColumnType("date");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cards_Users");
            });

            modelBuilder.Entity<Operations>(entity =>
            {
                entity.HasKey(e => e.OperationId);

                entity.Property(e => e.OperationId).ValueGeneratedNever();

                entity.Property(e => e.DateOperation).HasColumnType("date");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Operations)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Operations_Cards");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<Users>().HasData(new Users { Name = "Pablo", UserId = 1 });
            modelBuilder.Entity<Cards>().HasData(new Cards { CardId = 1, UserId = 1, IsLoked = false, Pin = 1234, Number = "4525111122223333", Balance = 35000, ValidDate = DateTime.Now.AddDays(365), ErrorsCount = 0 });
            modelBuilder.Entity<Operations>().HasData(new Operations { OperationId = 1, CardId = 1, Amount = 1000, DateOperation = DateTime.Now.AddDays(-1) });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
