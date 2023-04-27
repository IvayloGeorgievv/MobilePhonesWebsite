const Form = document.getElementById('Form');
const VariantsNum = document.getElementById('VariantsNum');


Form.addEventListener('submit', e => {
    validateInputs();

    if (isFormForPrevent()) {
        e.preventDefault();
    }
});

function isFormForPrevent() {

    if (document.getElementById('isVariantsNumValid').value == "error"

    )
    {
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
    const VariantsNumValue = VariantsNum.value;

    if (VariantsNumValue === '') {
        setError(VariantsNum, 'Enter variant');
        document.getElementById('isVariantsNumValid').value = "error";
    }
    else if (VariantsNumValue > 4 || VariantsNumValue < 1) {
        setError(VariantsNum, 'Varians must be between 1 and 4');
        document.getElementById('isVariantsNumValid').value = "error";
    } else {
        setSuccess(VariantsNum);
        document.getElementById('isVariantsNumValid').value = "successfull";
    }
};