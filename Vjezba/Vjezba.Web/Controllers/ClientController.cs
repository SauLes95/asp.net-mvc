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
	}
}
