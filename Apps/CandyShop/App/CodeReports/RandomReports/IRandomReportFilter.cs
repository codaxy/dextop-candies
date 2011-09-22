using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CandyShop.App.CodeReports.RandomReports
{
	public interface IRandomReportFilter
	{
		String Title { get; set; }

		int Count { get; set; }
	}
}