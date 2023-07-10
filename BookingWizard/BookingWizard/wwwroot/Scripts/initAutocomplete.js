
function initAutocomplete() {
    const input = document.getElementById("autocomplete");
    const options = {
        fields: ["place_id", "geometry", "name", "formatted_address"],
    };

    let autocomplete = new google.maps.places.Autocomplete(input, options);

    autocomplete.addListener("place_changed", function () {
        const place = autocomplete.getPlace();

        if (place.geometry && place.geometry.location) {
            const lat = parseFloat(place.geometry.location.lat());
            const lng = parseFloat(place.geometry.location.lng());

            document.getElementById("Lat").setAttribute("value", lat);
            document.getElementById("Lng").setAttribute("value", lng);

        }
    });

}



function initialize() {
    initAutocomplete();
    initMap();
}
