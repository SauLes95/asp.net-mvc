using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.DAL
{
	public class ClientManagerDbContext : DbContext
	{
		protected ClientManagerDbContext() { }
		public ClientManagerDbContext(DbContextOptions<ClientManagerDbContext> options) : base(options)
		{ }
	}
}