using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vjezba.DAL;
using Vjezba.Model;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
	public class ClientController : Controller
    {
		private readonly ClientManagerDbContext _dbContext;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ClientController(ClientManagerDbContext dbContext, IWebHostEnvironment webHostEnvironment)
		{
			_dbContext = dbContext;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index(ClientFilterModel filter = null)
        {
			filter ??= new ClientFilterModel();

			var clientQuery = _dbContext.Clients.Include(p => p.City).AsQueryable();

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
		[HttpPost]
		public IActionResult IndexAjax([FromForm] ClientFilterModel filter)
		{

			var clientQuery = _dbContext.Clients.Include(p => p.City).AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter.FullName))
				clientQuery = clientQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(filter.FullName.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Address))
				clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Email))
				clientQuery = clientQuery.Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.City))
				clientQuery = clientQuery.Where(p => p.CityID != null && p.City.Name.ToLower().Contains(filter.City.ToLower()));

			var model = clientQuery.ToList();
			return PartialView("_IndexTable", model);
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
			this.FillDropdownValues();
			return View();
		}

		[HttpPost]
		public IActionResult Create(Client model)
		{
			if (ModelState.IsValid)
			{
				_dbContext.Clients.Add(model);
				_dbContext.SaveChanges();

				return RedirectToAction(nameof(Index));
			}
			else
			{
				this.FillDropdownValues();
				return View();
			}
		}

		[ActionName(nameof(Edit))]
		public IActionResult Edit(int id)
		{
			var model = _dbContext.Clients
				.Include(c => c.Attachments)
				.FirstOrDefault(c => c.ID == id);
			this.FillDropdownValues();
			return View(model);
		}

		[HttpPost]
		[ActionName(nameof(Edit))]
		public async Task<IActionResult> EditPost(int id)
		{
			var client = _dbContext.Clients
				.Include(c => c.Attachments)
				.Single(c => c.ID == id);
			var ok = await this.TryUpdateModelAsync(client);

			if (ok && this.ModelState.IsValid)
			{
				_dbContext.SaveChanges();
				return RedirectToAction(nameof(Index));
			}

			this.FillDropdownValues();
			return View();
		}

		private void FillDropdownValues()
		{
			var selectItems = new List<SelectListItem>();

			//Polje je opcionalno
			var listItem = new SelectListItem();
			listItem.Text = "- odaberite -";
			listItem.Value = "";
			selectItems.Add(listItem);

			foreach (var category in _dbContext.Cities)
			{
				listItem = new SelectListItem(category.Name, category.ID.ToString());
				selectItems.Add(listItem);
			}

			ViewBag.PossibleCities = selectItems;
		}

		[HttpPost]
		public async Task<IActionResult> UploadAttachment(int clientID, IFormFile file)
		{
			if (file != null && file.Length > 0)
			{
				// Osiguraj da folder postoji
				var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Attachments");
				if (!Directory.Exists(folderPath))
					Directory.CreateDirectory(folderPath);

				var attachmentPath = Path.Combine(folderPath, file.FileName);

				using (var stream = new FileStream(attachmentPath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				var client = _dbContext.Clients.SingleOrDefault(c => c.ID == clientID);
				if (client == null)
					return NotFound();

				var attachment = new Attachment
				{
					ClientID = clientID,
					FileName = file.FileName,
					FilePath = "/Attachments/" + file.FileName, // bitno: bez "wwwroot"
					Client = client
				};

				_dbContext.Attachments.Add(attachment);
				await _dbContext.SaveChangesAsync();

				// Dropzone očekuje JSON, ne redirect
				return Ok(new { success = true, fileName = file.FileName });
			}

			return BadRequest(new { error = "File is missing or empty." });
		}

		public IActionResult GetAttachments(int clientID)
		{
			var attachments = _dbContext.Attachments
				.Where(a => a.ClientID == clientID)
				.ToList();
			return PartialView("_AttachmentList", attachments);
		}


		public IActionResult DeleteAttachment(int attachmentID, int clientID)
		{
			var attachment = _dbContext.Attachments.Find(attachmentID);
			if (attachment != null)
			{
				var attachmentPath = Path.Combine(_webHostEnvironment.WebRootPath, "Attachments", attachment.FileName);
				if (System.IO.File.Exists(attachmentPath))
				{
					System.IO.File.Delete(attachmentPath);
				}

				_dbContext.Attachments.Remove(attachment);
				_dbContext.SaveChanges();
			}

			return RedirectToAction(nameof(Details), new { id = clientID });
		}

	}
}
