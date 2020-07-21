using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BoniStreaming.Models;

namespace BoniStreaming.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; } 
        
        [StringLength(255)] 
        [Required] 
        public string Name { get; set; } 
        
        [Range(1, 6)] 
        [Required] 
        public int GenreId { get; set; }
        
        [Required] 
        public DateTime ReleaseDate { get; set; } 
        
        [Required] 
        public DateTime DateAdded { get; set; }
        
        [Range(1, 20)] 
        [Required] 
        public byte NumberInStock { get; set; }
    }
}
