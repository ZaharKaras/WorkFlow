using Identity.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.UseCases.Login
{
	public class LoginQuery : IRequest<User>
	{
		public string Email { get; set; }
		public string Password { get; set; }

		public LoginQuery(string email, string password)
		{
			Email = email;
			Password = password;
		}	
	}
}
