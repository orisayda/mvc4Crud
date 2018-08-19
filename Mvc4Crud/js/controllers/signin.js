'use strict';

/* Controllers */
  // signin controller
// app.controller('SigninFormController', ['$scope', '$http', '$state','$rootScope', function($scope, $http, $state,$rootScope,AUTH_EVENT,AuthService) {
// app.controller('SigninFormController', ['$scope', '$http', '$state','$urlRouterProvider', function($scope, $http, $state,$urlRouterProvider) {
app.controller('SigninFormController', ['$scope', '$http', '$state','$rootScope', function($scope, $http, $state,$rootScope) {
    $scope.user = {};
    $scope.authError = null;
    // $scope.credentials = { username:'',password:''};
    
    $scope.login = function() {
      $scope.authError = null;
      // Try to login
      $http.post('/User/Login', {email: $scope.user.email, password: $scope.user.password})
      .then(function (response,) {
          
          //console.log(response,toDoEventRoutes);

          if (response.data[0].success != 1) {
              $scope.authError = response.data[0].errormsg;
            //  $rootScope.$broadcast(AUTH_EVENTS.loginFailed);

        }else{

              //$rootScope.$broadcast(AUTH_EVENT.loginSuccess);
             // $scope.setCurrentUser(response);
              // document.cookie = 'ASP.NET_SessionId=' + response.data[2] + ';Path=/;';
              //$cookies.put("first_name",response.data.user.first_name );

              // $state.go('app.dashboard-v2');

              toDoEventRoutes.user_id = response.data[1].user_id;
              // console.log($rootScope);
              // $rootScope.loggeIn = true;

              //console.log(toDoEventRoutes,321);

              // $urlRouterProvider.otherwise('/app/dashboard-v2');
              
              $state.go('app.event_list');

             // window.location = 'http://localhost:51166/User/Auth#/app/dashboard-v2';

        }
      }, function(x) {
        $scope.authError = 'Server Error';
      });
    };
  }])
;


