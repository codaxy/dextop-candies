using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CandyShop.Reports;

namespace CandyShop.App.CodeReports.RandomReports
{
	public interface IRandomReportGenerator : IReportGenerator
	{
		IRandomReportFilter Filter { get; set; }
	}
}