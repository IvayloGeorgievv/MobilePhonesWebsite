const Form = document.getElementById('Form');
const Username = document.getElementById('Username');
const Email = document.getElementById('Email');
const Password = document.getElementById('Password');


Form.addEventListener('submit', e => {
    validateInputs();

    if (isFormForPrevent()) {
        e.preventDefault();
    }
});

function isFormForPrevent() {

    if (document.getElementById('isUsernameValid').value == "error"
        || document.getElementById('isEmailValid').value == "error"
        || document.getElementById('isPasswordValid').value == "error"

    ) {
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
    const UsernameValue = Username.value;
    const EmailValue = Email.value;
    const PasswordValue = Password.value;



    if (UsernameValue === '') {
        setError(Username, 'Enter username');
        document.getElementById('isUsernameValid').value = "error";
    } else {
        setSuccess(Username);
        document.getElementById('isUsernameValid').value = "successfull";
    }

    if (EmailValue === '') {
        setError(Email, 'Enter email');
        document.getElementById('isEmailValid').value = "error";
    } else if (!isValidEmail(EmailValue)) {
        setError(Email, 'Enter valid email');
        document.getElementById('isEmailValid').value = "error";
    } else {
        setSuccess(Email);
        document.getElementById('isEmailValid').value = "successfull";
    }

    if (PasswordValue === '') {
        setError(Password, 'Enter password');
        document.getElementById('isPasswordValid').value = "error";
    } else {
        setSuccess(Password);
        document.getElementById('isPasswordValid').value = "successfull";
    }
};