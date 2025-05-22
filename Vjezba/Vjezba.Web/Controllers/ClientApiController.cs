using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vjezba.DAL;
using Vjezba.Model;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
	[ApiController]
	[Route("api/client")]
	public class ClientApiController(ClientManagerDbContext _dbContext) : Controller
	{ 
		public IActionResult Get()
		{
			var clients = _dbContext.Clients
				.Include(p => p.City)
				.Select(p => new ClientDTO()
				{
					ID = p.ID,
					FullName = p.FirstName + " " + p.LastName,
					Address = p.Address,
					Email = p.Email,
					City = p.City
				})
				.ToList();

			return Ok(clients);
		}

		[Route("{id}")]
		public IActionResult Get(int id)
		{
			var clients = _dbContext.Clients
				.Include(p => p.City)
				.Where(p => p.ID == id)
				.Select(p => new ClientDTO()
				{
					ID = p.ID,
					FullName = p.FirstName + " " + p.LastName,
					Address = p.Address,
					Email = p.Email,
					City = p.City
				})
				.FirstOrDefault();

			if (clients == null)
			{
				return NotFound();
			}

			return Ok(clients);
		}

		[Route("pretraga/{q}")]
		public IActionResult Get(string q)
		{
			var clients = _dbContext.Clients
				.Include(p => p.City)
				.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(q.ToLower()))
				.Select(p => new ClientDTO()
				{
					ID = p.ID,
					FullName = p.FirstName + " " + p.LastName,
					Address = p.Address,
					Email = p.Email,
					City = p.City
				})
				.ToList();

			if (clients == null)
			{
				return NotFound();
			}

			return Ok(clients);
		}
	}

	public class ClientDTO
	{
		public int ID { get; set; }
		public string FullName { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public City City { get; set; }
	}

}
