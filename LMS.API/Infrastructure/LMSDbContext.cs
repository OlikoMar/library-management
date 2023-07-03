using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure;

public class LMSDbContext : DbContext
{
    public LMSDbContext(DbContextOptions<LMSDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().ToTable("Authors");
        modelBuilder.Entity<Author>().HasKey(cr => cr.Id);
        
        modelBuilder.Entity<Book>().ToTable("Books");
        modelBuilder.Entity<Book>().HasKey(cr => cr.Id);

        modelBuilder.Entity<BookAuthors>().ToTable("BookAuthors");
        modelBuilder.Entity<BookAuthors>().HasKey(cr => cr.Id);
    }

    public DbSet<Author> Author { get; set; } = default!;

    public DbSet<Book> Book { get; set; } = default!;
}