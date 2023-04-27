const form = document.getElementById('form');
const username = document.getElementById('username');
const email = document.getElementById('email');
const password = document.getElementById('password');
const password2 = document.getElementById('repeatPassword');

form.addEventListener('submit', e => {
    validateInputs();

    if (isFormForPrevent()) {
        e.preventDefault();
    }
});

function isFormForPrevent() {

    if (document.getElementById('isUsernameValid').value == "error"
        || document.getElementById('isEmailValid').value == "error"
        || document.getElementById('isPasswordValid').value == "error"
        || document.getElementById('isRepeatPasswordValid').value == "error")
    {
        return true;
    } else {
        return false;
    }
}

const isValidEmail = email => {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
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
    const usernameValue = username.value;
    const emailValue = email.value;
    const passwordValue = password.value;
    const password2Value = password2.value;

    if (usernameValue === '') {
        setError(username, 'Въведете име');
        document.getElementById('isUsernameValid').value = "error";
    }else {
        setSuccess(username);
        document.getElementById('isUsernameValid').value = "successfull";
    }

    if (emailValue === '') {
        setError(email, 'Въведете email');
        document.getElementById('isEmailValid').value = "error";
    } else if (!isValidEmail(emailValue)) {
        setError(email, 'Въведете валиден email адрес');
        document.getElementById('isEmailValid').value = "error";
    } else {
        setSuccess(email);
        document.getElementById('isEmailValid').value = "successfull";
    }

    if (passwordValue === '') {
        setError(password, 'Въведете парола');
        document.getElementById('isPasswordValid').value = "error";
    } else if (passwordValue.length < 8) {
        setError(password, 'Паролата трябва да е поне 8 символа.')
        document.getElementById('isPasswordValid').value = "error";
    }
     else {
        setSuccess(password);
        document.getElementById('isPasswordValid').value = "successfull";
    }

    if (password2Value === '') {
        setError(password2, 'Въведете парола');
        document.getElementById('isRepeatPasswordValid').value = "error";
    }
    else if (passwordValue != password2Value) {
        setError(password2, 'Паролата не е повторена правилно.')
        document.getElementById('isRepeatPasswordValid').value = "error";
    }
    else {
        setSuccess(password2);
        document.getElementById('isRepeatPasswordValid').value = "successfull";
    }
};