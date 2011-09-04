Ext.define('CandyShop.CandyModel', {
	extend: 'Ext.data.Model',
	fields: [
            { name: 'id', type: 'string' },
            { name: 'text', type: 'string' },
            { name: 'tooltip', type: 'string' }
        ]
});

Ext.define('CandyShop.CandyShop', {
	extend: 'Ext.window.Window',
	title: 'What can I get you?',
	width: 240,
	height: 500,
	x: 10,
	y: 10,
	iconCls: 'candy-shop',
	initComponent: function () {

		var store = Ext.define('Ext.data.TreeStore', {
			proxy: {
				model: 'CandyShop.CandyModel',
				type: 'memory',
				data: CandyShop.Candies
			},
			root: {
				expanded: true,
				id: 'root'
			}
		});

		var tree = Ext.define('Ext.tree.Panel', {
			border: false,
			store: store,
			rootVisible: true
		});

		Ext.apply(this, {
			closeAction: 'hide',
			layout: 'fit',
			items: [tree]
		});

		this.callParent(arguments);
	}
});