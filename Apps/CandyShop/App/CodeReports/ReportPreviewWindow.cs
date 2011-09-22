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
using System.Web.UI;
using Codaxy.CodeReports.Exporters.Html;
using Codaxy.CodeReports.Exporters.Xlio;
using Codaxy.CodeReports.Styling;
using Codaxy.WkHtmlToPdf;

namespace CandyShop.App.CodeReports
{
	public class ReportPreviewWindow : DextopWindow		
	{
		Report Report;
		public ReportPreviewWindow(Report report)
		{
			Report = report;
		}

		public override void InitRemotable(DextopRemote remote, DextopConfig config)
		{
			base.InitRemotable(remote, config);
			Remote.OnProcessAjaxRequest = ProcessAjaxRequest;
		}

		void ProcessAjaxRequest(HttpContext context)
		{
			try
			{
				switch (context.Request.QueryString["type"])
				{
					case "html":
						context.Response.BufferOutput = true;
						using (var htmlWriter = new HtmlTextWriter(context.Response.Output))
						{
							var htmlGen = new Codaxy.CodeReports.Exporters.Html.HtmlReportWriter(htmlWriter);
							htmlGen.RegisterCss(DextopUtil.AbsolutePath("client/css/report.css"));
							htmlGen.Write(Report, new DefaultHtmlReportTheme());
						}
						break;
					case "xlsx":
						context.Response.BufferOutput = true;
						var xlsxGen = new XlsxReportWriter();
						context.Response.ContentType = "application/force-download";
						context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Guid.NewGuid().ToString() + ".xlsx" + "\"");
						XlsxReportWriter.WriteToStream(Report, Themes.Default, context.Response.OutputStream);
						break;
					case "pdf":
						context.Response.BufferOutput = true;
						context.Response.ContentType = "application/force-download";
						context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Guid.NewGuid().ToString() + ".pdf" + "\"");						
						PdfConvert.ConvertHtmlToPdf(new PdfDocument
						{
							Url = context.Request.Url.ToString().Replace("=pdf", "=html")
						}, new PdfOutput
						{
							OutputStream = context.Response.OutputStream
						});
						break;
					default:
						context.Response.ContentType = "text/plain";
						context.Response.Write("Invalid options");
						break;
				}
			}
			catch (Exception ex)
			{
				context.Response.ContentType = "text/plain";
				context.Response.Clear();
				context.Response.Output.WriteLine("Error occured: " + ex);				
			}
		}
			
	}
}