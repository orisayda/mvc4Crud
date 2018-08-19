'use strict';




app.controller('HeaderController', ['$scope','$http','$state','$stateParams', function($scope, $http, $state, $stateParams,$filter) {
    


    setNotificationCount();

    function setNotificationCount(){

      

      if(toDoEventRoutes.notificationCheck == 0 ){

        var noti = document.getElementById("getNotificationCount");
        
        if(noti != null){
        
          if(noti.dataset.count != 0){
              $scope.notification_count = noti.dataset.count; 
              // console.log($scope.notification_count);    

          }  
          
          toDoEventRoutes.notificationCheck = 1;
        }      
      }
    }

    // console.log(toDoEventRoutes.notificationCheck,$scope.notification_count);


    $scope.notify = function(){
      // alert("removed");
      $scope.notification_count = null;
    }

}]);

app.controller('HeaderController1', ['$scope','$http','$state','$stateParams', function($scope, $http, $state, $stateParams,$filter) {
    


    setNotificationCount1();

    function setNotificationCount1(){
      

      if(toDoEventRoutes.notificationCheck == 0){

        var noti1 = document.getElementById("getNotificationCount1");
        
        if(noti1 != null){
        
          if(noti1.dataset.count != 0){
              $scope.notification_count1 = noti1.dataset.count; 
              // console.log($scope.notification_count);    

          }  
          
          // toDoEventRoutes.notificationCheck = 1;
        }      
      }
    }


    $scope.notify1 = function(){
      // alert("removed");
      $scope.notification_count1 = null;
    }

}]);