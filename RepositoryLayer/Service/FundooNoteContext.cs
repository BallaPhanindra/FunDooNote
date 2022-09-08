using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class FundooNoteContext : DbContext
    {
        public FundooNoteContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Label> Labels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasIndex(u => u.Email)
             .IsUnique();

            modelBuilder.Entity<Label>()
            .HasKey(p => new { p.UserId, p.NoteId });

            modelBuilder.Entity<Label>()
            .HasOne(u => u.user)
            .WithMany()
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Label>()
            .HasOne(n => n.Note)
            .WithMany()
            .HasForeignKey(n => n.NoteId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}