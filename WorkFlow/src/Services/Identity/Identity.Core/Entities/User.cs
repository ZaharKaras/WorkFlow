using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Identity.Core.Entities
{
	[CollectionName("users")]
	public class User : MongoIdentityUser<Guid>
	{
		
	}
}
