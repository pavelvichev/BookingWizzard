$('#text').on('input', function () {
   
    var privilegesInput = $(this);
    var privilegesValue = privilegesInput.val();
    var privilegesError = $('#privileges-error');

    // Регулярное выражение для проверки формата
    var regex = /(?=.*[А-Яа-яA-Za-z])(?=.*[0-9;])[А-Яа-яA-Za-z0-9;]+$/;

    if (!regex.test(privilegesValue)) {
        privilegesError.text(errorValidation);
    } else {
        privilegesError.text('');
    }
});