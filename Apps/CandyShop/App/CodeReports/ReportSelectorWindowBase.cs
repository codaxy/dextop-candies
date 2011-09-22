using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codaxy.Dextop.Data;
using Codaxy.Dextop;
using Codaxy.Dextop.Remoting;
using Codaxy.Xlio;
using Codaxy.Xlio.IO;
using Codaxy.CodeReports;
using CandyShop.Reports;

namespace CandyShop.App.CodeReports
{
	public abstract class ReportSelectorWindowBase<IRG, Filter> : DextopWindow
		where IRG : IReportGenerator
	{
		protected abstract IReportEngine<IRG> CreateReportEngine();
		protected abstract void ApplyReportFilter(IRG generator, Filter filter);

		IReportEngine<IRG> reportEngine;

		public override void InitRemotable(DextopRemote remote, DextopConfig config)
		{
			base.InitRemotable(remote, config);
			reportEngine = CreateReportEngine();
			config["data"] = CreateInitialFilterData();
			config["reportTree"] = BuildReportTree();
		}

		protected abstract Filter CreateInitialFilterData();

		[DextopRemotable]
		public DextopConfig PreviewReport(String reportId, Filter filter)
		{
			ReportGeneratorInfo info;
			var reportGenerator = reportEngine.CreateReportGenerator(reportId, out info);
			ApplyReportFilter(reportGenerator, filter);
			var report = reportGenerator.GenerateReport();
			var previewWindow = new ReportPreviewWindow(report);
			return Remote.Register(previewWindow);

		}

		class ReportNode
		{			
			public String text { get; set; }
			public String tooltip { get; set; }			
			public bool leaf { get; set; }
			public String iconCls { get; set; }
			public String reportId { get;set;}

			public List<ReportNode> children;

			public bool expanded { get; set; }
		}

		ReportNode BuildReportTree()
		{
			var root = new ReportNode
			{
				children = new List<ReportNode>(),
				text = ""
			};
			List<ReportNode> parents = new List<ReportNode>();
			parents.Add(root);
			var data = reportEngine.GetReportTypes();
			foreach (var item in data.OrderBy(a => a.ReportAttribute.Path).ThenBy(a => a.ReportAttribute.Priority).ThenBy(a => a.ReportAttribute.Title))
				AddNode(parents, item);
			return root;
		}

		void AddNode(List<ReportNode> parents, ReportGeneratorInfo info)
		{
			var crumbs = ("\\"+(info.ReportAttribute.Path ?? "").TrimStart('\\')).Split('\\', '/');
			ReportNode parent = parents.Last();
			var i = 0;
			for (; i < parents.Count && i < crumbs.Length; i++)
				if (parents[i].text != crumbs[i])
					break;

			if (i < parents.Count)
				parents.RemoveRange(i, parents.Count - i);

			for (var j = i; j < crumbs.Length; j++)
			{
				var node = new ReportNode()
				{
					children = new List<ReportNode>(),
					leaf = false,
					expanded = true,
					text = crumbs[j]
				};
				parents.Last().children.Add(node);
				parents.Add(node);
			}

			parents.Last().children.Add(new ReportNode
			{
				text = info.ReportAttribute.Title,
				leaf = true,
				reportId = info.Id
			});
		}
	}
}