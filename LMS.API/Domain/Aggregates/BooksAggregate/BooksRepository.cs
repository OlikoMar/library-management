using LMS.Domain.Entities;
using LMS.Infrastructure;

namespace LMS.Domain.Aggregates.BooksAggregate;

public class BooksRepository : Repository<Book>, IBooksRepository
{
    public BooksRepository(LMSDbContext context) : base(context)
    {
    }
}