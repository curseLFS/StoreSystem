var purchaseTable;

$(document).ready(function () {
    loadPurchase();
});

function loadPurchase() {
    
    //  Load Datatable
    if ($.fn.DataTable.isDataTable('#purchaseTable')) {
        purchaseTable.columns.adjust();
        $('#purchaseTable tbody').empty();
    }

    purchaseTable = $('#purchaseTable').DataTable({
       processing: true,
        serverSide: false,
        filter: false,
        orderMulti: false,
        autoWidth: false,
        ordering: true,
        pageLength: 5,
        "ajax": {
            "url": "/Purchase/LoadPurchase",
            "type": "GET"
        },
        "columns": [
            { "data": "item.itemName",
                render: function (data) {
                    return `
                        <div class="d-flex align-items-center">
                            <i class="bi bi-box-seam me-2 text-success"></i>
                            ${data}
                        </div>
                    `;
                }
            },
            { "data": "quantity",
                render: function (data) {
                    return `<span class="badge rounded-pill bg-success px-3">${data}</span>`;
                }
            },
            { "data": "purchaseDate",
                render: function (data) {
                    return new Date(data).toLocaleDateString("en-PH", {
                        year: "numeric",
                        month: "short",
                        day: "numeric"
                    });
                }
            }
        ],
        "order": [[2, "desc"]]
    });
}