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

        this.fillTableInfo(chartInfo);

        var ctx = this.chartElem.getContext('2d'); 
        this.chart = new Chart(ctx, config);
    }

    reload(chartInfo) {
        this.chart.destroy();
        this.build(chartInfo);
    }

    fillTableInfo(chartInfo) {
        this.tableElem.innerHTML = "";
        var total = chartInfo.amounts.reduce((accumulate, current) => accumulate + current);
        for (var i = 0; i < chartInfo.labels.length; i++) {
            var name = chartInfo.labels[i];
            var amount = chartInfo.amounts[i];

            var trElem = document.createElement('tr');

            var tdElem1 = document.createElement('td');
            tdElem1.innerHTML = name;
            trElem.appendChild(tdElem1);

            var tdElem2 = document.createElement('td');
            tdElem2.innerHTML = Math.round(amount / (total / 100)) + '%';
            trElem.appendChild(tdElem2);

            var tdElem3 = document.createElement('td');
            tdElem3.innerHTML = amount + ' UAH';
            trElem.appendChild(tdElem3);

            tableElem.appendChild(trElem);
        }

        var trElemTotal = document.createElement('tr');
        trElemTotal.appendChild(document.createElement('td'));
        trElemTotal.appendChild(document.createElement('td'));

        var tdElemTotalSum = document.createElement('td');
        tdElemTotalSum.innerHTML = total + ' UAH';
        trElemTotal.appendChild(tdElemTotalSum);

        this.tableElem.appendChild(trElemTotal);
    }
}