// Get the selling price
$("#ItemId").on("change", function () {

    var itemId = $(this).val();

    if (itemId) {
        $.get('/Sell/GetItemPrice',
            { itemId: itemId },
            function (price) {
                $("#sellingPrice").val(price);
            });
    } else {
        $("#sellingPrice").val('');
    }
});

// Get the quantity
$("#ItemId").on("change", function () {

    var itemId = $(this).val();

    if (itemId) {
        $.get('/Sell/GetQuantity',
            { itemId: itemId },
            function (quantity) {
                $("#countQuantity").val(quantity);
                // Save original quantity
                $("#countQuantity").data("original", quantity);
            });
    } else {
        $("#countQuantity").val('');
        $("#countQuantity").removeData("original");
    }
});

// Auto deduct in quantity
$('#quantity').on('input', function () {

    let originalQty = parseInt($('#countQuantity').data('original')) || 0;
    let sellQty = parseInt($(this).val()) || 0;

    if ($(this).val() === '') {
        $('#countQuantity').val(originalQty);
        return;
    }

    let remaining = originalQty - sellQty;

    if (remaining < 0) {
        remaining = 0;

        // Prevent selling more than stock
        $(this).val(originalQty);
    }

    $('#countQuantity').val(remaining);
});