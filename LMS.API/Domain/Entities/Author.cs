using LMS.Domain.Aggregates;

namespace LMS.Domain.Entities;

public class Author : BaseEntity, IAggregateRoot
{
    public Author(string name, string lastName, int birthYear)
    {
        Name = name;
        LastName = lastName;
        BirthYear = birthYear;
    }

    public string Name { get; private set; }
    public string LastName { get; private set; }
    public int BirthYear { get; private set; }
}