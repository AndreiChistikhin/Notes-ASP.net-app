using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Queires.GetNoteList;

public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListVm>
{
    private INotesDBContext _dbContext;
    private IMapper _mapper;

    public GetNoteListQueryHandler(INotesDBContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
    {
        var notesQuery = await _dbContext.Notes
            .Where(note => note.UserId == request.UserId)
            .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new NoteListVm {Notes = notesQuery};
    }
}