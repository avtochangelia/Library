#nullable disable

using Domain.AuthorManagement;
using Domain.BookManagement;
using Domain.UserManagement;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class EFDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<Dictionary<string, object>>(
                "BookAuthor",
                b => b.HasOne<Author>().WithMany().HasForeignKey("AuthorId"),
                a => a.HasOne<Book>().WithMany().HasForeignKey("BookId")
            );
    }
}