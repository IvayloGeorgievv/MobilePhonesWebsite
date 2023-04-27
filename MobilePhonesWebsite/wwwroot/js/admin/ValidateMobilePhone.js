const Form = document.getElementById('Form');
const Brand = document.getElementById('Brand');
const Model = document.getElementById('Model');
const BatteryLife = document.getElementById('BatteryLife');
const Weight = document.getElementById('Weight');
const Height = document.getElementById('Height');
const Width = document.getElementById('Width');
const Thickness = document.getElementById('Thickness');
const DisplaySize = document.getElementById('DisplaySize');
const DisplayTechnology = document.getElementById('DisplayTechnology');
const DisplayResolution = document.getElementById('DisplayResolution');
const ProcessorFrequency = document.getElementById('ProcessorFrequency');
const MainRearCamera = document.getElementById('MainRearCamera');
const RearCamera = document.getElementById('RearCamera');
const FrontCamera = document.getElementById('FrontCamera');
const OperatingMemory = document.getElementById('OperatingMemory');
const StorageSpace = document.getElementById('StorageSpace');
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
        || document.getElementById('isPhoneWeightValid').value == "error"
        || document.getElementById('isPhoneHeightValid').value == "error"
        || document.getElementById('isPhoneWidthValid').value == "error"
        || document.getElementById('isPhoneThicknessValid').value == "error"
        || document.getElementById('isDisplaySizeValid').value == "error"
        || document.getElementById('isDisplayTechnologyValid').value == "error"
        || document.getElementById('isDisplayResolutionValid').value == "error"
        || document.getElementById('isProcessorFrequencyValid').value == "error"
        || document.getElementById('isMainRearCameraValid').value == "error"
        || document.getElementById('isRearCameraValid').value == "error"
        || document.getElementById('isFrontCameraValid').value == "error"
        || document.getElementById('isOperatingMemoryValid').value == "error"
        || document.getElementById('isStorageSpaceValid').value == "error"
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
    const HeightValue = Height.value;
    const WidthValue = Width.value;
    const ThicknessValue = Thickness.value;
    const DisplaySizeValue = DisplaySize.value;
    const DisplayTechnologyValue = DisplayTechnology.value;
    const DisplayResolutionValue = DisplayResolution.value;
    const ProcessorFrequencyValue = ProcessorFrequency.value;
    const MainRearCameraValue = MainRearCamera.value;
    const RearCameraValue = RearCamera.value;
    const FrontCameraValue = FrontCamera.value;
    const OperatingMemoryValue = OperatingMemory.value;
    const StorageSpaceValue = StorageSpace.value;
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
        setSuccess(Email);
        document.getElementById('isModelValid').value = "successfull";
    }

    if (BatteryLifeValue === '') {
        setError(BatteryLife, 'Enter battery');
        document.getElementById('isBatteryLifeValid').value = "error";
    } else {
        setSuccess(BatteryLife);
        document.getElementById('isBatteryLifeValid').value = "successfull";
    }

    if (WeightValue === '') {
        setError(Weight, 'Enter weight');
        document.getElementById('isPhoneWeightValid').value = "error";
    } else {
        setSuccess(Weight);
        document.getElementById('isPhoneWeightValid').value = "successfull";
    }

    if (HeightValue === '') {
        setError(Height, 'Enter height');
        document.getElementById('isPhoneHeightValid').value = "error";
    } else {
        setSuccess(Height);
        document.getElementById('isPhoneHeightValid').value = "successfull";
    }

    if (WidthValue === '') {
        setError(Width, 'Enter width');
        document.getElementById('isPhoneWidthValid').value = "error";
    } else {
        setSuccess(Width);
        document.getElementById('isPhoneWidthValid').value = "successfull";
    }

    if (ThicknessValue === '') {
        setError(Thickness, 'Enter thickness');
        document.getElementById('isPhoneThicknessValid').value = "error";
    } else {
        setSuccess(Thickness);
        document.getElementById('isPhoneThicknessValid').value = "successfull";
    }

    if (DisplaySizeValue === '') {
        setError(DisplaySize, 'Enter display size');
        document.getElementById('isDisplaySizeValid').value = "error";
    } else {
        setSuccess(DisplaySize);
        document.getElementById('isDisplaySizeValid').value = "successfull";
    }

    if (DisplayTechnologyValue === '') {
        setError(DisplayTechnology, 'Enter display technology');
        document.getElementById('isDisplayTechnologyValid').value = "error";
    } else {
        setSuccess(DisplayTechnology);
        document.getElementById('isDisplayTechnologyValid').value = "successfull";
    }

    if (DisplayResolutionValue === '') {
        setError(DisplayResolution, 'Enter display resolution');
        document.getElementById('isDisplayResolutionValid').value = "error";
    } else {
        setSuccess(DisplayResolution);
        document.getElementById('isDisplayResolutionValid').value = "successfull";
    }

    if (ProcessorFrequencyValue === '') {
        setError(ProcessorFrequency, 'Enter processor frequency');
        document.getElementById('isProcessorFrequencyValid').value = "error";
    } else {
        setSuccess(ProcessorFrequency);
        document.getElementById('isProcessorFrequencyValid').value = "successfull";
    }

    if (MainRearCameraValue === '') {
        setError(MainRearCamera, 'Enter main rear camera');
        document.getElementById('isMainRearCameraValid').value = "error";
    } else {
        setSuccess(MainRearCamera);
        document.getElementById('isMainRearCameraValid').value = "successfull";
    }

    if (RearCameraValue === '') {
        setError(RearCamera, 'Enter rear camera');
        document.getElementById('isRearCameraValid').value = "error";
    } else {
        setSuccess(RearCamera);
        document.getElementById('isRearCameraValid').value = "successfull";
    }

    if (FrontCameraValue === '') {
        setError(FrontCamera, 'Enter display resolution');
        document.getElementById('isFrontCameraValid').value = "error";
    } else {
        setSuccess(FrontCamera);
        document.getElementById('isFrontCameraValid').value = "successfull";
    }

    if (OperatingMemoryValue === '') {
        setError(OperatingMemory, 'Enter operating memory');
        document.getElementById('isOperatingMemoryValid').value = "error";
    } else {
        setSuccess(OperatingMemory);
        document.getElementById('isOperatingMemoryValid').value = "successfull";
    }

    if (StorageSpaceValue === '') {
        setError(StorageSpace, 'Enter storage space');
        document.getElementById('isStorageSpaceValid').value = "error";
    } else {
        setSuccess(StorageSpace);
        document.getElementById('isStorageSpaceValid').value = "successfull";
    }

    if (PriceValue === '') {
        setError(Price, 'Enter price');
        document.getElementById('isPriceValid').value = "error";
    } else {
        setSuccess(Price);
        document.getElementById('isPriceValid').value = "successfull";
    }
};