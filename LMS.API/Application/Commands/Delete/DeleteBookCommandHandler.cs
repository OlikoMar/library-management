using LMS.Domain.Aggregates.BooksAggregate;
using MediatR;

namespace LMS.Application.Commands.Delete;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBooksRepository _booksRepository;

    public DeleteBookCommandHandler(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _booksRepository.FindByIdAsync(request.Id);
        _booksRepository.Remove(book);
        await _booksRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}