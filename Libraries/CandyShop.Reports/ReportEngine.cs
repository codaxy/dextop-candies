using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CandyShop.Reports
{

	public class ReportGeneratorInfo
	{
		public String Id { get; set; }
		public ReportAttribute ReportAttribute { get; set; }
		public Type ReportGeneratorType { get; set; }
	};

	public interface IReportEngine<IRG> where IRG : IReportGenerator
	{
		IList<ReportGeneratorInfo> GetReportTypes();
		IRG CreateReportGenerator(String id, out ReportGeneratorInfo info);
	}

	public class ReportEngine<IRG, RA> : IReportEngine<IRG>
		where IRG : IReportGenerator
		where RA: ReportAttribute
	{
		public ReportEngine()
		{
			Assemblies = new List<Assembly>();
		}

		public List<Assembly> Assemblies { get; private set; }

		public List<ReportGeneratorInfo> reportCache;

		public IList<ReportGeneratorInfo> GetReportTypes()
		{
			if (reportCache != null)
				return reportCache;
			
			reportCache = new List<ReportGeneratorInfo>();
			var irgType = typeof(IRG);
			foreach (var a in Assemblies)
			{
				var tad = Codaxy.Common.Reflection.AssemblyHelper.GetTypeAttributeDictionaryForAssembly<RA>(a, false);
				foreach (var ta in tad)
				{
					if (!irgType.IsAssignableFrom(ta.Key))
						continue;
					var rgi = new ReportGeneratorInfo
					{
						ReportAttribute = ta.Value,
						ReportGeneratorType = ta.Key,
						Id = Math.Abs(ta.Value.GetHashCode() + ta.Key.GetHashCode()).ToString()
					};
					reportCache.Add(rgi);					
				}
			}
			return reportCache;
		}

		public virtual IRG CreateReportGenerator(String id, out ReportGeneratorInfo info)
		{
			if (reportCache == null)
				throw new InvalidOperationException("Report engine is not initialized.");
			info = reportCache.Find(a => a.Id == id);
			var generator = Activator.CreateInstance(info.ReportGeneratorType);
			var gen = (IRG)generator;
			InitReportGenerator(gen);
			return gen;
		}

		protected virtual void InitReportGenerator(IRG generator)
		{
			if (OnInitReportGenerator != null)
				OnInitReportGenerator(generator);
		}

		public Action<IRG> OnInitReportGenerator;
	}
}
