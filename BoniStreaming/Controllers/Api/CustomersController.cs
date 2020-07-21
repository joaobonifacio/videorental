using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using AutoMapper;
using BoniStreaming.Dtos;
using BoniStreaming.Models;
using BoniStreaming.ViewModels;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace BoniStreaming.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext db;

        public CustomersController()
        {
            db = new ApplicationDbContext();
        }

        // 1985-09-01T00:00:00

        //  GET /api/customers
        public IHttpActionResult GetCustomers(String query = null)
        {
            if(db.Customers.ToList().Count== 0)
            {
                NotFound();
            }

           var customersQuery = db.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery
                   .Where(c => c.Name.Contains(query));
            }

            var customerDTOs = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDTOs);
        }

        // GET /api/customers/id
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = db.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                NotFound();
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Customer customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            db.Customers.Add(customer);
            db.SaveChanges();

            customerDto.Id = customer.Id;

           return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
        }

        // PUT /api/customer/id
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            Customer customerInDb = db.Customers
                .SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                BadRequest();
            }
            
            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);

            var customerTest = customerInDb;

            customerInDb.Id = id;

            db.SaveChanges();

            customerDto.Id = id;


            return Ok(customerDto);
        }

        // DELETE api/customers/id
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = db.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                NotFound();
            }

            db.Customers.Remove(customerInDb);
            db.SaveChanges();

            return Ok(db.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }
    }
}
