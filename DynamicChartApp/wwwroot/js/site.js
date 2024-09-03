let chartInstance;

// Verileri çekip grafik oluşturma işlevi
function fetchDataAndCreateChart() {
    const connectionString = document.getElementById('connectionString').value;
    const query = document.getElementById('query').value;
    const chartType = document.getElementById('chartType').value;

    fetch('https://localhost:7172/api/database/getdata', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            connectionString: connectionString,
            query: query,
            chartType: chartType
        })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log(data); // Sunucudan gelen verileri kontrol et
            if (!data.type || !data.data || !data.data.labels || !data.data.dataSets || data.data.dataSets.length === 0) {
                throw new Error('Incomplete data received from server');
            }
            createChart(data); // data direkt olarak gönder
        })
        .catch(error => console.error('Veri çekilirken hata oluştu:', error));
}

// Grafiği oluşturma işlevi
function createChart(chartData) {
    if (chartInstance) {
        chartInstance.destroy();  // Önceki grafiği yok et
    }

    const ctx = document.getElementById('chartCanvas').getContext('2d');

    chartInstance = new Chart(ctx, {
        type: chartData.type || 'bar',
        data: {
            labels: chartData.data.labels,
            datasets: [{
                label: 'Veriler',
                data: chartData.data.dataSets[0], // İlk veri kümesini kullan
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}
