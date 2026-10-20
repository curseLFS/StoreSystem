let salesGraph;

function initChart() {
 
    const ctx = document.getElementById('overallSalesGraph').getContext('2d');

    salesGraph = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['Purchased', 'Sold'],
            datasets: [
                {
                    label: 'Purchased',
                    data: [],
                    backgroundColor: '#198754',
                    borderColor: '#146c43',
                    borderWidth: 1,
                    borderRadius: 8
                },
                {
                    label: 'Sold',
                    data: [],
                    backgroundColor: '#dc3545',
                    borderColor: '#b02a37',
                    borderWidth: 1,
                    borderRadius: 8
                }
            ]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: { y: { beginAtZero: true } }
        }
    });
}
