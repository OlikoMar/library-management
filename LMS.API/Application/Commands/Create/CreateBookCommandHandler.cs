using LMS.Domain.Aggregates.BooksAggregate;
using LMS.Domain.Entities;
using MediatR;

namespace LMS.Application.Commands.Create;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private readonly IBooksRepository _booksRepository;

    public CreateBookCommandHandler(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book(
            request.Title,
            request.Description,
            request.Image,
            request.PublishDate);
        await _booksRepository.AddAsync(book);
        await _booksRepository.SaveChangesAsync(cancellationToken);
        return book.Id;
    }
}