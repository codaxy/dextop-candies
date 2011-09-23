Ext.define('CandyShop.GMapPicker', {
	extend: 'Ext.window.Window',

	width: 500,
	height: 500,
	title: 'Map',

	initComponent: function () {
		this.addEvents(['markermoved']);

		this.position = this.position || {
			lat: 44.774706715817416,
			lng: 17.18617916071777
		};

		Ext.apply(this, {
			bbar: ['Marker Position: ', this.markerPositionBox = Ext.widget('textfield', {
				width: 300
			})]
		});

		this.callParent(arguments);

		this.updateMarkerPosition(this.position, true);
	},

	afterRender: function () {
		var me = this;

		this.callParent(arguments);

		var latLng = new google.maps.LatLng(this.position.lat, this.position.lng);
		var map = new google.maps.Map(this.body.dom, {
			zoom: 12,
			center: latLng,
			mapTypeId: google.maps.MapTypeId.HYBRID
		});
		var marker = new google.maps.Marker({
			position: latLng,
			title: 'Point A',
			map: map,
			draggable: true
		});

		google.maps.event.addListener(marker, 'dragend', function () {
			var p = marker.getPosition();
			Ext.Function.defer(me.updateMarkerPosition, 10, me, [{
				lng: p.lng(),
				lat: p.lat()
			}]);
		});
	},

	updateMarkerPosition: function (pos, silent) {
		this.position = pos;
		if (!silent)
			this.fireEvent('markermoved', this, pos);
		this.markerPositionBox.setValue(pos.lng + ', ' + pos.lat);
	}
});