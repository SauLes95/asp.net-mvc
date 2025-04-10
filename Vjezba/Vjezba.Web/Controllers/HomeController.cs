using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Collections.Immutable;
using System.Diagnostics;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
    public class HomeController(
        ILogger<HomeController> _logger) 
        : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Jednostavan način proslijeđivanja poruke iz Controller -> View.";

            return View();
        }

		public IActionResult FAQ(int? selected = null)
		{
            ViewBag.OdabranoPitanje = selected;
			return View();
		}

		/// <summary>
		/// Ova akcija se poziva kada na formi za kontakt kliknemo "Submit"
		/// URL ove akcije je /Home/SubmitQuery, uz POST zahtjev isključivo - ne može se napraviti GET zahtjev zbog [HttpPost] parametra
		/// </summary>
		/// <param name="formData"></param>
		/// <returns></returns>
		[HttpPost]
        public IActionResult SubmitQuery(IFormCollection formData)
        {
            var fullName = formData["firstName"] + " " + formData["lastName"];
			var email = formData["email"];
			var message = formData["message"];
            var messageType = formData["messageType"];
			var newsletter = formData["newsletter"] == "on";
			string newsletterMessage = newsletter ? "obavijestit ćemo vas" : "nećemo vas obavijestiti";

            TempData["Message"] = $"Poštovani {fullName} ({email}) zaprimili smo vašu poruku te će vam se netko ubrzo javiti. Sadržaj vaše poruke je: [{messageType}] {message}. Također, {newsletterMessage} o daljnjim promjenama preko newslettera.";

			return RedirectToAction("ContactSuccess");
		}
		public IActionResult ContactSuccess()
		{
            ViewBag.Message = TempData["Message"];

			return View(viewName: "ContactSuccess");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new { RequestId = "Auditorne" });
        }
    }
}