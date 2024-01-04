using Identity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories.Interfaces
{
	public interface IUserRepository
	{
		public Task<User> GetUserByUserId(Guid userId);
		public Task<User> GetUserByEmail(string email);
		public Task UpdateUser(User user);
		public Task CreateUser(User user);
		public Task RemoveUser(Guid userId);
	}
}
