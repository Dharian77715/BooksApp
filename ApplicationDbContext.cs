using System;
using BooksApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApp
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BooksGenres>()
            .HasKey(bg => new { bg.GenreId, bg.BookId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BooksGenres> BooksGenres { get; set; }
        public DbSet<Sex> Sexes { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}

