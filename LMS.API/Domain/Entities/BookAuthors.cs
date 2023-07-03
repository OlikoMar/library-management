namespace LMS.Domain.Entities;

public class BookAuthors : BaseEntity
{
    public Author Author { get; set; }
    public Book Book { get; set; }
}