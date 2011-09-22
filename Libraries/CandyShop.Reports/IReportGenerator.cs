using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codaxy.CodeReports;

namespace CandyShop.Reports
{
	public interface IReportGenerator
	{
		Report GenerateReport();
	}
}
