using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandyShop.Reports
{
	public class ReportAttribute : System.Attribute
	{
		public String Path { get; set; }
		public String Title { get; set; }
		public int Priority { get; set; }

		public String[] DisabledField { get; set; }
		public String[] RequiredFields { get; set; }
	}


}
