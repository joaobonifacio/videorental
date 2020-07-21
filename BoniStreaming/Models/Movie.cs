using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoniStreaming.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        
        [Range(1, 6)]
        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Date Added")]

        public DateTime DateAdded { get; set; }

        [Range(1, 20)]
        [Required]
        [Display(Name = "Number In Stock")]
        public byte NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }
    }
}