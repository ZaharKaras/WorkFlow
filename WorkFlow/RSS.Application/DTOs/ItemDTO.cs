namespace RSS.Application.DTOs;
public record ItemDTO(string Title, string Uri, DateTime PubDate);

public class Item
{
	public string Title { get; set; } = string.Empty;
	public string Uri { get; set; } = string.Empty;
	public DateTime PubDate { get; set; } = DateTime.Now;
}

