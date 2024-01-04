using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Entities
{
	public class RefreshToken
	{
		public string Value { get; set; }
		public bool Active { get; set; }
		public DateTime ExpirationDate { get; set; }
	}
}
