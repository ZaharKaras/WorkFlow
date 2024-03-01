using System.ServiceModel.Syndication;

namespace RSS.Core.Interfaces
{
	public interface IParseService
	{
		SyndicationFeed ParseFeedAsync(string uri);
		IEnumerable<SyndicationItem> ParseItemAsync(string uri);
	}
}
