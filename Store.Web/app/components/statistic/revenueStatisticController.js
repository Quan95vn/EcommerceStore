
(function (app) {
    app.controller('revenueStatisticController', revenueStatisticController);

    revenueStatisticController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService','$filter'];

    function revenueStatisticController($scope, apiService, notificationService, $state, commonService, $filter) {
        $scope.tabledata = [];
        $scope.labels = [];
        $scope.series = ['Doanh số', 'Lợi nhuận'];
        $scope.loading = true;

        $scope.chartdata = [];
        $scope.getStatistic = getStatistic;

        var config = {
            params:{
                //mm/dd/yyyy
                fromDate: '01/01/2016',
                toDate: '01/01/2018'
            }
        }

        function getStatistic() {
            apiService.get('/api/statistic/getrevenue?fromDate=' + config.params.fromDate + '&toDate=' + config.params.toDate, null, loadRevenueCompleted, loadRevenueFailed);

            function loadRevenueCompleted(response) {
                $scope.tabledata = response.data;

                var labels = [];
                var chartData = [];
                var revenues = [];
                var benefits = [];

                $.each(response.data, function (i, item) {
                    labels.push($filter('date')(item.Date, 'dd/MM/yyyy'));
                    revenues.push(item.Revenue);
                    benefits.push(item.Benefit);
                });
                chartData.push(revenues);
                chartData.push(benefits);

                $scope.chartdata = chartData;
                $scope.labels = labels;
              
                $scope.loading = false;
            }

            function loadRevenueFailed(response) {
                notificationService.displayError(response.data);
            }
        }

        getStatistic();

    }

})(angular.module('quanstore.statistic'));