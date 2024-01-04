using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Entities
{
	public class User
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public IList<Claim> Claims { get; set; }
		public RefreshToken RefreshToken { get; set; }
		public User()
		{
			Claims = new List<Claim>();
			RefreshToken = new RefreshToken();
		}
	}
}
