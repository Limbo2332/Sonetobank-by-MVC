using CourseWork_Update.Models;
using CourseWork_Update.Models.Deposits;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWork_Update.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CreditModel> Credits { get; set; }
        public DbSet<DepositModel> Deposits { get; set; }
        public DbSet<DepositsInfoModel> DepositsInfoModels { get; set; }
        public DbSet<PhotoInfosModel> PhotoInfos { get; set; }
        public DbSet<DepositPhotoInfo> DepositPhotoInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DepositPhotoInfo>().HasKey(pc => new { pc.DepositInfoId, pc.PhotoInfoId });
            base.OnModelCreating(builder);
        }

    }
}