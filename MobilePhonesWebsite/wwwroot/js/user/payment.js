
function getTotalPrice(shippingMethod) {
    $.ajax({
        type: "GET",
        url: "/Products/PlaceOrder",
        data: {
            shippingMethod: shippingMethod
        },
        success: function () {
            // Reload the page on success
            location.reload();
        },
        error: function (xhr, status, error) {
            // handle error
        }
    });
}

function changePaymentMethod(AddOrderVM) {
    $.ajax({
        type: "GET",
        url: "/Products/PlaceOrder",
        data: {
            AddOrderVM: AddOrderVM
        },
        success: function () {
            location.reload();
        },
        error: function (xhr, status, error) {
            // handle error
        }
    });
}