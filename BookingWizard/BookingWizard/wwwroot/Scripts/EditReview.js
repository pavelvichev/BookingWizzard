var reviewId;
var reviewDate;
var reviewText;
    function init() {

        const buttonsContainerList = document.querySelectorAll('#review');

        // Вешаем обработчик на контейнер
        buttonsContainerList.forEach((container) => container.addEventListener('click', (event) => {

            // Проверяем, что клик был на кнопке
            if (event.target.classList.contains('edit-review-btn')) {

                const editButton = event.target;
                const reviewContainer = container; // Сохраняем ссылку на контейнер

                editButton.addEventListener('click', () => {

                    reviewId = editButton.getAttribute('data-id');
                    reviewDate = editButton.getAttribute('data-date');
                    reviewText = editButton.getAttribute('data-text');


                    const formHtml = `
          <form method="post" id="editReviewForm" action="/Reviews/EditReview">
            <div class="form-group">
              <label for="text">Текст отзыва</label>
              <textarea name="Text" placeholder="${reviewText}" class="form-control" id="text" rows="5" required></textarea>
            </div>

            <div class="form-group">
              <label for="rating">Рейтинг (1-5)</label>
              <select class="form-control"  name="Rating" id="rating" required>
                <option>1</option>
                <option>2</option>
                <option>3</option>
                <option>4</option>
                <option>5</option>
              </select>
            </div>

            <button type="button" id="submitBtn" class="btn btn-primary">Отправить</button>
          </form>
        `;


                    reviewContainer.innerHTML = formHtml; 


                    const submitButton = reviewContainer.querySelector('#submitBtn'); 

                    submitButton.addEventListener('click', () => {
                        sendReviewData();
                    });
                });
            }
        }));
    }
init();




function sendReviewData() {
	const form = $('#editReviewForm');

	const textarea = form.find('textarea#text.form-control');
	var text = textarea.val();

	if (text === '') {
		text = reviewText;
		textarea.val(text); 
	} 

	const formData = new FormData(form[0]);
	
	formData.append('Id', reviewId);
	formData.append('HotelId', modelId);
	formData.append('IdentityUserId', IdentityUserId);
	formData.append('ReviewDate', reviewDate);

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
}