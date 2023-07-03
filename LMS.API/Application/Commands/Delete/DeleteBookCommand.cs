using MediatR;

namespace LMS.Application.Commands.Delete;

public class DeleteBookCommand : IRequest<bool>
{
    public int Id { get; set; }
}