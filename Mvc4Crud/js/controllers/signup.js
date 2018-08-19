'use strict';

// signup controller
app.controller('SignupFormController', ['$scope', '$http', '$state', function ($scope, $http, $state) {
    $scope.user = {};
    $scope.authError = null;
    $scope.signup = function () {
        $scope.authError = null;
        // Try to create
        $http.post('/User/Register', { first_name: $scope.user.name, last_name: $scope.user.last_name, email: $scope.user.email, password: $scope.user.password })
      .then(function (response) {
         
         //console.log(response,toDoEventRoutes);

          if (response.data[0].success != 1) {
              $scope.authError = response.data[0].errormsg;
          } else {

              toDoEventRoutes.user_id = response.data[1].user_id;
              $state.go('app.event_list');
          }
      }, function (x) {
          $scope.authError = 'Server Error';
      });
    };
}])
;