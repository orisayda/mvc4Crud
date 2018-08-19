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
              .state('app.dashboard-v3', {
                  url: '/dashboard-v3',
                  templateUrl: '/tpl/app_dashboard_v3.html',
                  resolve: load(['/js/controllers/chart.js'])
              })
              .state('app.ui', {
                  url: '/ui',
                  template: '<div ui-view class="fade-in-up"></div>'
              })
              .state('app.ui.buttons', {
                  url: '/buttons',
                  templateUrl: '/tpl/ui_buttons.html'
              })
              .state('app.ui.icons', {
                  url: '/icons',
                  templateUrl: '/tpl/ui_icons.html'
              })
              .state('app.ui.grid', {
                  url: '/grid',
                  templateUrl: '/tpl/ui_grid.html'
              })
              .state('app.ui.widgets', {
                  url: '/widgets',
                  templateUrl: '/tpl/ui_widgets.html'
              })          
              .state('app.ui.bootstrap', {
                  url: '/bootstrap',
                  templateUrl: '/tpl/ui_bootstrap.html'
              })
              .state('app.ui.sortable', {
                  url: '/sortable',
                  templateUrl: '/tpl/ui_sortable.html'
              })
              .state('app.ui.scroll', {
                  url: '/scroll',
                  templateUrl: '/tpl/ui_scroll.html',
                  resolve: load('/js/controllers/scroll.js')
              })
              .state('app.ui.portlet', {
                  url: '/portlet',
                  templateUrl: '/tpl/ui_portlet.html'
              })
              .state('app.ui.timeline', {
                  url: '/timeline',
                  templateUrl: '/tpl/ui_timeline.html'
              })
              .state('app.ui.tree', {
                  url: '/tree',
                  templateUrl: '/tpl/ui_tree.html',
                  resolve: load(['angularBootstrapNavTree', '/js/controllers/tree.js'])
              })
              .state('app.ui.toaster', {
                  url: '/toaster',
                  templateUrl: '/tpl/ui_toaster.html',
                  resolve: load(['toaster', '/js/controllers/toaster.js'])
              })
              .state('app.ui.jvectormap', {
                  url: '/jvectormap',
                  templateUrl: '/tpl/ui_jvectormap.html',
                  resolve: load('/js/controllers/vectormap.js')
              })
              .state('app.ui.googlemap', {
                  url: '/googlemap',
                  templateUrl: '/tpl/ui_googlemap.html',
                  resolve: load(['/js/app/map/load-google-maps.js', '/js/app/map/ui-map.js', '/js/app/map/map.js'], function(){ return loadGoogleMaps(); })
              })
              .state('app.chart', {
                  url: '/chart',
                  templateUrl: '/tpl/ui_chart.html',
                  resolve: load('/js/controllers/chart.js')
              })
              // table
              .state('app.table', {
                  url: '/table',
                  template: '<div ui-view></div>'
              })
              .state('app.table.static', {
                  url: '/static',
                  templateUrl: '/tpl/table_static.html'
              })
              .state('app.table.datatable', {
                  url: '/datatable',
                  templateUrl: '/tpl/table_datatable.html'
              })
              .state('app.table.footable', {
                  url: '/footable',
                  templateUrl: '/tpl/table_footable.html'
              })
              .state('app.table.grid', {
                  url: '/grid',
                  templateUrl: '/tpl/table_grid.html',
                  resolve: load(['ngGrid','/js/controllers/grid.js'])
              })
              .state('app.table.uigrid', {
                  url: '/uigrid',
                  templateUrl: '/tpl/table_uigrid.html',
                  resolve: load(['ui.grid','/js/controllers/uigrid.js'])
              })
              .state('app.table.editable', {
                  url: '/editable',
                  templateUrl: '/tpl/table_editable.html',
                  controller: 'XeditableCtrl',
                  resolve: load(['xeditable','/js/controllers/xeditable.js'])
              })
              .state('app.table.smart', {
                  url: '/smart',
                  templateUrl: '/tpl/table_smart.html',
                  resolve: load(['smart-table','/js/controllers/table.js'])
              })
              // form
              .state('app.form', {
                  url: '/form',
                  template: '<div ui-view class="fade-in"></div>',
                  resolve: load('/js/controllers/form.js')
              })
             /* .state('app.event', {
                  url: '/event',
                  template: '<div ui-view class="fade-in"></div>',
                  resolve: load('/js/controllers/event.js')
              })*/
              .state('app.form.components', {
                  url: '/components',
                  templateUrl: '/tpl/form_components.html',
                  resolve: load(['ngBootstrap','daterangepicker','/js/controllers/form.components.js'])
              })
              .state('app.form.elements', {
                  url: '/elements',
                  templateUrl: '/tpl/form_elements.html'
              })
              .state('app.form.validation', {
                  url: '/validation',
                  templateUrl: '/tpl/form_validation.html'
              })
              .state('app.form.wizard', {
                  url: '/wizard',
                  templateUrl: '/tpl/form_wizard.html'
              })
              .state('app.form.fileupload', {
                  url: '/fileupload',
                  templateUrl: '/tpl/form_fileupload.html',
                  resolve: load(['angularFileUpload','/js/controllers/file-upload.js'])
              })
              .state('app.form.imagecrop', {
                  url: '/imagecrop',
                  templateUrl: '/tpl/form_imagecrop.html',
                  resolve: load(['ngImgCrop','/js/controllers/imgcrop.js'])
              })
              .state('app.form.select', {
                  url: '/select',
                  templateUrl: '/tpl/form_select.html',
                  controller: 'SelectCtrl',
                  resolve: load(['ui.select','/js/controllers/select.js'])
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

              .state('app.form.slider', {
                  url: '/slider',
                  templateUrl: '/tpl/form_slider.html',
                  controller: 'SliderCtrl',
                  resolve: load(['vr.directives.slider','/js/controllers/slider.js'])
              })
              .state('app.form.editor', {
                  url: '/editor',
                  templateUrl: '/tpl/form_editor.html',
                  controller: 'EditorCtrl',
                  resolve: load(['textAngular','/js/controllers/editor.js'])
              })
              .state('app.form.xeditable', {
                  url: '/xeditable',
                  templateUrl: '/tpl/form_xeditable.html',
                  controller: 'XeditableCtrl',
                  resolve: load(['xeditable','/js/controllers/xeditable.js'])
              })
              // pages
              .state('app.page', {
                  url: '/page',
                  template: '<div ui-view class="fade-in-down"></div>'
              })
              .state('app.page.profile', {
                  url: '/profile',
                  templateUrl: '/tpl/page_profile.html'
              })
              .state('app.page.post', {
                  url: '/post',
                  templateUrl: '/tpl/page_post.html'
              })
              .state('app.page.search', {
                  url: '/search',
                  templateUrl: '/tpl/page_search.html'
              })
              .state('app.page.invoice', {
                  url: '/invoice',
                  templateUrl: '/tpl/page_invoice.html'
              })
              .state('app.page.price', {
                  url: '/price',
                  templateUrl: '/tpl/page_price.html'
              })
              .state('app.docs', {
                  url: '/docs',
                  templateUrl: '/tpl/docs.html'
              })
             
              // others
              .state('lockme', {
                  url: '/lockme',
                  templateUrl: '/tpl/page_lockme.html'
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


              .state('access.forgotpwd', {
                  url: '/forgotpwd',
                  templateUrl: '/tpl/page_forgotpwd.html'
              })
              .state('access.404', {
                  url: '/404',
                  templateUrl: '/tpl/page_404.html'
              })

              // fullCalendar
              .state('app.calendar', {
                  url: '/calendar',
                  templateUrl: '/tpl/app_calendar.html',
                  // use resolve to load other dependences
                  resolve: load(['moment','fullcalendar','ui.calendar','js/app/calendar/calendar.js'])
              })

              // mail
              .state('app.mail', {
                  abstract: true,
                  url: '/mail',
                  templateUrl: '/tpl/mail.html',
                  // use resolve to load other dependences
                  resolve: load( ['/js/app/mail/mail.js','/js/app/mail/mail-service.js','moment'] )
              })
              .state('app.mail.list', {
                  url: '/inbox/{fold}',
                  templateUrl: '/tpl/mail.list.html'
              })
              .state('app.mail.detail', {
                  url: '/{mailId:[0-9]{1,4}}',
                  templateUrl: '/tpl/mail.detail.html'
              })
              .state('app.mail.compose', {
                  url: '/compose',
                  templateUrl: '/tpl/mail.new.html'
              })

              .state('layout', {
                  abstract: true,
                  url: '/layout',
                  templateUrl: '/tpl/layout.html'
              })
              .state('layout.fullwidth', {
                  url: '/fullwidth',
                  views: {
                      '': {
                          templateUrl: '/tpl/layout_fullwidth.html'
                      },
                      'footer': {
                          templateUrl: '/tpl/layout_footer_fullwidth.html'
                      }
                  },
                  resolve: load( ['/js/controllers/vectormap.js'] )
              })
              .state('layout.mobile', {
                  url: '/mobile',
                  views: {
                      '': {
                          templateUrl: '/tpl/layout_mobile.html'
                      },
                      'footer': {
                          templateUrl: '/tpl/layout_footer_mobile.html'
                      }
                  }
              })
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
              })
              .state('apps.note', {
                  url: '/note',
                  templateUrl: '/tpl/apps_note.html',
                  resolve: load( ['/js/app/note/note.js','moment'] )
              })
              .state('apps.contact', {
                  url: '/contact',
                  templateUrl: '/tpl/apps_contact.html',
                  resolve: load( ['/js/app/contact/contact.js'] )
              })
              .state('app.weather', {
                  url: '/weather',
                  templateUrl: '/tpl/apps_weather.html',
                  resolve: load(['/js/app/weather/skycons.js','angular-skycons','/js/app/weather/ctrl.js','moment'])
              })
              .state('app.todo', {
                  url: '/todo',
                  templateUrl: '/tpl/apps_todo.html',
                  resolve: load(['/js/app/todo/todo.js', 'moment'])
              })
              .state('app.todo.list', {
                  url: '/{fold}'
              })
              .state('app.note', {
                  url: '/note',
                  templateUrl: '/tpl/apps_note_material.html',
                  resolve: load(['/js/app/note/note.js', 'moment'])
              })
              .state('music', {
                  url: '/music',
                  templateUrl: '/tpl/music.html',
                  controller: 'MusicCtrl',
                  resolve: load([
                            'com.2fdevs.videogular', 
                            'com.2fdevs.videogular.plugins.controls', 
                            'com.2fdevs.videogular.plugins.overlayplay',
                            'com.2fdevs.videogular.plugins.poster',
                            'com.2fdevs.videogular.plugins.buffering',
                            '/js/app/music/ctrl.js', 
                            '/js/app/music/theme.css'
                  ])
              })
                  .state('music.home', {
                      url: '/home',
                      templateUrl: '/tpl/music.home.html'
                  })
                  .state('music.genres', {
                      url: '/genres',
                      templateUrl: '/tpl/music.genres.html'
                  })
                  .state('music.detail', {
                      url: '/detail',
                      templateUrl: '/tpl/music.detail.html'
                  })
                  .state('music.mtv', {
                      url: '/mtv',
                      templateUrl: '/tpl/music.mtv.html'
                  })
                  .state('music.mtvdetail', {
                      url: '/mtvdetail',
                      templateUrl: '/tpl/music.mtv.detail.html'
                  })
                  .state('music.playlist', {
                      url: '/playlist/{fold}',
                      templateUrl: '/tpl/music.playlist.html'
                  })
              .state('app.material', {
                  url: '/material',
                  template: '<div ui-view class="wrapper-md"></div>',
                  resolve: load(['/js/controllers/material.js'])
              })
                .state('app.material.button', {
                    url: '/button',
                    templateUrl: '/tpl/material/button.html'
                })
                .state('app.material.color', {
                    url: '/color',
                    templateUrl: '/tpl/material/color.html'
                })
                .state('app.material.icon', {
                    url: '/icon',
                    templateUrl: '/tpl/material/icon.html'
                })
                .state('app.material.card', {
                    url: '/card',
                    templateUrl: '/tpl/material/card.html'
                })
                .state('app.material.form', {
                    url: '/form',
                    templateUrl: '/tpl/material/form.html'
                })
                .state('app.material.list', {
                    url: '/list',
                    templateUrl: '/tpl/material/list.html'
                })
                .state('app.material.ngmaterial', {
                    url: '/ngmaterial',
                    templateUrl: '/tpl/material/ngmaterial.html'
                });
             /*   .state('app.create.event', {
                    url: '/create/event',
                    templateUrl: '/tpl/create/event.html'
                });


        /*  app.run(['authService', function (authService) {
              authService.fillAuthData();
          }]);

          */
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

// .controller('TableEventListController', ['$scope',
//     function($scope) {}
//   ])

//   // .controller('userController', ['$scope',
//   //   function($scope) {}
//   // ])
//   // .controller('companyController', ['$scope',
//   //   function($scope) {}
//   // ])

//   .run(['$rootScope', '$state', '$stateParams',
//     function($rootScope, $state, $stateParams) {
//       $rootScope.$state = $state;
//       $rootScope.$stateParams = $stateParams;
//     }
//   ])
//   .run(['$rootScope', '$state',
//     function($rootScope, $state) {
//       $rootScope.$on('$stateChangeStart', function(evt, to, params) {
//         if (to.redirectTo) {
//           evt.preventDefault();
//           $state.go(to.redirectTo, params)
//         }
//       });
//     }
//   ])
