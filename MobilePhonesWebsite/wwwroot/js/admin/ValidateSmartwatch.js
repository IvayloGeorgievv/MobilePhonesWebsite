const Form = document.getElementById('Form');
const Brand = document.getElementById('Brand');
const Model = document.getElementById('Model');
const BatteryLife = document.getElementById('BatteryLife');
const DisplaySize = document.getElementById('DisplaySize');
const DisplayTehnology = document.getElementById('DisplayTechnology');
const Weight = document.getElementById('Weight');
const Price = document.getElementById('Price');

Form.addEventListener('submit', e => {
    validateInputs();

    if (isFormForPrevent()) {
        e.preventDefault();
    }
});

function isFormForPrevent() {

    if (document.getElementById('isBrandValid').value == "error"
        || document.getElementById('isModelValid').value == "error"
        || document.getElementById('isBatteryLifeValid').value == "error"
        || document.getElementById('isDisplaySizeValid').value == "error"
        || document.getElementById('isDisplayTechnologyValid').value == "error"
        || document.getElementById('isWeightValid').value == "error"
        || document.getElementById('isPriceValid').value == "error"
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
    const ModelValue = Model.value;
    const BatteryLifeValue = BatteryLife.value;
    const WeightValue = Weight.value;
    const DisplaySizeValue = DisplaySize.value;
    const DisplayTechnologyValue = DisplayTehnology.value;
    const PriceValue = Price.value;


    if (BrandValue === '') {
        setError(Brand, 'Enter brand');
        document.getElementById('isBrandValid').value = "error";
    } else {
        setSuccess(Brand);
        document.getElementById('isBrandValid').value = "successfull";
    }

    if (ModelValue === '') {
        setError(Model, 'Enter model');
        document.getElementById('isModelValid').value = "error";
    } else {
        setSuccess(Model);
        document.getElementById('isModelValid').value = "successfull";
    }

    if (BatteryLifeValue === '') {
        setError(BatteryLife, 'Enter battery');
        document.getElementById('isBatteryLifeValid').value = "error";
    } else {
        setSuccess(BatteryLife);
        document.getElementById('isBatteryLifeValid').value = "successfull";
    }

    if (DisplaySizeValue === '') {
        setError(DisplaySize, 'Enter display size');
        document.getElementById('isDisplaySizeValid').value = "error";
    } else {
        setSuccess(DisplaySize);
        document.getElementById('isDisplaySizeValid').value = "successfull";
    }

    if (DisplayTechnologyValue === '') {
        setError(DisplayTehnology, 'Enter display technology');
        document.getElementById('isDisplayTechnologyValid').value = "error";
    } else {
        setSuccess(DisplayTehnology);
        document.getElementById('isDisplayTechnologyValid').value = "successfull";
    }

    if (WeightValue === '') {
        setError(Weight, 'Enter weight');
        document.getElementById('isWeightValid').value = "error";
    } else {
        setSuccess(Weight);
        document.getElementById('isWeightValid').value = "successfull";
    }
    if (PriceValue === '') {
        setError(Price, 'Enter price');
        document.getElementById('isPriceValid').value = "error";
    } else {
        setSuccess(Price);
        document.getElementById('isPriceValid').value = "successfull";
    }
};