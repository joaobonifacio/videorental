using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoniStreaming.Models;

namespace BoniStreaming.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie movie { get; set; }

        public string genreName;

        public IEnumerable<Genre> genres;
    }
}