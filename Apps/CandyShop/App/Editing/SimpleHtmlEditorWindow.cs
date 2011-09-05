using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codaxy.Dextop.Remoting;
using Codaxy.Dextop;

namespace CandyShop.App
{
    [Candy("simple-html-editor",
        Title = "Simple HTML Editor", Category = "HTML Editing"
    )]
	public class SimpleHtmlEditorWindow : DextopWindow
	{
        [DextopRemotableConstructor(alias="simple-html-editor")]
        public SimpleHtmlEditorWindow() { }

		[DextopRemotable]
		public String UploadContent(String content)
		{
			return String.Format("HTML content of length {0:#,#0} has been uploaded.", content.Length);
		}
	}
}