(function (app) {
    app.controller('applicationChuaBenhListController', applicationChuaBenhListController);
    applicationChuaBenhListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function applicationChuaBenhListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.applicationChuaBenhs = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getApplicationChuaBenhs = getApplicationChuaBenhs;
        $scope.keyWord = '';
        $scope.search = search;
        $scope.selected;
        $scope.deleteApplicationChuaBenh = deleteApplicationChuaBenh;
        $scope.deleteMutile = deleteMutile;
        $scope.selectAll = selectAll;
        $scope.isAll = false;

        function search() {
            getApplicationChuaBenhs();
        }

        function deleteMutile() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            })
            var config = {
                params: {
                    listProductCategoryId: JSON.stringify(listId)
                }
            }
            apiService.del('api/chuaBenh/deletemutile', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi');
                search()
            }, function (error) {
                notificationService.displayWarning('Xóa không thành công')
            });
        };

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.applicationChuaBenhs, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.applicationChuaBenhs, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("applicationChuaBenhs", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteApplicationChuaBenh(id) {
            var config = {
                params: {
                    id: id
                }
            }
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                apiService.del('api/chuaBenh/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data);
                    search();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công');
                })
            })
        }

        function getApplicationChuaBenhs(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('api/chuaBenh/getlistpaging', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy')
                }
                else {
                    $scope.applicationChuaBenhs = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pageCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount
                }

            }, function () {
                console.log('Load failed.');

            });
        }

        $scope.getApplicationChuaBenhs();

    }
})(angular.module('tedushop.applicationChuaBenhs'));