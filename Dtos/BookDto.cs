using Biblioteca.Models;

namespace Biblioteca.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Author { get; set; }

        public int PublishedYear { get; set; }

        public string? Genre { get; set; }
    }
}