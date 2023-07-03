using LMS.Domain.Aggregates.BooksAggregate;
using MediatR;

namespace LMS.Application.Commands.Update;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
{
    private readonly IBooksRepository _booksRepository;

    public UpdateBookCommandHandler(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _booksRepository.FindByIdAsync(request.Id);
        book.UpdateData(request.Title, request.Description, request.Image, request.PublishDate);
        _booksRepository.Update(book);
        await _booksRepository.SaveChangesAsync(cancellationToken);
        return true;
    }
}