using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Vjezba.DAL;
using Vjezba.Model;
using Vjezba.Web.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Vjezba.Web.Controllers
{
    public class ClientController : Controller
    {
		private ClientManagerDbContext _dbContext;
		public ClientController(ClientManagerDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		public IActionResult Index(string query = null)
        {
			var clientQuery = _dbContext.Clients
                .Include(c => c.City)
                .AsQueryable();


			if (!string.IsNullOrWhiteSpace(query))
                clientQuery = clientQuery.Where(p => 
                p.FirstName.ToLower().Contains(query) ||
		        p.LastName.ToLower().Contains(query));

            ViewBag.ActiveTab = 1;

            return View(clientQuery.ToList());
        }

        [HttpPost]
        public ActionResult Index(string queryName, string queryAddress)
        {
			var clientQuery = _dbContext.Clients
				.Include(c => c.City)
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(queryName))
				clientQuery = clientQuery.Where(p =>
				p.FirstName.ToLower().Contains(queryName) ||
				p.LastName.ToLower().Contains(queryName));

			if (!string.IsNullOrWhiteSpace(queryAddress))
                clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(queryAddress));

            ViewBag.ActiveTab = 2;

            var model = clientQuery.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AdvancedSearch(ClientFilterModel filter, int tab)
        {
			var clientQuery = _dbContext.Clients
				.Include(c => c.City)
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter.FullName))
				clientQuery = clientQuery.Where(p =>
				p.FirstName.ToLower().Contains(filter.FullName) ||
				p.LastName.ToLower().Contains(filter.FullName));

			if (!string.IsNullOrWhiteSpace(filter.Address))
                clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                clientQuery = clientQuery.Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.City))
                clientQuery = clientQuery.Where(p => p.City != null && p.City.Name.ToLower().Contains(filter.City.ToLower()));

            ViewBag.ActiveTab = tab;

            var model = clientQuery.ToList();
            return View("Index", model);
        }


		public IActionResult Details(int? id = null)
		{
			if (id == null)
				return NotFound();

			var model = _dbContext.Clients
				.Include(c => c.City)
				.FirstOrDefault(c => c.ID == id.Value);

			if (model == null)
				return NotFound();

			return View(model);
		}

		public IActionResult Create()
        {
			var cities = _dbContext.Cities.ToList();
			ViewBag.Cities = cities;
			return View(new Client());
        }

        [HttpPost]
        public IActionResult Create(Client newClient)
        {

            ViewBag.Cities = _dbContext.Cities.ToList();
            if (string.IsNullOrEmpty(newClient.FirstName) || string.IsNullOrEmpty(newClient.LastName))
            {
                return View();
            }

			_dbContext.Clients.Add(newClient);
			_dbContext.SaveChanges();

			TempData["SuccessMessage"] = "Klijent dodan.";

			return RedirectToAction("Create", "Client");
		}
	}

}
