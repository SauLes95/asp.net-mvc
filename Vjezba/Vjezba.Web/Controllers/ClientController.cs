using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Net;
using System.Xml.Linq;
using Vjezba.Web.Mock;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
	public class ClientController : Controller
	{
		[HttpGet]
		public IActionResult Index(string query)
		{
			List<Client> clientList = MockClientRepository.Instance.All().ToList();
			List<City> cityList = MockCityRepository.Instance.All().ToList();
			ViewBag.Cities = cityList;

			if (query != null)
			{
				clientList = clientList.Where(x => x.FullName.ToLower().Contains(query.ToLower())).ToList();
				//ViewBag.Query = query;
				return View(clientList);
			}

			return View(clientList);
		}

		[HttpPost]
		public IActionResult Index(string? queryName, string? queryAddress)
		{
			var clientList = MockClientRepository.Instance.All().ToList();
			var cityList = MockCityRepository.Instance.All().ToList();
			ViewBag.Cities = cityList;

			var filteredClients = clientList.AsQueryable();

			if (!string.IsNullOrWhiteSpace(queryName))
				filteredClients = filteredClients.Where(c => c.FullName.ToLower().Contains(queryName.ToLower()));

			if (!string.IsNullOrWhiteSpace(queryAddress))
				filteredClients = filteredClients.Where(c => c.Address.ToLower().Contains(queryAddress.ToLower()));

			return View(filteredClients.ToList());
		}

		[HttpPost]
		public IActionResult AdvancedSearch(ClientFilterModel? model)
		{

			var clientList = MockClientRepository.Instance.All().ToList();
			var cityList = MockCityRepository.Instance.All().ToList();
			ViewBag.Cities = cityList;

			var clients = MockClientRepository.Instance.All();
			if (model == null)
			{
				return View("Index");
			}

			if (!string.IsNullOrEmpty(model.partQueryName))
			{
				clients = clients.Where(c => c.FullName.ToLower().Contains(model.partQueryName.ToLower()));
			}

			if (!string.IsNullOrEmpty(model.partQueryEmail))
			{
				clients = clients.Where(c => c.Email.ToLower().Contains(model.partQueryEmail.ToLower()));
			}

			if (!string.IsNullOrEmpty(model.partQueryAddress))
			{
				clients = clients.Where(c => c.Address.ToLower().Contains(model.partQueryAddress.ToLower()));
			}

			if (!string.IsNullOrEmpty(model.partQueryCity))
			{
				clients = clients.Where(c => c.City != null && c.City.Name.ToLower().Contains(model.partQueryCity.ToLower()));
			}

			return View("Index", clients.ToList());

		}


		public IActionResult Details(int? id)
		{
			var model = id != null ? MockClientRepository.Instance.FindByID(id.Value) : null;
			if ( id > MockCityRepository.Instance.All().ToList().Count)
			{
				return View(null);
			}

			List<City> cityList = MockCityRepository.Instance.All().ToList();

			foreach (var city in cityList)
			{
				if (model.CityID == city.ID)
				{
					ViewBag.City = city.Name;
				}
			}
			return View(model);
		}
	}
}
