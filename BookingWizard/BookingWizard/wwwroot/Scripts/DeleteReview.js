var reviewId;

function init() {

    const buttonsContainerList = document.querySelectorAll('#review');

    // Вешаем обработчик на контейнер
    buttonsContainerList.forEach((container) => container.addEventListener('click', (event) => {

        // Проверяем, что клик был на кнопке
        if (event.target.classList.contains('edit-review-btn')) {

            const deleteButton = event.target;
            const reviewContainer = container; // Сохраняем ссылку на контейнер

            deleteButton.addEventListener('click', () => {

                reviewId = editButton.getAttribute('data-id');
             
                deleteReview();

                submitButton.addEventListener('click', () => {
                    sendReviewData();
                });
            });
        }
    }));
}
init();



function deleteReview() {
	const formData = new FormData();

	formData.append('id', reviewId);


	$.ajax({
		url: '/Reviews/EditReview',
		type: 'POST',
		data: formData,
		processData: false,
		contentType: false,
		success: function (response) {
			window.location.href = "/Hotels/Hotel/" + modelId;
		},
		error: function (xhr, status, error) {
		}
	});