using System.ServiceModel.Syndication;

namespace RSS.Application.Services.Interfaces
{
	public interface IParseService
	{
		SyndicationFeed ParseFeedAsync(string uri);
		IEnumerable<SyndicationItem> ParseItemAsync(string uri);
	}
}
