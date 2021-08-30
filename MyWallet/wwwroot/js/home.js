class WalletInfoChart {
    constructor(chartElem, tableElem, dataSetNameElem) {
        this.chartElem = chartElem;
        this.tableElem = tableElem;
        this.dataSetNameElem = dataSetNameElem;
    }

    build(chartInfo) {
        this.dataSetNameElem.innerHTML = chartInfo.dataSetLabel;

        const data = {
            labels: chartInfo.labels,
            datasets: [
                {
                    label: chartInfo.dataSetLabel,
                    data: chartInfo.amounts,
                    backgroundColor: chartInfo.colors,
                    hoverOffset: 4
                }]
        };

        const config = {
            type: 'pie',
            data: data,
        };

        var ctx = this.chartElem.getContext('2d'); 
        this.chart = new Chart(ctx, config);
    }

    reload(chartInfo) {
        this.chart.destroy();
        this.build(chartInfo);
    }
}