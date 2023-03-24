using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain;
using Notes.Persistence.EntityTypeConfiguration;

namespace Notes.Persistence;

public class NotesDbContext : DbContext, INotesDBContext
{
    public DbSet<Note> Notes { get; set; }

    protected NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new NoteConfiguration());
        base.OnModelCreating(builder);
    }

    public Task<int> SaveChangedAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}