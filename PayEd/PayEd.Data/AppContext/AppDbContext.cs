using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayEd.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Data.AppContext
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users {  get; set; }
        public DbSet<Streams> Streams {  get; set; }
        public DbSet<Income> Incomes {  get; set; }
        public DbSet<Expenses> Expenses {  get; set; }
        public DbSet<Budgets> Budgets {  get; set; }
        public DbSet<School> Schools {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.User_Id);

            modelBuilder.Entity<Budgets>()
                .HasOne(b => b.User)
                .WithMany(u => u.Budgets)
                .HasForeignKey(b => b.UserId); // Choose the appropriate delete behavior

            modelBuilder.Entity<Expenses>()
                .HasOne(e => e.Budget)
                .WithMany(b => b.Expenses)
                .HasForeignKey(e => e.BudgetId)
                .OnDelete(DeleteBehavior.Cascade); // Choose the appropriate delete behavior

            modelBuilder.Entity<Income>()
                .HasOne(i => i.User)
                .WithMany(u => u.Income)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Choose the appropriate delete behavior

            modelBuilder.Entity<Income>()
                .HasOne(i => i.Stream)
                .WithMany(s => s.Income)
                .HasForeignKey(i => i.StreamId)
                .OnDelete(DeleteBehavior.Restrict); // Choose the appropriate delete behavior

            modelBuilder.Entity<Streams>()
                .HasOne(s => s.User)
                .WithMany(u => u.Streams)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Choose the appropriate delete behavior
        }
    }
}
