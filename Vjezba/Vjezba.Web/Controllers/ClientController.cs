using Microsoft.AspNetCore.Mvc;
using Vjezba.Web.Mock;

namespace Vjezba.Web.Controllers
{
	public class ClientController : Controller
	{
		public IActionResult Index()
		{
			List<Client> clientList = MockClientRepository.Instance.All().ToList();
			List<City> cityList = MockCityRepository.Instance.All().ToList();
			ViewBag.Cities = cityList;
			return View(clientList);
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
