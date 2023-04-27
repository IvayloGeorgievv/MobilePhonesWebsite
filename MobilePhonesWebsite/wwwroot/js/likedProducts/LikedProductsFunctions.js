function addToLikedMobilePhone(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToLikedMobilePhone",
        data: { Id: Id },
        success: function (data) {
            $('#MessageLiked').text('Продуктът е добавен!');
            updateCartIcon();
        },
        error: function (xhr, status, error) {
            $('#MessageLiked').text('Извинете, възникна проблем и вашият продукт не е добавен към харесани.');
        }
    });
}

function addToLikedWiredHeadphones(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToLikedWiredHeadphones",
        data: { Id: Id },
        success: function (data) {
            $('#MessageLiked').text('Продуктът е добавен!');
            updateCartIcon();
        },
        error: function (xhr, status, error) {
            $('#MessageLiked').text('Извинете, възникна проблем и вашият продукт не е добавен към харесани.');
        }
    });
}

function addToLikedWirelessHeadphones(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToLikedWirelessHeadphones",
        data: { Id: Id },
        success: function (data) {
            $('#MessageLiked').text('Продуктът е добавен!');
            updateCartIcon();
        },
        error: function (xhr, status, error) {
            $('#MessageLiked').text('Извинете, възникна проблем и вашият продукт не е добавен към харесани.');
        }
    });
}

function addToLikedSmartwatch(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToLikedSmartwatch",
        data: { Id: Id },
        success: function (data) {
            $('#MessageLiked').text('Продуктът е добавен!');
            updateCartIcon();
        },
        error: function (xhr, status, error) {
            $('#MessageLiked').text('Извинете, възникна проблем и вашият продукт не е добавен към харесани.');
        }
    });
}

function addToLikedPhoneCase(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToLikedPhoneCase",
        data: { Id: Id },
        success: function (data) {
            $('#MessageLiked').text('Продуктът е добавен!');
            updateCartIcon();
        },
        error: function (xhr, status, error) {
            $('#MessageLiked').text('Извинете, възникна проблем и вашият продукт не е добавен към харесани.');
        }
    });
}

function addToLikedPhoneProtector(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/AddToLikedPhoneProtector",
        data: { Id: Id },
        success: function (data) {
            $('#MessageLiked').text('Продуктът е добавен!');
            updateCartIcon();
        },
        error: function (xhr, status, error) {
            $('#MessageLiked').text('Извинете, възникна проблем и вашият продукт не е добавен към харесани.');
        }
    });
}

function confirmRemoveFromLiked(id) {
    if (confirm('Желаете ли да продължите с премахването на продукта?')) {
        removeFromLiked(id);
    }
}

function removeFromLiked(Id) {
    $.ajax({
        type: "POST",
        url: "/Products/RemoveFromLiked",
        data: { Id: Id },
        success: function (data) {
            location.reload();
            $('#cartMessage').text('Продуктът е премахнат!');
            updateCartIcon();
            // Update cart summary on the page
            $('#cartSummary').html(data);
        },
        error: function (xhr, status, error) {
            $('#cartMessage').text('Извинете, възникна грешка и вашият продукт не е премахнат от харесани.');
        }
    });
}