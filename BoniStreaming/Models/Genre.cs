using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoniStreaming.Models
{
    public class Genre
    {
        public int Id { set; get; }

        [Required]
        [Display(Name = "Movie Genre")] 
        public string Name { set; get; }
    }
}