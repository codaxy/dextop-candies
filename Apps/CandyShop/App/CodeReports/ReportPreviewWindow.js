Ext.define('CandyShop.ReportPreviewWindow', {
	extend: 'Dextop.Window',
	width: 400,
	height: 400,

	title: 'Report Preview',
	iconCls: 'icon-grid',

	initComponent: function () {

		Ext.apply(this, {
			bbar: [{
				text: 'Export to Excel',
				scope: this,
				handler: function() {
					window.open(this.remote.getAjaxUrl({ type: 'xlsx' }));
				}
			}, {
				text: 'Export to PDF',
				scope: this,
				handler: function() {
					window.open(this.remote.getAjaxUrl({ type: 'pdf' }));
				}
			}],
			tpl: new Ext.Template('<iframe width="100%" height="100%" frameborder="0" src="{src}" />'),
			data: {
				src: this.remote.getAjaxUrl({ type: 'html' })
			}
		});

		this.callParent(arguments);
	}
});