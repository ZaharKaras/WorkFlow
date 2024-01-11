using Identity.API.DTOs;
using Identity.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface IIdentityService
	{
		public Task<AuthResult> Register(RegistrationRequest registerRequest);
		public Task<AuthResult> Login(LoginRequest loginRequest);
		public Task<AuthResult?> RefreshToken(TokenRequest tokenRequest);
		public Task<AuthResult?> Logout(TokenRequest tokenRequest);

	}
}
