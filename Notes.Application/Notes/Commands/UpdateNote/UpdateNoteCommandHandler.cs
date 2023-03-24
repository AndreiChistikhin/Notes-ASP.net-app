using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exception;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.UpdateNote;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
{
    private INotesDBContext _dbContext;

    public UpdateNoteCommandHandler(INotesDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);

        if (entity == null || entity.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Note), request.Id);
        }

        entity.Details = request.Details;
        entity.Title = request.Title;
        entity.EditDate = DateTime.Now;

        await _dbContext.SaveChangedAsync(cancellationToken);
    }
}