using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoniStreaming.Models;

namespace BoniStreaming.Controllers
{
    public class MembershipTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       public ActionResult Index()
        {
            IEnumerable<MembershipType> membershipTypes = db.MembershipTypes.ToList();

            return View("Index", membershipTypes);
        }

       public ActionResult Create()
       {
           return null;
       }

       public ActionResult Edit()
       {
           return null;
       }

       public ActionResult Delete()
       {
           return null;
       }
    }
}