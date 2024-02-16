using System;
using System.Xml;

namespace RSS.Core.Entities
{
	public interface NodeInformation
	{
		public string? Name { get; set; }
		public string? Prefix { get; set; }
		public string? LocalName { get; set; }
		public XmlNodeType? Type { get; set; }
		public string? Value { get; set; }
		public string? Namespace { get; set; }
		public int? LineNumber { get; set; }
		public int? LinePosition { get; set; }
		public int? Depth { get; set; }
		public bool? IsEmpty { get; set; }
		public bool? HasAttributes { get; set; }
	}
}
