using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vjezba.DAL;
using Vjezba.Model;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
	public class ClientController(
        ClientManagerDbContext _dbContext) : Controller
    {
        public IActionResult Index(ClientFilterModel filter = null)
        {
			filter ??= new ClientFilterModel();

			var clientQuery = _dbContext.Clients.Include(c => c.City).AsQueryable();

			if (!ModelState.IsValid)
			{
				foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
				{
					Console.WriteLine(error.ErrorMessage); // ili stavi breakpoint
				}
			}
			//Primjer iterativnog građenja upita - dodaje se "where clause" samo u slučaju da je parametar doista proslijeđen.
			//To rezultira optimalnijim stablom izraza koje se kvalitetnije potencijalno prevodi u SQL
			if (!string.IsNullOrWhiteSpace(filter.FullName))
				clientQuery = clientQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(filter.FullName.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Address))
                clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                clientQuery = clientQuery.Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.City))
				clientQuery = clientQuery.Where(p => p.CityID != null && p.City.Name.ToLower().Contains(filter.City.ToLower()));

            var model = clientQuery.ToList();
            return View(model);
        }

        public IActionResult Details(int? id = null)
        {
			var client = _dbContext.Clients
				.Include(p => p.City)
				.Where(p => p.ID == id)
				.FirstOrDefault();

			return View(client);
		}

		public IActionResult Create()
		{
	
			FillDropDownValues();
			return View(new Client());
		}

		[HttpPost]
		public IActionResult Create(Client newClient)
		{

			FillDropDownValues();

			if (ModelState.IsValid)
			{
				_dbContext.Clients.Add(newClient);
				_dbContext.SaveChanges();
				TempData["SuccessMessage"] = "Klijent dodan.";

				return RedirectToAction(nameof(Index));
			}
	
			return View(newClient);
		}

		public IActionResult Edit(int id)
		{
			var client = _dbContext.Clients
				.Include(p => p.City)
				.Where(p => p.ID == id)
				.FirstOrDefault();

			if (client == null)
			{
				return NotFound();
			}

			FillDropDownValues();
			return View(client);
		}

		[HttpPost]
		[ActionName("Edit")]
		public async Task <IActionResult> EditPost(int id)
		{
			var client = _dbContext.Clients
				.Include(p => p.City)
				.Where(p => p.ID == id)
				.Single();

			var ok = await this.TryUpdateModelAsync(client);

			if (ok)
			{
				_dbContext.SaveChanges();
				return RedirectToAction(nameof(Index));
			}

			FillDropDownValues();
			return View(client);
		}

		private void FillDropDownValues()
		{
			var cities = _dbContext.Cities
				.Select(c => new SelectListItem
				{
					Value = c.ID.ToString(),
					Text = c.Name
				})
				.ToList();

			cities.Insert(0, new SelectListItem
			{
				Value = "",
				Text = "Enter City"
			});

			ViewBag.Cities = cities;
		}
	}
}

