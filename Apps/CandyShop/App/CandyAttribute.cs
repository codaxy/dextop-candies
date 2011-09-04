using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CandyShop.App
{
	public class CandyAttribute : System.Attribute
	{
		public CandyAttribute(String windowType)
		{
			WindowType = windowType;
		}
		public String WindowType { get; set; }
		public String Category { get; set; }
		public String Title { get; set; }
		public String Description { get; set; }
	}
}