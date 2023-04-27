const form = document.getElementById('contactForm');
const username = document.getElementById('username');
const password = document.getElementById('password');

form.addEventListener('submit', e => {
    validateInputs();

    if (isFormForPrevent()) {
        e.preventDefault();
    }
});

function isFormForPrevent() {

    if (document.getElementById('isUsernameValid').value == "error"
        || document.getElementById('isPasswordValid').value == "error") {
        return true;
    } else {
        return false;
    }
}

const setError = (element, message) => {
    const inputControl = element.parentElement;
    const errorDisplay = inputControl.querySelector('.error');

    errorDisplay.innerText = message;
    inputControl.classList.add('error');
    inputControl.classList.remove('success')
}

const setSuccess = element => {
    const inputControl = element.parentElement;
    const errorDisplay = inputControl.querySelector('.error');

    errorDisplay.innerText = '';
    inputControl.classList.add('success');
    inputControl.classList.remove('error');
};

const validateInputs = () => {
    const usernameValue = username.value.trim();
    const passwordValue = password.value.trim();

    if (usernameValue === '') {
        setError(firstName, 'Enter username');
        document.getElementById('isFirstNameValid').value = "error";
    } else if (usernameValue.length < 4) {
        setError(username, 'Потребителското име трябва да е поне 4 символа');
        document.getElementById('isUsernameValid').value = "error";
    } else {
        setSuccess(username);
        document.getElementById('isUsernameValid').value = "successfull";
    }

    if (passwordValue === '') {
        setError(password, 'Password is required');
        document.getElementById('isPasswordValid').value = "error";
    } else if (passwordValue.length < 8) {
        setError(password, 'Password must be at least 8 character.')
        document.getElementById('isPasswordValid').value = "error";
    } else {
        setSuccess(password);
        document.getElementById('isPasswordValid').value = "successfull";
    }
};