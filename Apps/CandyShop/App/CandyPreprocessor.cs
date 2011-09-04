using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codaxy.Dextop;
using System.IO;

namespace CandyShop.App
{
	public class CandyPreprocessor : IDextopAssemblyPreprocessor
	{
		class Model
		{
			public String text { get; set; }			
			public String tooltip { get; set; }
			public String id { get; set; }
			public bool leaf { get; set; }
			public String iconCls { get; set; }

			public Model[] children;
		}



		public void ProcessAssemblies(DextopApplication application, IList<System.Reflection.Assembly> assemblies, System.IO.Stream outputStream)
		{
			List<Model> list = new List<Model>();
			foreach (var assembly in assemblies)
			{
				var dict = Codaxy.Common.Reflection.AssemblyHelper.GetTypeAttributeDictionaryForAssembly<CandyAttribute>(assembly, false);
				foreach (var v in dict.OrderBy(a => a.Value.Category).ThenBy(a => a.Value.Title))
				{
					var m = new Model
					{
						tooltip = v.Value.Description,
						text = v.Value.Title,
						id = v.Value.WindowType,
						leaf = true,
					};

					list.Add(m);
				}
			}
			using (var w = new StreamWriter(outputStream))
			{
				w.WriteLine("Ext.ns('CandyShop');");
				w.Write("CandyShop.Candies = ");
				w.Write(DextopUtil.Encode(list));
				w.WriteLine(";");
			}
		}
	}
}