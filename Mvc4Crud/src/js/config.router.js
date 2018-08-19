'use strict';

/**
 * Config for the router
 */


angular.module('app')
  .run(
    [          '$rootScope', '$state', '$stateParams',
      function ($rootScope,   $state,   $stateParams) {
          $rootScope.$state = $state;
          $rootScope.$stateParams = $stateParams; 
          //console.log(toDoEventRoutes.edit_event);    

          $rootScope.title = $state.current.title;
   
      }
    ]
  )
  .config(
    [          '$stateProvider', '$urlRouterProvider','$locationProvider', 'JQ_CONFIG', 'MODULE_CONFIG', 
      function ($stateProvider,   $urlRouterProvider,  $locationProvider, JQ_CONFIG, MODULE_CONFIG) {
          
          var layout = "/tpl/app.html";

          //console.log(toDoEventRoutes.singInUser);


          if(window.location.href.indexOf("material") > 0){
            layout = "/tpl/blocks/material.layout.html";
            $urlRouterProvider
              .otherwise('/app/dashboard-v2');
              // .otherwise('dashboard-v2');
              // console.log('material');
          }
          else{

            if(toDoEventRoutes.user_id == ""){

             $urlRouterProvider
              
              .otherwise('/access/signin');

            }else{

              // console.log('dashboard-v2');
              $urlRouterProvider.otherwise('/app/dashboard-v2');


            }

            $urlRouterProvider
              // .otherwise('/app/dashboard-v2');
             
              .otherwise('/access/signin');

              //.otherwise(toDoEventRoutes.list_event); 

             // .otherwise(toDoEventRoutes.singInUser);
          }

          // console.log(toDoEventRoutes);

        
          $stateProvider
              .state('app', {
                  abstract: true,
                  url: '/app',
                  templateUrl: layout,
                  resolve: load('/js/controllers/headerController.js')
              })
              .state('app.dashboard-v12', {
                  url: '/app/dashboard-v2',
                  templateUrl: '/tpl/app_dashboard_v2.html',
                  resolve: load(['/js/controllers/chart.js'])
              })
              .state('app.dashboard-v2', {
                  url: '/app/dashboard-v2',
                  // url: 'http://localhost:51166/User/Auth',
                  // templateUrl: '/tpl/app_dashboard_v2.html',
                  templateUrl:  function(){ //

                      if(toDoEventRoutes.user_id == ""){
                        $state.go('access.signin');
                      }

                      // console.log(1);

                      return toDoEventRoutes.dashboard;
                      // return '/tpl/app_dashboard_v2.html';
                  },
                  resolve: load(['/js/controllers/chart.js'])
                  // redirectTo: function(routeParams) {
                  //               alert("hello");
                  //               // window.location = 'http://localhost:51166/User/Auth';
                  //           }
              })

               .state('app.event', {
                   url: '/task/add',
                   title: 'Create Task',
                   // templateUrl: '/tpl/create_event.html',
                   templateUrl: function(){ //

                      if(toDoEventRoutes.user_id == ""){
                        $state.go('access.signin');
                      }
                      return  toDoEventRoutes.create_event
                   },
                   controller:'SelectCtrl',
                   resolve: load(['ui.select', '/js/controllers/event.js'])
               })

              .state('app.event_edit', {

                   url: '/task/edit/{event_id}',
                   title:" Edit Task",
                   // templateUrl: '/tpl/create_event.html',

                   templateUrl: function($stateParams){ 

                    if(toDoEventRoutes.user_id == ""){
                        $state.go('access.signin');
                    }
                      
                      $stateParams.event_id = globalEvent_id;
                      
                      return toDoEventRoutes.edit_event+'/'+$stateParams.event_id;

                   },

                  controller:'SelectCtrl',
                   // resolve: load(['ui.select', '/js/controllers/eventEdit.js'])
                   resolve: load(['ui.select','/js/controllers/eventEdit.js'])

               })
                .state('app.aboutUs', {
                  url: '/about',
                  templateUrl: '/tpl/about-us.html',
                  // templateUrl: function(){

                  //   if(toDoEventRoutes.user_id != ""){
                  //       $state.go('app.event_list');
                  //   }
                  //   return '/tpl/about-us.html';//toDoEventRoutes.registerUser;
                  // } ,
                  //resolve: load( ['/js/controllers/signup.js'] )
              })

                .state('app.event_list', {            

                   url: '/task/list/?page={pageNumber}&search={searchEvent}&sortBy&status',  
                   title: 'Tasks',

                  templateUrl: function($stateParams ){
                      // console.log($stateParams,"params");

                      if(toDoEventRoutes.user_id == ""){
                        $state.go('access.signin');
                      }

                      var searchvar ="";
                      var pageno = 1;
                      var sortBy = "";
                      var status = null;
                      // var edit1 = "";
                      if(typeof $stateParams.searchEvent !== 'undefined'){

                        //console.log("search agaya");
                        //return toDoEventRoutes.list_event+"/?page=1&search="+$stateParams.searchEvent;
                        searchvar = $stateParams.searchEvent;
                      }

                      if(typeof page !== 'undefined'){

                          // return page+"&search=";
                          pageno = $stateParams.pageNumber;

                      }

                      if(typeof $stateParams.sortBy !== 'undefined'){
                          sortBy = $stateParams.sortBy;
                      }

                      if(typeof $stateParams.status !== 'undefined'){
                          status = $stateParams.status;                          
                      }

                      // return toDoEventRoutes.list_event+"/?page="+pageno+"&search="+searchvar+"&edit="+edit1;
                      return toDoEventRoutes.list_event+"/?page="+pageno+"&search="+searchvar+"&sortBy="+sortBy+"&status="+status;
                  },                  
                   resolve: load(['ui.select','/js/controllers/event_list.js'])
               })

              .state('access', {
                  url: '/access',
                  template: function(){                              
                                       
                                       // return '<style type="text/css"> @media only screen and (min-width: 768px){    .uniqueSee2 {                    margin-top: 50px !important;    }    }  @media only screen and (max-width: 767px){    .uniqueSee3{            margin-bottom: 100px !important;    }} </style><div class="app-header navbar bg-black"><div class="navbar-header bg-black text-center"><button class="pull-right visible-xs dk" ui-toggle-class="show" data-target=".navbar-collapse"><i class="glyphicon glyphicon-align-justify"></i></button><a href="#/" class="navbar-brand text-lt"><img src="/img/logo.png" alt="." class="hide"><span class="hidden-folded m-l-xs" style="font-size: 15px;"><img src="/img/header_logo.png" alt="..."></span></a></div><div class="collapse pos-rlt navbar-collapse box-shadow ng-scope"><div class="row"><div class="col-sm-5"></div><div class="col-sm-3 hidden-xs" style="margin-top: 8px;"><ul class="nav navbar-nav"><li><span style="margin-top: 13px;margin-right: 50px;" class="thumb-sm avatar pull-right m-t-n-sm m-b-n-sm m-l-sm"><img src="/img/header_logo.png" alt="..."></span></li><li><a ui-sref="access.signup" href="#/access/signup"><i class="icon-arrow-up"></i>&nbsp;                                                                                      <span class="text-white">SIGN-UP</span></a></li><li><a ui-sref="access.signin" href="#/access/signin"><i class="fa fa-sign-in"></i> &nbsp;                                                                                          <span class="text-white">SIGN IN</span></a></li></ul></div><div class="col-sm-4"></div><ul class="nav navbar-nav navbar-right" style="margin: 0 10px;"><li class="hidden-sm hidden-md hidden-lg"><a ui-sref="access.signup" href="#/access/signup"><i class="icon-arrow-up"></i>&nbsp;                                                                                      <span class="text-white">SIGN-UP</span></a></li><li class="hidden-sm hidden-md hidden-lg"><a ui-sref="access.signin" href="#/access/signin"><i class="fa fa-sign-in"></i> &nbsp;                                                                                          <span class="text-white">SIGN IN</span></a></li></ul></div><div class="row"><ul class="nav navbar-nav hidden-xs"></ul></div></div></div><div ui-view class="fade-in-right-big smooth uniqueSee2"></div>';
                                       return '<style type="text/css"> @media only screen and (min-width: 768px){    .uniqueSee2 {                    margin-top: 50px !important;    }    }  @media only screen and (max-width: 767px){    .uniqueSee3{            margin-bottom: 100px !important;    }} </style><div class="app-header navbar bg-black"><div class="navbar-header bg-black text-center"><button class="pull-right visible-xs dk" ui-toggle-class="show" data-target=".navbar-collapse"><i class="glyphicon glyphicon-align-justify"></i></button><a href="#/" class="navbar-brand text-lt"><img src="/img/logo.png" alt="." class="hide"><span class="hidden-folded m-l-xs" style="font-size: 15px;"><img src="/img/header_logo.png" alt="..."></span></a></div><div class="collapse pos-rlt navbar-collapse box-shadow ng-scope"><div class="row"><div class="col-sm-5 hidden-xs" style="margin-top: 8px;"><ul class="nav navbar-nav"><li><span style="margin-top: 5px;margin-right: 50px;" class="thumb-sm avatar pull-right m-t-n-sm m-b-n-sm m-l-sm"><img src="/img/header_logo.png" alt="..."></span></li><li><a ui-sref="access.signup" href="#/access/signup"><i class="icon-arrow-up"></i>&nbsp;                                                                                                          <span class="text-white">SIGN-UP</span></a></li><li><a ui-sref="access.signin" href="#/access/signin"><i class="fa fa-sign-in"></i> &nbsp;                                                                                                              <span class="text-white">SIGN IN</span></a></li></ul></div><div class="col-sm-3 hidden-xs" style="margin-top: 8px;"></div><div class="col-sm-4"></div><ul class="nav navbar-nav navbar-right" style="margin: 0 10px;"><li class="hidden-sm hidden-md hidden-lg"><a ui-sref="access.signup" href="#/access/signup"><i class="icon-arrow-up"></i>&nbsp;                                                                                                        <span class="text-white">SIGN-UP</span></a></li><li class="hidden-sm hidden-md hidden-lg"><a ui-sref="access.signin" href="#/access/signin"><i class="fa fa-sign-in"></i> &nbsp;                                                                                                            <span class="text-white">SIGN IN</span></a></li></ul></div><div class="row"><ul class="nav navbar-nav hidden-xs"></ul></div></div></div><div ui-view class="fade-in-right-big smooth uniqueSee2"></div>';

                  }                 
              })
              .state('access.signin', {
                  url: '/signin',
                  // url: toDoEventRoutes.singInUser,
                  // templateUrl: '/tpl/page_signin.html',
                  templateUrl: function(){

                    if(toDoEventRoutes.user_id != ""){
                        $state.go('app.event_list');
                    }

                      return toDoEventRoutes.singInUser;
                  }, 

                  resolve: load( ['/js/controllers/signin.js'] )
              })
              .state('access.signup', {
                  url: '/signup',
                  // templateUrl: '/tpl/page_signup.html',
                  templateUrl: function(){
                    if(toDoEventRoutes.user_id != ""){
                        $state.go('app.event_list');
                    }
                    return toDoEventRoutes.registerUser;
                  } ,
                  resolve: load( ['/js/controllers/signup.js'] )
              })
              //  .state('access.aboutUs', {
              //     url: '/about',
              //     // templateUrl: '/tpl/page_signup.html',
              //     templateUrl: '/tpl/about-us1.html',
              //     //resolve: load( ['/js/controllers/signup.js'] )
              // })



              .state('layout.app', {
                  url: '/app',
                  views: {
                      '': {
                          templateUrl: '/tpl/layout_app.html'
                      },
                      'footer': {
                          templateUrl: '/tpl/layout_footer_fullwidth.html'
                      }
                  },
                  resolve: load( ['/js/controllers/tab.js'] )
              })
              .state('apps', {
                  abstract: true,
                  url: '/apps',
                  templateUrl: '/tpl/layout.html'
              });

          function load(srcs, callback) {
            return {
                deps: ['$ocLazyLoad', '$q',
                  function( $ocLazyLoad, $q ){
                    var deferred = $q.defer();
                    var promise  = false;
                    srcs = angular.isArray(srcs) ? srcs : srcs.split(/\s+/);
                    if(!promise){
                      promise = deferred.promise;
                    }
                    angular.forEach(srcs, function(src) {
                      promise = promise.then( function(){
                        if(JQ_CONFIG[src]){
                          return $ocLazyLoad.load(JQ_CONFIG[src]);
                        }
                        angular.forEach(MODULE_CONFIG, function(module) {
                          if( module.name == src){
                            name = module.name;
                          }else{
                            name = src;
                          }
                        });
                        return $ocLazyLoad.load(name);
                      } );
                    });
                    deferred.resolve();
                    return callback ? promise.then(function(){ return callback(); }) : promise;
                }]
            }
          }


      }
    ]
  )
;
