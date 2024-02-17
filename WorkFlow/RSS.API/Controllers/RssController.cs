using Microsoft.AspNetCore.Mvc;
using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RSS.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RssController : ControllerBase
	{

		[HttpPost]
		[Route("feeds")]
		public async Task<ActionResult<SyndicationItem>> GetFeedAsync(
			[FromBody] string url, CancellationToken token)
		{
			using var reader = XmlReader.Create(url);
			var feed = SyndicationFeed.Load(reader);

			var post = feed.Items.FirstOrDefault();

			return Ok(post);
		}

	}
}
