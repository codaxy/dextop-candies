using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CandyShop.Reports;
using Codaxy.Dextop.Remoting;
using Codaxy.Dextop.Forms;

namespace CandyShop.App.CodeReports.RandomReports
{
	[Candy("random-report-selector",
		Title = "Random Reports", 
		Description = "It's really easy to add new reports...",	
		Category = "Reports"
	)]
	public class RandomReportSelector : ReportSelectorWindowBase<IRandomReportGenerator, RandomReportSelector.Filter>
	{
		[DextopRemotableConstructor(alias="random-report-selector")]
		public RandomReportSelector() { }

		protected override Reports.IReportEngine<IRandomReportGenerator> CreateReportEngine()
		{
			var engine = new ReportEngine<IRandomReportGenerator, ReportAttribute>();
			engine.Assemblies.Add(this.GetType().Assembly);
			return engine;
		}

		protected override void ApplyReportFilter(IRandomReportGenerator generator, Filter filter)
		{
			generator.Filter = filter;
		}

		protected override RandomReportSelector.Filter CreateInitialFilterData()
		{
			return new Filter
			{
				Title = "Default Title",
				Count = 100
			};
		}		

		[DextopForm]
		public class Filter : IRandomReportFilter
		{
			[DextopFormField(allowBlank=false, anchor="0")]
			public String Title { get; set; }


			[DextopFormField()]
			public int Count { get; set; }
		}
	}
}