function addToCartMobilePhone(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToCartMobilePhone",
        data: { Id: Id },
        success: function (data) {
            $('#Message').text('Продуктът е добавен!');
        },
        error: function (xhr, status, error) {
            $('#Message').text('Извинете, възникна проблем и вашият продукт не е добавен към количката ви.');
        }
    });
}

function addToCartWiredHeadphones(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToCartWiredHeadphones",
        data: { Id: Id },
        success: function (data) {
            $('#Message').text('Продуктът е добавен!');
        },
        error: function (xhr, status, error) {
            $('#Message').text('Извинете, възникна проблем и вашият продукт не е добавен към количката ви.');
        }
    });
}

function addToCartWirelessHeadphones(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToCartWirelessHeadphones",
        data: { Id: Id },
        success: function (data) {
            $('#Message').text('Продуктът е добавен!');
        },
        error: function (xhr, status, error) {
            $('#Message').text('Извинете, възникна проблем и вашият продукт не е добавен към количката ви.');
        }
    });
}

function addToCartSmartwatch(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToCartSmartwatch",
        data: { Id: Id },
        success: function (data) {
            $('#Message').text('Продуктът е добавен!');
        },
        error: function (xhr, status, error) {
            $('#Message').text('Извинете, възникна проблем и вашият продукт не е добавен към количката ви.');
        }
    });
}

function addToCartPhoneCase(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToCartPhoneCase",
        data: { Id: Id },
        success: function (data) {
            $('#Message').text('Продуктът е добавен!');
        },
        error: function (xhr, status, error) {
            $('#Message').text('Извинете, възникна проблем и вашият продукт не е добавен към количката ви.');
        }
    });
}

function addToCartPhoneProtector(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToCartPhoneProtector",
        data: { Id: Id },
        success: function (data) {
            $('#Message').text('Продуктът е добавен!');
        },
        error: function (xhr, status, error) {
            $('#Message').text('Извинете, възникна проблем и вашият продукт не е добавен към количката ви.');
        }
    });
}

function confirmRemoveFromCart(id) {
    if (confirm('Желаете ли да продължите с премахването на продукта?')) {
        removeFromCart(id);
    }
}

function removeFromCart(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/RemoveFromCart",
        data: { Id: Id },
        success: function (data) {
            location.reload();
            $('#cartMessage').text('Продуктът е премахнат!');
            // Update cart summary on the page
            $('#cartSummary').html(data);
        },
        error: function (xhr, status, error) {
            $('#cartMessage').text('Извинете, възникна грешка и вашият продукт не е премахнат от количката ви.');
        }
    });
}

function updateCartItemQuantity(Id,ProductType ,Quantity) {
    $.ajax({
        type: "POST",
        url: "/Products/UpdateCartItemQuantity",
        data: { productId: Id, ProductType: ProductType, quantity: Quantity },
        success: function (data) {
            location.reload();
        },
        error: function (xhr, status, error) {
            // handle error
        }
    });
}

function getTotalPrice() {
    $.ajax({
        type: "GET",
        url: "/Cart/GetCartTotalPrice",
        success: function (data) {
            // Update cart total price on the page
            $('#cartTotalPrice').text(data);
        },
        error: function (xhr, status, error) {
            // handle error
        }
    });
}

function confirmCancelOrder(id) {
    if (confirm('Наистина ли желаете да откажете поръчката?')) {
        cancelOrder(id);
    }
}

function cancelOrder(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/CancelOrder",
        data: { Id: Id },
        success: function (data) {
            location.reload();
            // Update cart summary on the page
            $('#cartSummary').html(data);
        },
        error: function (xhr, status, error) {
            $('#cartMessage').text('Извинете, възникна грешка и вашата поръчка не е отказана! Моля опитайте след няколко минути.');
        }
    });
}