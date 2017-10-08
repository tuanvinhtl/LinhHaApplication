(function (app) {
    app.filter('statusFilter', statusFilter);
    function statusFilter() {
        return function (input) {
            if (input==true) {
                return 'Hết Nợ';
            }
            else {
                return 'Còn Nợ';
            }
        }
    }
})(angular.module('tedushop.common'));