Ext.define('CandyShop.WelcomeWindow', {
	extend: 'Ext.window.Window',

	title: 'Welcome',

	width: 700,
	height: 300,

	initComponent: function () {

		var store = Ext.create('Ext.data.Store', {
			autoLoad: true,
			fields: ['title', 'type', 'iconCls'],
			reader: {
				type: 'json'
			},
			proxy: {
				type: 'memory',
				data: [{
					title: 'Manage Domains',
					type: 'gmap-picker',
					iconCls: 'cloud64'
				}, {
					title: 'Manage Users',
					type: 'xlio-grid-export',
					iconCls: 'people64'
				}, {
					title: 'Manage Applications',
					type: 'simple-html-editor',
					iconCls: 'apps64'
				}, {
					title: 'View Reports',
					type: 'random-report-selector',
					iconCls: 'reports64'
				}]
			}
		});

		Ext.apply(this, {
			border: false,
			layout: 'fit',
			items: [{
				xtype: 'dataview',
				store: store,
				border: false,
				tpl: [
					'<tpl for=".">',
						'<div class="{iconCls} welcome-navigation-pane">',
						'{title}',
						'</div>',
					'</tpl>',
					'<div class="x-clear"></div>'
				],
				multiSelect: true,
				trackOver: true,
				overItemCls: 'x-item-over',
				itemSelector: 'div.welcome-navigation-pane',
				listeners: {
					scope: this,
					'itemclick': function (view, record) {
						Dextop.getSession().requestWindow(record.get('type'));
					}
				}

			}]
		});

		this.callParent(arguments);
	}
});