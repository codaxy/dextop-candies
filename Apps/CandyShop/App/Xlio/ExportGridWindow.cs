using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codaxy.Dextop.Data;
using Codaxy.Dextop;
using Codaxy.Dextop.Remoting;
using Codaxy.Xlio;
using Codaxy.Xlio.IO;

namespace CandyShop.App.Xlio
{
	[Candy("xlio-grid-export",
		Title = "Export Grid to Excel"
	)]
	public class ExportGridWindow : DextopWindow
	{
        [DextopRemotableConstructor(alias = "xlio-grid-export")]
        public ExportGridWindow() {}

		DextopMemoryDataProxy<Model, int> crud;

		public override void InitRemotable(DextopRemote remote, DextopConfig config)
		{
			base.InitRemotable(remote, config);
			crud = new DextopMemoryDataProxy<Model, int>(a => a.Id, (lastId) => { return ++lastId; }) { 
				 Paging = true
			};
			Remote.AddStore("model", crud);
			Remote.OnProcessAjaxRequest = ProcessAjaxRequest;
		}

		void ProcessAjaxRequest(HttpContext context)
		{
			String msg = null;

			if (context.Request.QueryString["type"] == "xlsx")
			{
				try
				{
					var wb = new Workbook();
					var sheet = new Sheet("Exported");
					sheet[0, 0].Value = "First Name";
					sheet[0, 1].Value = "Last Name";
					sheet[0, 2].Value = "Company Name";
					for (var i = 0; i < crud.Count; i++)
					{
						sheet[i + 1, 0].Value = crud.Records[i].FirstName;
						sheet[i + 1, 1].Value = crud.Records[i].LastName;
						sheet[i + 1, 2].Value = crud.Records[i].CompanyName;
					}
					wb.Sheets.AddSheet(sheet);

					context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
					context.Response.Buffer = true;
					context.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xlsx", DateTime.Now.Ticks));
					wb.SaveToStream(context.Response.OutputStream, XlsxFileWriterOptions.AutoFit);
					return;
				}
				catch (Exception ex)
				{
					msg = ex.ToString();
				}
			}

			context.Response.Clear();
			context.Response.ContentType = "text/plain";
			context.Response.Output.Write(msg ?? "Invalid params.");
		}

		[DextopGrid]
		[DextopModel]
		class Model
		{
			[DextopModelId()]
			public int Id { get; set; }

			[DextopGridColumn(flex = 1)]
			public String FirstName { get; set; }

			[DextopGridColumn(flex = 1)]
			public String LastName { get; set; }

			[DextopGridColumn(flex=1)]
			public String CompanyName { get; set; }
		}
	}
}