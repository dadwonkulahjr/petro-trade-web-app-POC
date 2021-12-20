using HADI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HADI.Data
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<CheckList> Checklists { get; set; }
        public DbSet<CheckListReport> CheckListReports { get; set; }

        public DbSet<CheckListReportBridgeTable> CheckListReportBridgeTables { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CheckListReportBridgeTable>().HasKey(clrbt => new { clrbt.CheckListId, clrbt.CheckListReportId });
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
