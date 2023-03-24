using MediatR;

namespace Notes.Application.Notes.Queires.GetNoteDetails;

public class GetNoteDetailsQuery : IRequest<NoteDetailsVm>
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
}