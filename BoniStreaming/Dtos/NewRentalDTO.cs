using BoniStreaming.Models;
using System.Collections.Generic;

namespace BoniStreaming.Dtos
{
    public class NewRentalDTO
    {
        public int CustomerID { get; set; }

        public List<int> MovieIds { get; set; }
    }
}