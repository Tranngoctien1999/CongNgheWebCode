function UpdateChart(labels, data) {
    'use strict'
    // Make the dashboard widgets sortable Using jquery UI
    $('.connectedSortable').sortable({
        placeholder: 'sort-highlight',
        connectWith: '.connectedSortable',
        handle: '.card-header, .nav-tabs',
        forcePlaceholderSize: true,
        zIndex: 999999
    })
    $('.connectedSortable .card-header').css('cursor', 'move')

    // bootstrap WYSIHTML5 - text editor
    $('.textarea').summernote()

    // The Calender
    $('#calendar').datetimepicker({
        format: 'L',
        inline: true
    })

    // SLIMSCROLL FOR CHAT WIDGET
    $('#chat-box').overlayScrollbars({
        height: '250px'
    })

    /* Chart.js Charts */
    // Sales chart
    var salesChartCanvas = document.getElementById('revenue-chart-canvas').getContext('2d')
    // $('#revenue-chart').get(0).getContext('2d');

    var salesChartData = {
        labels: labels,
        datasets: [
            {
                label: 'Revenue',
                backgroundColor: 'rgba(60,141,188,0.9)',
                borderColor: 'rgba(60,141,188,0.8)',
                pointRadius: false,
                pointColor: '#3b8bba',
                pointStrokeColor: 'rgba(60,141,188,1)',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(60,141,188,1)',
                data: data
            }
        ]
    }

    var salesChartOptions = {
        maintainAspectRatio: false,
        responsive: true,
        legend: {
            display: false
        },
        scales: {
            xAxes: [
                {
                    gridLines: {
                        display: false
                    }
                }
            ],
            yAxes: [
                {
                    gridLines: {
                        display: false
                    }
                }
            ]
        }
    }

    // This will get the first returned node in the jQuery collection.
    // eslint-disable-next-line no-unused-vars
    var salesChart = new Chart(salesChartCanvas,
        {
            type: 'line',
            data: salesChartData,
            options: salesChartOptions
        })
}