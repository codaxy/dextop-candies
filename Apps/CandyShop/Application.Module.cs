using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codaxy.Dextop;
using CandyShop.App;

namespace CandyShop
{
    public class AppModule : DextopModule
    {
        protected override void InitNamespaces()
        {
			RegisterNamespaceMapping("CandyShop*", "CandyShop");
        }

        protected override void InitResources()
        {
            RegisterJs("sample", "", 
                "client/js/generated/",
                "client/js/",
                "App/*/");

            RegisterCss("client/css/site.css");
            
            RegisterCss("App/Navigation/Boxes/welcome.css");
        }

        public override string ModuleName
        {
            get { return "sample"; }
        }

        protected override void RegisterAssemblyPreprocessors(Dictionary<string, IDextopAssemblyPreprocessor> preprocessors)
        {
            RegisterStandardAssemblyPreprocessors("client/js/generated", preprocessors);
			preprocessors.Add("client/js/generated/candies.js", new CandyPreprocessor());
        }
    }
}