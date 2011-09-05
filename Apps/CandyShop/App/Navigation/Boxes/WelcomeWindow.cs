using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codaxy.Dextop.Remoting;
using Codaxy.Dextop;

namespace CandyShop.App
{
    [Candy("navigation-boxes",
        Title = "Big Boxes", Category = "Navigation"
    )]
	public class WelcomeWindow : DextopWindow
	{
        [DextopRemotableConstructor(alias="navigation-boxes")]
        public WelcomeWindow() { }
	}
}