using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CinemaObject
{
    public partial class Movie
    {
        public Movie()
        {
            Bills = new HashSet<Bill>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name cannot be null!")]
        [StringLength(50, ErrorMessage = "Lenght of name must be from 5 to 50!", MinimumLength = 5)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Image cannot be null!")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Description cannot be null!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "MainCharacters cannot be null!")]
        public string MainCharacters { get; set; }
        [Required(ErrorMessage = "MainCharacters cannot be null!")]
        [Range(1, 200, ErrorMessage = "Duration must be number!")]
        public int Duration { get; set; }
        [Required(ErrorMessage = "Genres cannot be null!")]
        public string Genres { get; set; }
        [Required(ErrorMessage = "Genres id cannot be null!")]
        [Range(1, 200, ErrorMessage = "Genres id must be number!")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Rating cannot be null!")]
        [Range(1, 200, ErrorMessage = "Rating must be number!")]
        public int Rating { get; set; }


        public virtual Genre Genre { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
