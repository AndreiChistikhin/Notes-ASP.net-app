using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.CreateNode;

public class CreateNodeCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private INotesDBContext _dbContext;

    public CreateNodeCommandHandler(INotesDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note
        {
            UserId = request.UserId,
            Title = request.Title,
            Details = request.Details,
            Id = Guid.NewGuid(),
            CreationDate = DateTime.Now,
            EditDate = null
        };

        await _dbContext.Notes.AddAsync(note, cancellationToken);
        await _dbContext.SaveChangedAsync(cancellationToken);
        
        return note.Id;
    }
}