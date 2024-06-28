using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace quanLyNo.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AccountHolderInformation> AccountHolderInformations { get; set; }
        public DbSet<BorrowerInformation> BorrowerInformations { get; set; }
        public DbSet<LoanInformation> LoanInformations { get; set; }
        public DbSet<RelativeInformation> RelativeInformations { get; set; }
        public DbSet<LoanContract> LoanContracts { get; set; }
        public DbSet<LoanRepayment> LoanRepayments { get; set; }
        public DbSet<LoanDone> LoanDones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowerInformation>()
                        .HasOne(ld => ld.UserIdF)
                        .WithMany()
                        .HasForeignKey(ld => ld.UserId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LoanDone>()
                        .HasOne(ld => ld.userIdForeignKey)
                        .WithMany()
                        .HasForeignKey(ld => ld.UserId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LoanRepayment>()
                        .HasOne(ld => ld.UserIdF)
                        .WithMany()
                        .HasForeignKey(ld => ld.UserId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LoanContract>()
                        .HasOne(ld => ld.UserIdF)
                        .WithMany()
                        .HasForeignKey(ld => ld.UserId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LoanInformation>()
                        .HasOne(ld => ld.UserIdF)
                        .WithMany()
                        .HasForeignKey(ld => ld.UserId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RelativeInformation>()
                        .HasOne(ld => ld.UseuserIdForeignKeyrIdF)
                        .WithMany()
                        .HasForeignKey(ld => ld.UserId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LoanDone>()
                        .HasOne(ld => ld.LoanInformationidF)
                        .WithMany()
                        .HasForeignKey(ld => ld.LoanInformationId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoanRepayment>()
                        .HasOne(ld => ld.LoanInformationidF)
                        .WithMany()
                        .HasForeignKey(ld => ld.LoanInformationId)
                        .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
