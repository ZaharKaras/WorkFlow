using RSS.Application.Services.Interfaces;
using Serilog;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RSS.Application.Services
{
	public class ParseService : IParseService
	{
		public SyndicationFeed ParseFeedAsync(string uri)
		{
			using var reader = XmlReader.Create(uri);

			var feed =  SyndicationFeed.Load(reader);

			feed.BaseUri = new Uri(uri);

			return feed;
		}

		public IEnumerable<SyndicationItem> ParseItemAsync(string uri)
		{
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.DtdProcessing = DtdProcessing.Parse;
			settings.MaxCharactersFromEntities = 15;

			var items = new List<SyndicationItem>();

			using (var reader = XmlReader.Create(uri, settings))
			{
				while (reader.ReadToFollowing("item"))
				{
					var item = ParseSingleItem(reader);
					items.Add(item);
				}
			}

			Log.Information("Service send parsed xml");

			return items;
		}

		private SyndicationItem ParseSingleItem(XmlReader reader)
		{
			string title = string.Empty;
			string link = string.Empty;
			string description = string.Empty;
			string pubDate = string.Empty;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					switch (reader.Name)
					{
						case "title":
							title = reader.ReadElementContentAsString();
							break;
						case "link":
							link = reader.ReadElementContentAsString();
							break;
						case "description":
							description = reader.ReadElementContentAsString();
							break;
						case "pubDate":
							pubDate = reader.ReadElementContentAsString();
							break;
						default:
							reader.Skip();
							break;
					}
				}

				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "item")
				{
					break;
				}
			}

			var item = new SyndicationItem(
				title,
				description,
				new Uri(link),
				Guid.NewGuid().ToString(),
				DateTimeOffset.Parse(pubDate)
			);

			return item;
		}
	}
}
