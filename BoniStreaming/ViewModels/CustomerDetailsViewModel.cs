using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoniStreaming.Models;

namespace BoniStreaming.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public Customer Customer { get; set; }

        public string MembershipTypeName { get; set; }

        public string IsSubscribedToNewsletter { get; set; }
    }
}