var addresses = [];

// Перебор каждой модели HotelVM и выполнение геокодирования для получения координат
@foreach(var hotel in Model)
{
    <text>
        var address = '@hotel.Address';

        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({address: address }, function(results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
          var location = results[0].geometry.location;
        var latitude = location.lat();
        var longitude = location.lng();

        var addressData = {
            address: address,
        lat: latitude,
        lng: longitude
          };

        addresses.push(addressData);
        } else {
            console.error('Геокодирование не удалось для адреса: ' + address);
        }
      });
    </text>
}

function findNearestAddress() {
    var addressInput = document.getElementById('addressInput');
    var nearestAddressElement = document.getElementById('nearestAddress');
    var inputAddress = addressInput.value;

    // Создание экземпляра объекта геокодера
    var geocoder = new google.maps.Geocoder();

    // Геокодирование введенного адреса
    geocoder.geocode({ address: inputAddress }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            var inputLocation = results[0].geometry.location;

            var nearestAddress = findNearestLocation(inputLocation, addresses);
            nearestAddressElement.innerHTML = 'Ближайший адрес: ' + nearestAddress.address;
        } else {
            alert('Ошибка геокодирования: ' + status);
        }
    });
}

function findNearestLocation(inputLocation, databaseAddresses) {
    var nearestDistance = Number.MAX_VALUE;
    var nearestAddress = null;

    for (var i = 0; i < databaseAddresses.length; i++) {
        var databaseAddress = databaseAddresses[i];
        var databaseLocation = new google.maps.LatLng(databaseAddress.lat, databaseAddress.lng);
        var distance = google.maps.geometry.spherical.computeDistanceBetween(inputLocation, databaseLocation);

        if (distance < nearestDistance) {
            nearestDistance = distance;
            nearestAddress = databaseAddress;
        }
    }

    return nearestAddress;
}