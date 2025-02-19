document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("myForm");
    form.addEventListener("submit", function (event) {
        event.preventDefault();
        clearErrors();
        let isValid = true;
        const surname = document.getElementById("surname").value.trim();
        const name = document.getElementById("name").value.trim();
        const email = document.getElementById("email").value.trim();
        const phone = document.getElementById("phone").value.trim();
        const city = document.getElementById("city").value;
        const bstuChecked = document.getElementById("bstu").checked;
        const courseValue = document.querySelector('input[name="course"]:checked');
        const about = document.getElementById("about").value.trim();
        if (!surname) {
            showError("surname-error", "Поле не должно быть пустым!");
            isValid = false;
        } else if (!/^[a-zA-Zа-яА-ЯёЁ]{1,20}$/.test(surname)) {
            showError("surname-error", "Допускаются только буквы (не более 20)!");
            isValid = false;
        }
        if (!name) {
            showError("name-error", "Поле не должно быть пустым!");
            isValid = false;
        } else if (!/^[a-zA-Zа-яА-ЯёЁ]{1,20}$/.test(name)) {
            showError("name-error", "Допускаются только буквы (не более 20)!");
            isValid = false;
        }
        if (!email) {
            showError("email-error", "Поле не должно быть пустым!");
            isValid = false;
        } else if (!/^[^\s]+@[A-Za-z]{2,5}\.[A-Za-z]{2,3}$/.test(email)) {
            showError("email-error", "Неверный формат e-mail (пример: name@abcde.com)!");
            isValid = false;
        }
        if (!phone) {
            showError("phone-error", "Поле не должно быть пустым!");
            isValid = false;
        } else if (!/^\(0\d{2}\)\d{3}-\d{2}-\d{2}$/.test(phone)) {
            showError("phone-error", "Формат телефона: (0xx)xxx-xx-xx");
            isValid = false;
        }
        if (!city) {
            showError("city-error", "Выберите город!");
            isValid = false;
        }
        if (!courseValue) {
            showError("course-error", "Выберите курс!");
            isValid = false;
        }
        if (!about) {
            showError("about-error", "Поле не должно быть пустым!");
            isValid = false;
        } else if (about.length > 250) {
            showError("about-error", "Превышен лимит в 250 символов!");
            isValid = false;
        }
        if (isValid) {
            if (city !== "Минск" || courseValue.value !== "3" || !bstuChecked) {
                if (!confirm("Вы уверены в своем выборе?")) {
                    return;
                }
            }
            form.submit();
        }
    });
    function clearErrors() {
        const errorSpans = document.querySelectorAll(".error-message");
        errorSpans.forEach((span) => {
            span.textContent = "";
        });
    }
    function showError(elementId, message) {
        const errorSpan = document.getElementById(elementId);
        if (errorSpan) {
            errorSpan.textContent = message;
        }
    }
});