// Modal for Edit
$(".editItemBtn").click(function () {
    $("#EditId").val($(this).data("id"));
    $("#EditCode").val($(this).data("code"));
    $("#EditName").val($(this).data("name"));
    $("#EditPrice").val($(this).data("price"));
});