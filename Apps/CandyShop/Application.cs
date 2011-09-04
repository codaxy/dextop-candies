using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codaxy.Dextop;
using Codaxy.Dextop.Modules;

namespace CandyShop
{
    public class Application : DextopApplication
    {
        protected override void RegisterModules()
        {
			RegisterModule("client/lib/ext", new DextopExtJSModule() { Debug = true });
            RegisterModule("client/lib/ext/examples/desktop", new ExtDesktopModule());
            RegisterModule("client/lib/dextop", new DextopCoreModule());
            RegisterModule("", new AppModule());            
        }

        protected override void OnModulesInitialized()
        {
            base.OnModulesInitialized();
#if !DEBUG
            OptimizeModules("client/js/cache");
#endif
        }
    }
}