using Core.Entity.Concrete;
using Core.Methods;
using Entities;
using Entities.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Makale> Makaleler { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MakaleMap());
            modelBuilder.ApplyConfiguration(new UyeMap());
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            NetworkFunctions functions = new NetworkFunctions();
            List<EntityEntry> modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified).ToList();
            string identity = WindowsIdentity.GetCurrent().Name;
            string computerName = Environment.MachineName;
            DateTime dateTime = DateTime.Now;
            string user = Environment.UserName;
            string ip = functions.GetLocalIPAddress();

            foreach (var item in modifiedEntries)
            {
                CoreEntity? entity = item.Entity as CoreEntity;
                if (item != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        entity.CreatedADUserName = identity;
                        entity.CreatedComputerName = computerName;
                        entity.CreatedDate = dateTime;
                        entity.CreatedIP = ip;
                        entity.CreatedBy = user;
                    }
                    else if (item.State == EntityState.Modified)
                    {
                        entity.ModifiedADUserName = identity;
                        entity.ModifiedComputerName = computerName;
                        entity.ModifiedDate = dateTime;
                        entity.ModifiedIP = ip;
                        entity.ModifiedBy = user;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
