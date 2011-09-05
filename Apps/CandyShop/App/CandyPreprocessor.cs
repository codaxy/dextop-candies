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
			public String windowType { get; set; }
			public bool leaf { get; set; }
			public String iconCls { get; set; }

			public List<Model> children;
		}



		public void ProcessAssemblies(DextopApplication application, IList<System.Reflection.Assembly> assemblies, System.IO.Stream outputStream)
		{
            var root = new Model { children = new List<Model>() };

			
			foreach (var assembly in assemblies)
			{
				var dict = Codaxy.Common.Reflection.AssemblyHelper.GetTypeAttributeDictionaryForAssembly<CandyAttribute>(assembly, false);
				foreach (var v in dict.OrderBy(a => a.Value.Category).ThenBy(a => a.Value.Title))
				{
                    Model parent;
                    if (String.IsNullOrEmpty(v.Value.Category))
                        parent = root;
                    else
                    {
                        parent = root;
                        var crumbs = v.Value.Category.Split('\\', '/');
                        foreach (var crumb in crumbs)
                        {
                            if (parent.children == null)
                                parent.children = new List<Model>();
                            var node = parent.children.FirstOrDefault(a => a.text == crumb);
                            if (node == null)
                            {
                                node = new Model { text = crumb };
                                parent.children.Add(node);
                            }                            
                            node.leaf = false;
                            parent = node;
                        }
                        
                    }
					var m = new Model
					{
						tooltip = v.Value.Description,
						text = v.Value.Title,
						windowType = v.Value.WindowType,
						leaf = true,
					};

                    if (parent.children == null)
                        parent.children = new List<Model>();
					parent.children.Add(m);
				}
			}
			using (var w = new StreamWriter(outputStream))
			{
				w.WriteLine("Ext.ns('CandyShop');");
				w.Write("CandyShop.Candies = ");
				w.Write(DextopUtil.Encode(root.children));
				w.WriteLine(";");
			}
		}
	}
}