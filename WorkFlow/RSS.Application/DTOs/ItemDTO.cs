namespace RSS.Application.DTOs;

public class ItemDTO
{
	public string Title { get; set; } = string.Empty;
	public string Uri { get; set; } = string.Empty;
	public DateTime PubDate { get; set; } = DateTime.Now;
}

