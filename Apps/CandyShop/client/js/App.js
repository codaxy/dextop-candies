﻿Ext.define('Desktop.App', {
	extend: 'Ext.ux.desktop.App',

	init: function () {
		this.callParent();
		// now ready...
	},

	getModules: function () {
		return [{
			id: 'candy-shop',
			text: 'Candy Shop',
			iconCls: 'candy-shop',
			createWindow: function () {
				Dextop.getSession().showCandyShop();
				return undefined;
			}
		}];
	},

	createWindowModules: function (modules) {
		for (var i = 0; i < modules.length; i++)
			modules[i] = Ext.create('Desktop.WindowModule', modules[i]);
		return modules;
	},

	getDesktopConfig: function () {
		var me = this, ret = me.callParent();

		return Ext.apply(ret, {
			//cls: 'ux-desktop-black',

			contextMenuItems: [
			//{ text: 'Change Settings', handler: me.onSettings, scope: me }
            ],

			shortcuts: Ext.create('Ext.data.Store', {
				model: 'Ext.ux.desktop.ShortcutModel',
				data: [
					{ name: 'Candy Shop', iconCls: 'candy-shortcut', module: 'candy-shop' }
                ]
			}),

			wallpaper: '/client/lib/ext/examples/desktop/wallpapers/Blue-Sencha.jpg',
			wallpaperStretch: false
		});
	},

	// config for the start menu
	getStartConfig: function () {
		var me = this, ret = me.callParent();

		return Ext.apply(ret, {
			title: 'User',
			iconCls: 'user',
			height: 400,
			toolConfig: {
				width: 100,
				items: [
				//				    {
				//				    	text: 'Settings',
				//				    	iconCls: 'settings',
				//				    	handler: me.onSettings,
				//				    	scope: me
				//				    },
				//				    '-',
                    {
                    text: 'Logout',
                    iconCls: 'logout',
                    handler: me.onLogout,
                    scope: me
                   }
                ]
			}
		});
	},

	getTaskbarConfig: function () {
		var ret = this.callParent();

		return Ext.apply(ret, {
			quickStart: [
			//{ name: 'Settings', iconCls: 'settings', module: 'settings' },
				{name: 'Candy Shop', iconCls: 'candy-shop', module: 'candy-shop' }
			],
			trayItems: [
                { xtype: 'trayclock', flex: 1 }
            ]
		});
	},

	onLogout: function () {
		Ext.Msg.confirm('Logout', 'Are you sure you want to logout?');
	},

	onSettings: function () {
		//alert('todo');
	}
});
