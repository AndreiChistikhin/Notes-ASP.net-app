using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exception;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Queires.GetNoteDetails;

public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
{
    private IMapper _mapper;
    private INotesDBContext _dbContext;

    public GetNoteDetailsQueryHandler(INotesDBContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);

        if (entity == null || entity.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Note), request.Id);
        }

        return _mapper.Map<NoteDetailsVm>(entity);
    }
}