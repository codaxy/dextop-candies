using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codaxy.Dextop.Data;
using Codaxy.Dextop;
using Codaxy.Dextop.Remoting;
using Codaxy.Xlio;
using Codaxy.Xlio.IO;

namespace CandyShop.App.GMaps
{
    [Candy("gmap-picker",
        Title = "Pick a place in Google Maps", Category = "Google Maps"
    )]
    public class GMapPicker : DextopWindow
    {
        [DextopRemotableConstructor(alias = "gmap-picker")]
        public GMapPicker() { }
    }
}