Ext.define('CandyShop.ExportGridWindow', {
	extend: 'Dextop.Window',
	width: 500,
	height: 300,

	title: 'Export Grid To Excel',
	iconCls: 'icon-grid',

	initComponent: function () {

		var grid = Ext.create('Dextop.ux.SwissArmyGrid', {
			remote: this.remote,
			paging: true,
			border: false,
			editing: 'row',
			tbar: ['add', 'edit', 'remove', {
				text: 'Export to Excel',
				handler: function () {
					window.open(this.remote.getAjaxUrl({
						type: 'xlsx'
					}));
				},
				scope: this
			}],
			storeOptions: {
				pageSize: 10,
				autoLoad: true,
				autoSync: true
			}
		});

		Ext.apply(this, {
			layout: 'fit',
			items: grid
		});

		this.callParent(arguments);
	}
});