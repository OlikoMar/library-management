using LMS.Application.Dtos;

namespace LMS.Application.Queries;

public interface IBooksQueries
{
    Task<IEnumerable<BookDto>> GetBooks();
    Task<BookDto> GetBook(int id);
}