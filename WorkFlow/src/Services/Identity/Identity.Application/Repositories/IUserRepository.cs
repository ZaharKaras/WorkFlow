using Identity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Repositories
{
	public interface IUserRepository
	{
		Task<User> GetUserByEmail(string email);
		Task UpdateUser(User user);
		Task<User> GetUserByUserId(Guid userId);
		Task CreateUser(User user);
	}
}
