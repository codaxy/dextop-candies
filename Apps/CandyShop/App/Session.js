Ext.define('CandyShop.Session', {
	extend: 'Dextop.Session',

	initSession: function () {
		this.callParent(arguments);
		this.initDesktop();
	},

	initDesktop: function () {
		this.app = Ext.create('Desktop.App', {
			session: this
		});
		this.showCandyShop();
	},

	showCandyShop: function () {
		if (!this.candyShop)
			this.candyShop = new CandyShop.CandyShop({});
		this.candyShop.show();
	},

	addDesktopWindow: function (win) {
		this.app.getDesktop().addWindow(win);
	},

	requestWindow: function (windowType, windowArgs) {
		Dextop.getSession().remote.Instantiate(windowType, windowArgs, {
			type: 'alert',
			success: function (result) {
				var win= Dextop.create(result);
				Dextop.getSession().addDesktopWindow(win);
				win.show();
			}
		});
	}
});