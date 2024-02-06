using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace Identity.Core.Entities
{
	[CollectionName("refresh_tokens")]
	public class RefreshToken
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }
		public string? UserId { get; set; }
		public string? Token {  get; set; }
		public string? JwtId { get; set; }
		public bool IsUsed {  get; set; }
		public bool IsRevoked { get; set; }
		public DateTime AddedDate { get; set; }
		public DateTime ExpiryDate { get; set; }
	}
}
