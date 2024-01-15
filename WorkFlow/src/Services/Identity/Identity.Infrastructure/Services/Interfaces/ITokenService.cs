using Identity.API.DTOs;
using Identity.Core.Entities;
using Identity.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services.Interfaces
{
	public interface ITokenService
	{
		public Task<AuthResult> GenerateToken(User user);
		public Task<AuthResult?> VerifyAndGenerateToken(TokenRequest tokenRequest);

	}
}
