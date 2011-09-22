using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CandyShop.Reports;
using Codaxy.CodeReports.CodeModel;
using Codaxy.CodeReports.Controls;
using Codaxy.CodeReports.Data;
using Codaxy.CodeReports;

namespace CandyShop.App.CodeReports.RandomReports
{
	[Report(
		Path = "Folder1", 
		Title = "Report1"
	)]
	public class Report1 : IRandomReportGenerator
	{
		public IRandomReportFilter Filter
		{
			get;
			set;
		}

		[GroupingLevel(0, ShowHeader=true, ShowFooter = true, ShowCaption=true, CaptionFormat="Report1")]
		class Item
		{
			[TableColumn]
			public int V1 { get; set; }

			[TableColumn]
			public int V2 { get; set; }

			[TableColumn]
			public int V3 { get; set; }

			[TableColumn]
			public int V4 { get; set; }

			[TableColumn]
			public int V5 { get; set; }

		}

		DataContext GetData()
		{
			var dc = new DataContext();
			List<Item> t = new List<Item>();
			var r = new Random();
			for (var i = 0; i < Filter.Count; i++)
				t.Add(new Item
				{
					V1 = r.Next(),
					V2 = r.Next(),
					V3 = r.Next(),
					V4 = r.Next(),
					V5 = r.Next()
				});

			dc.AddTable("table", t);

			return dc;

		}

		public Report GenerateReport()
		{
			var flow = new Flow { Orientation = FlowOrientation.Vertical };
			flow.AddTable<Item>("table");
			return Report.CreateReport(flow, GetData());
		}
	}
}