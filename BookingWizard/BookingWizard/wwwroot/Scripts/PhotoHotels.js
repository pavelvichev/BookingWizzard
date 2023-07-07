var currentPhotoIndex = 0;

function showPhoto(index) {
	var photoId = photos[index].id;
	var photoUrl = '/PhotoHotels/GetPhoto/' + photoId;
	$("#photoContainer").html('<img src="'+photoUrl+'" class="img-fluid" style="height: 300px;" />');
}

$(document).ready(function () {

	$("#addButton").click(function () {
		$("#photoInput").click();
	});

	$("#nextButton").click(function () {
		currentPhotoIndex = (currentPhotoIndex + 1) % photos.length;
		showPhoto(currentPhotoIndex);
		
	});

	$("#prevButton").click(function () {
		currentPhotoIndex = (currentPhotoIndex - 1 + photos.length) % photos.length;
		showPhoto(currentPhotoIndex);
	});

	$("#photoInput").change(function () {
		var files = this.files;
		

		var formData = new FormData();
		for (var i = 0; i < files.length; i++) {
			formData.append("files", files[i]);
			photos.push(files[i].name);
		}

		formData.append("model", JSON.stringify(model));

		$.ajax({
			url: "/PhotoHotels/PhotoUpload",
			type: "POST",
			data: formData,
			contentType: false,
			processData: false,
			success: function (response) {

				window.location.href = "/Hotels/Hotel?id=" + model.id;

			},
			error: function (xhr, status, error) {
				debugger;
				console.error("Error. Status:", status, "Error:", error);

			}
		});
	});




	$("#deletePhotoButton").click(function () {

		var currentPhoto = photoContainer.querySelector("img");
		var HotelId = model.id;
		if (currentPhoto) {
			var id = currentPhoto.getAttribute("src").split("/").pop();
			
		}

		$.post("/PhotoHotels/DeletePhoto", { id: id, HotelId: HotelId })
			.done(function () {
				// Перенаправление на другой контроллер
				window.location.href = "/Hotels/Hotel/"+ model.id;
			})
			.fail(function (xhr, status, error) {
				// Обработка ошибки
				console.error("Error. Status:", status, "Error:", error);
			});
	});


});


