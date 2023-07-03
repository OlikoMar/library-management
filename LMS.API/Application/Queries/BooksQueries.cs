using LMS.Application.Dtos;
using LMS.Domain.Entities;
using LMS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Queries;

public class BooksQueries : IBooksQueries
{
    private readonly LMSDbContext _dbContext;

    public BooksQueries(LMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BookDto>> GetBooks()
    {
        var books = await _dbContext.Set<Book>().ToListAsync();
        return books.Select(s => new BookDto
        {
            Title = s.Title,
            Description = s.Description,
            Image = s.Image,
            Rating = s.Rating,
            PublishDate = s.PublishDate,
            IsBorrowed = s.IsBorrowed
        });
    }

    public async Task<BookDto> GetBook(int id)
    {
        var book = await _dbContext.Set<Book>().FirstOrDefaultAsync(s => s.Id == id);
        return new BookDto
        {
            Title = book.Title,
            Description = book.Description,
            Image = book.Image,
            Rating = book.Rating,
            PublishDate = book.PublishDate,
            IsBorrowed = book.IsBorrowed
        };
    }
}