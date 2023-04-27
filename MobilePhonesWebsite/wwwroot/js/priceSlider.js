const rangeInput = document.querySelectorAll(".price-range-input input"),
    priceInput = document.querySelectorAll(".price-input input"),
    range = document.querySelector(".price-slider .price-progress");
const GetPriceGap = document.getElementById('priceGap');
let priceGap = GetPriceGap.value;

priceInput.forEach(input => {
    input.addEventListener("price-input", e => {
        let minPrice = parseInt(priceInput[0].value),
            maxPrice = parseInt(priceInput[1].value);

        if ((maxPrice - minPrice >= priceGap) && maxPrice <= rangeInput[1].max) {
            if (e.target.className === "price-input-min") {
                rangeInput[0].value = minPrice;
                range.style.left = ((minPrice / rangeInput[0].max) * 100) + "%";
            } else {
                rangeInput[1].value = maxPrice;
                range.style.right = 100 - (maxPrice / rangeInput[1].max) * 100 + "%";
            }
        }
    });
});

rangeInput.forEach(input => {
    input.addEventListener("input", e => {
        let minVal = parseInt(rangeInput[0].value),
            maxVal = parseInt(rangeInput[1].value);

        if ((maxVal - minVal) < priceGap) {
            if (e.target.className === "price-range-min") {
                rangeInput[0].value = maxVal - priceGap
            } else {
                rangeInput[1].value = minVal + priceGap;
            }
        } else {
            priceInput[0].value = minVal;
            priceInput[1].value = maxVal;
            range.style.left = ((minVal / rangeInput[0].max) * 100) + "%";
            range.style.right = 100 - (maxVal / rangeInput[1].max) * 100 + "%";
        }
    });
});