using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using BoniStreaming.Models;
using BoniStreaming.ViewModels;
using Microsoft.Ajax.Utilities;

namespace BoniStreaming.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Index()
        {
            return View("Index");
        }

        // GET: Customer/Create
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Create()
        {
            Customer customer = new Customer();

            IEnumerable<MembershipType> membershipTypes = db.MembershipTypes.ToList();

            CustomerFormViewModel customerFormViewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", customerFormViewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var membershipTypeList = db.MembershipTypes.ToList();

                var viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = membershipTypeList
                };

                return View("CustomerForm", viewModel);
            }
            
            if (customer == null)
            {
                return HttpNotFound();
            }

            if (customer.Id == 0)
            {
                Customer customerTest = new Customer();

                customerTest.Id = customer.Id;
                customerTest.MembershipTypeId = customer.MembershipTypeId;
                customerTest.Birthday = customer.Birthday;
                customerTest.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerTest.Name = customer.Name;


                db.Customers.Add(customer);
            }
            else
            {
                Customer customerToEdit = db.Customers.Single(c => c.Id == customer.Id);

                customerToEdit.Name = customer.Name;
                customerToEdit.Birthday = customer.Birthday;
                customerToEdit.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                if (customer.MembershipTypeId != 0)
                {
                    customerToEdit.MembershipTypeId = customer.MembershipTypeId;
                }
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Customer/Details/5
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Details(int id)
        {
            string isSubscribed;
            
            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            if (customer.IsSubscribedToNewsletter)
            {
                isSubscribed = "Yes";
            }
            else
            {
                isSubscribed = "No";
            }

            string typeName ="";

            foreach (var m in db.MembershipTypes)
            {
                if (m.Id == customer.MembershipTypeId)
                {
                    typeName = m.TypeName;
                } 
            }


            CustomerDetailsViewModel viewModel = new CustomerDetailsViewModel()
            {
                Customer = customer,
                MembershipTypeName = typeName.ToString(),
                IsSubscribedToNewsletter = isSubscribed
            };

            return View(viewModel);
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            Customer customer = db.Customers.Find(id);
            IEnumerable<MembershipType> membershipTypes = db.MembershipTypes.ToList();
            
            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerFormViewModel viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = membershipTypes
            };

            return View(viewModel);
        }

        /*

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

    */

        // GET: Customer/Delete/5
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
