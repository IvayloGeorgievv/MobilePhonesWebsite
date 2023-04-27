const form = document.getElementById('form');
const username = document.getElementById('username');
const email = document.getElementById('email');
const password = document.getElementById('password');
const password2 = document.getElementById('password2');

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
        || document.getElementById('isPassword2Valid').value == "error") {
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
        setError(username, '�������� ������������� ���');
        document.getElementById('isUsernameValid').value = "error";
    } else if (usernameValue.length < 4) {
        setError(username, '��������������� ��� ������ �� � ���� 4 �������');
        document.getElementById('isUsernameValid').value = "error";
    } else {
        setSuccess(username);
        document.getElementById('isUsernameValid').value = "successfull";
    }

    if (emailValue === '') {
        setError(email, '�������� email');
        document.getElementById('isEmailValid').value = "error";
    } else if (!isValidEmail(emailValue)) {
        setError(email, '�������� ������� email �����');
        document.getElementById('isEmailValid').value = "error";
    } else {
        setSuccess(email);
        document.getElementById('isEmailValid').value = "successfull";
    }

    if (passwordValue === '') {
        setError(password, '�������� ������');
        document.getElementById('isPasswordValid').value = "error";
    } else if (passwordValue.length < 8) {
        setError(password, '�������� ������ �� � ���� 8 �������.')
        document.getElementById('isPasswordValid').value = "error";
    }
    else {
        setSuccess(password);
        document.getElementById('isPasswordValid').value = "successfull";
    }

    if (password2Value === '') {
        setError(password2, '�������� ������');
        document.getElementById('isPassword2Valid').value = "error";
    }
    else if (passwordValue != password2Value) {
        setError(password2, '�������� �� � ��������� ��������.')
        document.getElementById('isPassword2Valid').value = "error";
    }
    else {
        setSuccess(password2);
        document.getElementById('isPassword2Valid').value = "successfull";
    }
};