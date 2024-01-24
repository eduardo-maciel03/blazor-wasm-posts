﻿using ApiBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBlog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        // agregar modelos
        public DbSet<Post> Post { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
