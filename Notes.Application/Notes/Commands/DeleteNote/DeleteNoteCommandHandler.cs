using MediatR;
using Notes.Application.Common.Exception;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private INotesDBContext _dbContext;

    public DeleteNoteCommandHandler(INotesDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Notes.FindAsync(new object[] {request.Id}, cancellationToken);

        if (entity == null || entity.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Note), request.Id);
        }

        _dbContext.Notes.Remove(entity);
        await _dbContext.SaveChangedAsync(cancellationToken);
    }
}