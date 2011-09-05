Ext.define('Desktop.WindowModule', {
	extend: 'Ext.ux.desktop.Module',

	init: function () {
		this.windowType = this.windowType || this.id;
		Ext.applyIf(this.launcher, {
			handler: this.createWindow,
			scope: this
		});
	},

	createWindow: function () 
	{
		Dextop.getSession().requestWindow(this.windowType, this.windowArgs);
	}
});