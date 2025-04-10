using Microsoft.AspNetCore.Mvc;
using Vjezba.Web.Mock;

namespace Vjezba.Web.Controllers
{
	public class ClientController : Controller
	{
		public IActionResult Index()
		{
			List<Client> clientList = MockClientRepository.Instance.All().ToList();

			return View(clientList);
		}
	}
}
