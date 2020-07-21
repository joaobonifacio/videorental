using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoniStreaming.Models;

namespace BoniStreaming.ViewModels
{
    public class CustomerFormViewModel
    {
        public Customer Customer { get; set; }

        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}