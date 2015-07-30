
var homeIndexModule = angular.module("homeIndex", ['ngRoute', 'ui.bootstrap']);
      


homeIndexModule.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "productsController",
        templateUrl: "/templates/productsView.html"
    });


    $routeProvider.otherwise({ redirectTo: "/" });
}]);

homeIndexModule.factory("dataService", ["$http", "$q", function ($http, $q) {
    var products = [];
    var isInit = false;

    var isReady = function () {
        return isInit;
    };

    var getproducts = function () {
        var deferred = $q.defer();
        $http.get("/api/catalogs")
            .then(function (result) {
                // success
                angular.copy(result.data, products);
                deferred.resolve();
                isInit = true;
            },
                function () {
                    // error
                    deferred.reject();
                });

        return deferred.promise;
    };


    function findproduct(id) {
        var found = null;

        $.each(products, function (i, item) {
            if (item.id == id) {
                found = item;
                return false;
            }
        });
        return found;
    }

  
    var updateproduct = function (product) {
        var deferred = $q.defer();
        // product.body = product.newBody;
        $http.put("/api/products/" + product.id, product)
        .then(function (result) {
            window.location.reload();
            //var t = findProduct(product);
            //t.name = product.name;
            //t.price = product.price;
            //t.description = t.description;
            deferred.resolve();
        },
        function () {
            deferred.reject;
        });
        return deferred.promise;
    };

    return {
        products: products,
        getproducts: getproducts,
        isReady: isReady,
        updateproduct: updateproduct
    };

}]);

homeIndexModule.controller("productsController", ["$modal", "$scope", "$http", "dataService", function ($modal, $scope, $http, dataService) {
    $scope.categories = dataService;
    $scope.isBusy = false;

    if (dataService.isReady() === false) {
        $scope.isBusy = true;
        dataService.getproducts()
            .then(function () {
                // success
            },
                function () {
                    // error
                    alert("could not load products.");
                })
            .then(function () {
                $scope.isBusy = false;
            });
    }

    //MODAL windows
    //reply 
  

    //updateproduct
    $scope.updateproduct = function (product, idx) {
        var modalInstance = $modal.open({
            controller: "updateproductControllerInline",
            templateUrl: '/templates/updateProduct.html',
            resolve: {
                product: function () {
                    return angular.copy(product);
                },
                idx: function () {
                    return idx;
                }

            }
        });
    };

 




}]);


homeIndexModule.controller("updateproductControllerInline", ["$modalInstance", "$scope", "dataService", "$window", "product", "idx", function ($modalInstance, $scope, dataService, $window, product, idx) {
    //$scope.something = {};
    $scope.product = product;
    $scope.idx = idx;
    //   $scope.something = $scope.product;
    //   $scope.product.newBody = $scope.product.body;
   
    $scope.updateproduct = function () {
        dataService.updateproduct($scope.product, idx)
            .then(function () {
                //success

                $modalInstance.close();
            }, function () {
                //error
                alert("could not save");
            });

    };

    $scope.cancel = function () {
        closeModal($modalInstance);

    };

}]);



//helpers
function goHome() {
    window.location = "#/";
}

function closeModal(instance) {

    instance.close();
}