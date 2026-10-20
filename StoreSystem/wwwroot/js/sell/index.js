var sellTable;

$(document).ready(function () {
    loadSell();
});

function loadSell() {
    
    //  Load Datatable
    if ($.fn.DataTable.isDataTable('#sellTable')) {
        sellTable.columns.adjust();
        $('#sellTable tbody').empty();
    }

    sellTable = $('#sellTable').DataTable({
        processing: true,
        serverSide: false,
        filter: false,
        orderMulti: false,
        autoWidth: false,
        ordering: true,
        pageLength: 5,
        "ajax": {
            "url": "/Sell/LoadSell",
            "type": "GET"
        },
        "columns": [
            { "data": "item.itemName",
                render: function (data) {
                    return `
                        <div class="d-flex align-items-center">
                            <i class="bi bi-box-seam me-2 text-warning"></i>
                            ${data}
                        </div>
                    `;
                }
            },
            { "data": "quantity",
                render: function (data) {
                    return `<span class="badge rounded-pill bg-danger px-3">${data}</span>`;
            }
            },
            { "data": "sellDate",
                render: function (data) {
                    return new Date(data).toLocaleDateString('en-PH', {
                        year: 'numeric',
                        month: 'short',
                        day: 'numeric'
                    });
                }
            }
        ],
        "order": [[2, "desc"]]
    });
}