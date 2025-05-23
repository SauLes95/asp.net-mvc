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
				.Select(MapFromClient)
				.ToList();

			return Ok(clients);
		}

		[Route("{id:int}")]
		public IActionResult Get(int id)
		{
			var clients = _dbContext.Clients
				.Include(p => p.City)
				.Where(p => p.ID == id)
				.Select(MapFromClient)
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
				.Select(MapFromClient)
				.ToList();

			if (clients == null)
			{
				return NotFound();
			}

			return Ok(clients);
		}


		[HttpPost]
		public IActionResult Post([FromBody] ClientDTO client)
		{
			if (string.IsNullOrEmpty(client.FullName))
			{
				return BadRequest();
			}

			var clientDb = new Client
			{
				FirstName = client.FullName.Split(" ")[0],
				LastName = client.FullName.Split(" ")[1],
				Address = client.Address,
				CityID = client.City.Id,
				Email = client.Email
			};

			_dbContext.Clients.Add(clientDb);
			_dbContext.SaveChanges();

			return Get(clientDb.ID);
		}

		[HttpPut]
		[Route("{id:int}")]
		public IActionResult Put(int id, [FromBody] ClientDTO client)
		{

			var clientDb = _dbContext.Clients.Find(id);
			if (clientDb == null) return NotFound(new { Message = "Client not found" });

			if (!string.IsNullOrWhiteSpace(client.FullName))
			{
				clientDb.FirstName = client.FullName.Split(" ")[0];
				clientDb.LastName = client.FullName.Split(" ")[1];
			}
			if (!string.IsNullOrWhiteSpace(client.Address)) clientDb.Address = client.Address;
			if (client.City != null) clientDb.CityID = client.City.Id;
			if (!string.IsNullOrWhiteSpace(client.Email)) clientDb.Email = client.Email;

			_dbContext.SaveChanges();
			return Ok(client);
		}


		private ClientDTO MapFromClient(Client client)
		{
			if (client.City == null)
			{
				client.City = _dbContext.Cities.Find(client.CityID);
			}

			if (client.City == null)
			{
				return new ClientDTO
				{
					ID = client.ID,
					FullName = client.FirstName + " " + client.LastName,
					Address = client.Address,
					Email = client.Email,
					City = null
				};
			}
			return new ClientDTO
			{
				ID = client.ID,
				FullName = client.FirstName + " " + client.LastName,
				Address = client.Address,
				City = new CityDTO
				{
					Id = client.City.ID,
					Name = client.City.Name,
					NumClients = _dbContext.Clients.Count(c => c.City.ID == client.CityID)
				},
				Email = client.Email
			};

		}

	} 
}
