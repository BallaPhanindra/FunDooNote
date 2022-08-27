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
    }
}