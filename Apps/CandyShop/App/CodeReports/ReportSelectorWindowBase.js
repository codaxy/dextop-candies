Ext.define('CandyShop.ReportSelectorWindowBase', {
	extend: 'Dextop.Window',
	width: 600,
	height: 400,

	title: 'Select Report',
	iconCls: 'icon-grid',

	initComponent: function () {

		var formItems = this.getFilterFormItems();
		formItems.push({
			xtype: 'button',
			text: 'Preview',
			scale: 'large',
			handler: this.previewReport,
			scope: this,
			formBind: true
		});

		Ext.apply(this, {
			layout: 'border',
			items: [{
				region: 'west',
				width: 200,
				xtype: 'treepanel',
				root: this.reportTree,
				rootVisible: false,
				border: true,
				margins: '-1',
				split: true
			}, {
				region: 'center',
				xtype: 'form',
				border: true,
				margins: '-1',
				bodyStyle: 'padding: 10px',
				items: formItems
			}]
		});

		this.callParent(arguments);
	},

	getFilterFormItems: function () {
		var formItems = Ext.create(this.getNestedTypeName('.form.Filter')).getItems({
			remote: this.remote,
			data: this.data
		});
		return formItems;
	},

	previewReport: function () {
		var tree = this.down('treepanel');
		var node = tree.getSelectionModel().getLastSelected();
		if (!node || !node.isLeaf())
			return;
		var reportId = node.raw.reportId;
		var form = this.down('form');
		if (!form.getForm().isValid())
			return;
		var filter = form.getForm().getFieldValues();
		this.remote.PreviewReport(reportId, filter, {
			type: 'alert',
			scope: this,
			success: function (config) {
				var w = Dextop.create(config);
				w.show();
			}
		});
	}
});