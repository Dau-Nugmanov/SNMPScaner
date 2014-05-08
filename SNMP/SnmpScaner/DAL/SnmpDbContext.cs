using DAL.EfModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SnmpDbContext : DbContext
    {
        public SnmpDbContext()
            : base("db")
        {
            Database.SetInitializer<SnmpDbContext>(new DatabaseInitializer());
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeviceEntity> Devices { get; set; }
        public DbSet<DeviceItemEntity> Parameters { get; set; }
        public DbSet<DeviceModel> Models { get; set; }
        public DbSet<Maker> Makers { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<EmailEntity> EmailEntities { get; set; }
        public DbSet<DeviceItemHistory> ParametersHistory { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PhoneNotification> PhoneNotifications { get; set; }
        public DbSet<EmailNotification> EmailNotifications { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
