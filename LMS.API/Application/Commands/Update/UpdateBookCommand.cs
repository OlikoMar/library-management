using MediatR;

namespace LMS.Application.Commands.Update;

public class UpdateBookCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public decimal Rating { get; set; }
    public DateTimeOffset PublishDate { get; set; }
    public bool IsBorrowed { get; set; }
}