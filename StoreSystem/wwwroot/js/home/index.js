var itemTable;

$(document).ready(function () {
    initChart();
    loadDashboard();
    loadChart();
});

function loadDashboard() {

    // Load DataTable
    if ($.fn.DataTable.isDataTable('#homeTable')) {
        itemTable.destroy();
        $('#homeTable tbody').empty();
    }

    itemTable = $('#homeTable').DataTable({
        processing: true,
        serverSide: false,
        filter: false,
        orderMulti: false,
        autoWidth: false,
        ordering: true,
        scrollX: true,
        pageLength: 5,
        lengthMenu: [[5, 15, 30, 60, 100], [5, 15, 30, 60, 100]],
        ajax: {
            url: '/Home/LoadDashBoard',
            //url: '/StoreSystem/Home/LoadDashBoard',
            type: 'GET',
            dataSrc: function (json) {
                // Dashboard cards
                $('#totalItems').text(json.totalItems);
                $('#totalPurchased').text(json.totalPurchased);
                $('#totalSold').text(json.totalSold);
                $('#currentStock').text(json.currentStock);

                return json.data;
            }
        },
        columns: [
            { data: 'itemCode', title: 'Item Code' },
            { data: 'itemName', title: 'Item Name' }
        ],
        order: [[0, 'asc']]
    });
}

function loadChart() {

    $.ajax({
        url: '/Home/GetItemSalesChart',
        //url: '/StoreSystem/Home/GetItemSalesChart',
        type: 'GET',
        success: function (data) {

            salesGraph.data.labels =
                data.map(x => x.itemName);

            salesGraph.data.datasets[0].data =
                data.map(x => x.purchased);

            salesGraph.data.datasets[1].data =
                data.map(x => x.sold);

            salesGraph.update();
        }
    });

}
