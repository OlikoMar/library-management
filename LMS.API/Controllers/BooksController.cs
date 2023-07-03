using System.Net;
using LMS.Application.Commands.Borrow;
using LMS.Application.Commands.Create;
using LMS.Application.Commands.Delete;
using LMS.Application.Commands.Update;
using LMS.Application.Dtos;
using LMS.Application.Queries;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksQueries _booksQueries;
        private readonly IMediator _mediator;

        public BooksController(IBooksQueries booksQueries, IMediator mediator)
        {
            _booksQueries = booksQueries;
            _mediator = mediator;
        }

        /// <summary>
        /// Get books
        /// </summary>
        /// <returns>List of books</returns>
        [HttpGet] 
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var result = await _booksQueries.GetBooks();
            if (!result.Any())
                return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// Get book
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single book</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _booksQueries.GetBook(id);
            return Ok(book);
        }

        /// <summary>
        /// Update book
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutBook([FromBody] UpdateBookCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Create book
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Book>> PostBook([FromBody] CreateBookCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Remove book
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteBook([FromBody] DeleteBookCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Borrow book
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("borrow")]
        [Authorize]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
    }
}