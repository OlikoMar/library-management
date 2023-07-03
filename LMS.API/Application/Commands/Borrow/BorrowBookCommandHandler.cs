using LMS.Domain.Aggregates.BooksAggregate;
using MediatR;

namespace LMS.Application.Commands.Borrow;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand,bool>
{
    private readonly IBooksRepository _booksRepository;

    public BorrowBookCommandHandler(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public async Task<bool> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _booksRepository.FindByIdAsync(request.Id);
        book.Borrow(request.IsBorrowed);
        _booksRepository.Update(book);
        await _booksRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}