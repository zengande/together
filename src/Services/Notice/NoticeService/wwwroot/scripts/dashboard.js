var dashboard = new Vue({
    el: '#dashboard',
    data: {
        statistics: {
            total: 0,
            success: 0,
            failure: 0,
            failureRatio: 0
        }
    },
    computed: {
        failureRatioValue: function () {
            if (this.statistics.total <= 0) {
                return "N/A";
            }
            return (100 * this.statistics.failure / this.statistics.total).toFixed(2) + '%';
        }
    },
    mounted: function () {
        var chart = initChart();

        window.addEventListener("resize", function () {
            chart.resize();
        });
    }
});

function initChart() {
    var domChart = document.getElementById("chart");
    var chart = echarts.init(domChart, "westeros");
    var option = {
        title: {

        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['要求', '成功', '失败']
        },
        toolbox: {
            feature: {
                saveAsImage: {}
            }
        },
        xAxis: {
            type: 'category',
            boundaryGap: false,
            data: []
        },
        yAxis: {
            type: 'value'
        },
        series: [
            {
                name: '要求',
                type: 'line',
                data: [],
                itemStyle: {
                    normal: {
                        color: '#006394',
                        lineStyle: {
                            color: '#006394'
                        }
                    }
                }
            },
            {
                name: '成功',
                type: 'line',
                itemStyle: {
                    normal: {
                        color: '#008069',
                        lineStyle: {
                            color: '#008069'
                        }
                    }
                },
                data: []
            },
            {
                name: '失败',
                type: 'line',
                itemStyle: {
                    normal: {
                        color: '#a00006',
                        lineStyle: {
                            color: '#a00006'
                        }
                    }
                },
                data: []
            }
        ]
    };
    chart.setOption(option);
    axios.get('/api/statistics/overview')
        .then(function (response) {
            // handle success
            if (response.status === 200) {
                var data = response.data;
                dashboard.statistics.total = data.total;
                dashboard.statistics.success = data.success;
                dashboard.statistics.failure = data.failure;
                chart.setOption({
                    xAxis: {
                        data: data.category
                    },
                    series: [{
                        name: "要求",
                        data: data.series[0].data
                    }, {
                        name: "成功",
                        data: data.series[1].data
                    }, {
                        name: "失败",
                        data: data.series[2].data
                    }]
                });
            }
        })
        .catch(function (error) {
            // handle error
            console.log(error);
        });

    return chart;
}