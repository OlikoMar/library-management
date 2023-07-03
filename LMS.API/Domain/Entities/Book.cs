using LMS.Domain.Aggregates;

namespace LMS.Domain.Entities;

public class Book : BaseEntity, IAggregateRoot
{
    public Book(
        string title,
        string description,
        string image,
        DateTimeOffset publishDate)
    {
        Title = title;
        Description = description;
        Image = image;
        Rating = default;
        PublishDate = publishDate;
        IsBorrowed = default;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Image { get; private set; }
    public decimal Rating { get; private set; }
    public DateTimeOffset PublishDate { get; private set; }
    public bool IsBorrowed { get; private set; }

    public void Borrow(bool isBorrowed)
    {
        IsBorrowed = isBorrowed;
    }

    public void UpdateData(string title, string description, string image, DateTimeOffset publishDate)
    {
        Title = title;
        Description = description;
        Image = image;
        PublishDate = publishDate;
    }
}