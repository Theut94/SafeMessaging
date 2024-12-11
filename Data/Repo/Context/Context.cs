using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo.Context
{
    public class Context : DbContext
    {
        public required DbSet<Chat> Chats { get; set; }
        public required DbSet<Message> Messages { get; set; }
        public required DbSet<User> Users { get; set; }
        public required DbSet<Credentials> Credentials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.GUID);
                entity.Property(u => u.GUID).IsRequired();
                entity.Property(u => u.FirstName).IsRequired();
                entity.Property(u => u.LastName).IsRequired();
                entity.Property(u => u.Credentials).IsRequired();

                entity.HasMany(u => u.FriendList)
                      .WithMany();

                entity.HasMany(u => u.Chats)
                      .WithMany(c => c.Users);

                entity.HasOne(u => u.Credentials);
            });

            modelBuilder.Entity<Credentials>(entity =>
            {
                entity.Property(c => c.Password).IsRequired();
                entity.Property(c => c.UserName).IsRequired();
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(c => c.GUID);
                entity.Property(c => c.GUID).IsRequired();

                entity.HasMany(c => c.Users)
                      .WithMany(u => u.Chats);

                entity.HasMany(c => c.Messages)
                      .WithOne();
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(m => m.GUID);
                entity.Property(m => m.GUID).IsRequired();
                entity.Property(m => m.Text).IsRequired(false);
                entity.Property(m => m.IV).IsRequired(false);
                entity.Property(m => m.Sender).IsRequired();
            });
        }
    }
}