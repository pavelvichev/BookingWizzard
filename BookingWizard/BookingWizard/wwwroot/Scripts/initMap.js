function initMap() {
	const map = new google.maps.Map(document.getElementById("map"), {
		center: { lat: -33.8688, lng: 151.2195 },
		zoom: 13,
	});
	

	var addressData = document.getElementById("map");
	var address = addressData.getAttribute("data-map");
	

	const geocoder = new google.maps.Geocoder();
	geocoder.geocode({ address }, (results, status) => {
		if (status === google.maps.GeocoderStatus.OK) {
			const place = results[0];

			map.setCenter(place.geometry.location);

			const marker = new google.maps.Marker({
				map: map,
				position: place.geometry.location,
			});

			const infowindow = new google.maps.InfoWindow({
				content: `<div><strong>${place.formatted_address}</strong></div>`,
			});

			marker.addListener("click", () => {
				infowindow.open(map, marker);
			});
		} else {
			alert("Geocode was not successful for the following reason: " + status);
		}
	});
}

window.initMap = initMap;
