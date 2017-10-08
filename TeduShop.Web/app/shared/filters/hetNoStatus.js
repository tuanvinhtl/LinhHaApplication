(function (app) {
    app.filter('statusNoLai', statusNoLai);
    function statusNoLai() {
        return function (input) {
            if (input==0) {
                return 'Hết Nợ';
            }
        }
    }
})(angular.module('tedushop.common'));