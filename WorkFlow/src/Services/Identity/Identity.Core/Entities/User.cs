using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;


namespace Identity.Core.Entities
{
	[CollectionName("users")]
	public class User : MongoIdentityUser<Guid>
	{
		
	}
}
