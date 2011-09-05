Ext.define('CandyShop.CandyShop', {
	extend: 'Ext.window.Window',
	title: 'What can I get you?',
	width: 240,
	height: 500,
	x: 10,
	y: 10,
	iconCls: 'candy-shop',
	initComponent: function () {

		var store = Ext.create('Ext.data.TreeStore', {
			root: {
				expanded: true,
				children: CandyShop.Candies
			}
		});

		var tree = Ext.create('Ext.tree.Panel', {
			border: false,
			store: store,
			rootVisible: false,
			listeners: {
				scope: this,
				itemdblclick: function (tree, record) {
					if (record.raw.windowType) {
						Dextop.getSession().requestWindow(record.raw.windowType);
					}
				}
			}
		});

		Ext.apply(this, {
			closeAction: 'hide',
			layout: 'fit',
			items: [tree]			
		});

		this.callParent(arguments);
	}
});