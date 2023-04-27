const Form = document.getElementById('Form');
const Brand = document.getElementById('Brand');
const FitFor = document.getElementById('FitFor');
const Price = document.getElementById('Price');

Form.addEventListener('submit', e => {
    validateInputs();

    if (isFormForPrevent()) {
        e.preventDefault();
    }
});

function isFormForPrevent() {

    if (document.getElementById('isBrandValid').value == "error"
        || document.getElementById('isPriceValid').value == "error"
        || document.getElementById('isFitForValid').value == "error"
    ) {
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
    const BrandValue = Brand.value;
    const FitForValue = FitFor.value;
    const PriceValue = Price.value

    if (BrandValue === '') {
        setError(Brand, 'Enter brand');
        document.getElementById('isBrandValid').value = "error";
    } else {
        setSuccess(Brand);
        document.getElementById('isBrandValid').value = "successfull";
    }

    if (FitForValue === '') {
        setError(FitFor, 'Enter for which phone is this protector');
        document.getElementById('isPriceValid').value = "error";
    } else {
        setSuccess(FitFor);
        document.getElementById('isPriceValid').value = "successfull";
    }

    if (PriceValue === '') {
        setError(Price, 'Enter price');
        document.getElementById('isFitForValid').value = "error";
    } else {
        setSuccess(Price);
        document.getElementById('isFitForValid').value = "successfull";
    }
};