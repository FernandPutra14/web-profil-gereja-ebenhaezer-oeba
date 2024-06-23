document.addEventListener('DOMContentLoaded', () => {
    const ctx = document.getElementById('doughnut');
    let chart; 
    let isAnimating = false; 

    function createChart() {
        chart = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ['Lansia', 'Dewasa', 'Pemuda', 'Remaja', 'Anak'],
                datasets: [{
                    label: 'Jemaat Gereja',
                    data: [90, 410, 240, 160, 100],
                    borderWidth: 1,
                }]
            },
            options: {
                responsive: true,
                borderWidth: 20,
                borderRadius: 1,
                animation: {
                    duration: 4000
                },
                plugins: {
                    legend: {
                        display: true,
                        position: 'right',
                        labels: {
                            font: {
                                size: 14 
                            },
                            padding: 35
                        }
                    }
                }
            }
        });
    }

    const observer = new IntersectionObserver(function (entries) {
        if (entries[0].isIntersecting === true && !isAnimating) {
            isAnimating = true; 
            setTimeout(() => { 
                if (chart) { 
                    chart.destroy();
                }
                createChart(); 
            }, 300); 
        } else if (entries[0].isIntersecting === false) {
            isAnimating = false;
        }
    }, { threshold: [0.1] });

    observer.observe(document.querySelector("#doughnut"));
});