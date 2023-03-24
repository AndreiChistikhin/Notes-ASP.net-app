using Microsoft.EntityFrameworkCore;
using Notes.Domain;

namespace Notes.Application.Interfaces;

public interface INotesDBContext
{
    DbSet<Note> Notes { get; set; }
    Task<int> SaveChangedAsync(CancellationToken cancellationToken);
}