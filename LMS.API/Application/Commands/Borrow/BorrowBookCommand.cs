using MediatR;

namespace LMS.Application.Commands.Borrow;

public class BorrowBookCommand : IRequest<bool>
{
    public int Id { get; set; }
    public bool IsBorrowed { get; set; }
}