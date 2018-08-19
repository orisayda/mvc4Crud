'use strict';




/* Controllers */

  // Form controller
app.controller('editEventController', ['$scope','$http','$state','$stateParams', function($scope, $http, $state,$stateParams,$filter) {


    $scope.addError = null;
    $scope.country  = {};
    $scope.start_time = {};
    $scope.end_time = {};
    $scope.priority = {};

    $state.current.title  = "sadfasdfasd";

    $scope.editrequired_date = null;


    // $scope.today = function() {
    //   $scope.newEvent.start_date = new Date();
    //   $scope.newEvent.end_date = new Date();
    //   // $scope.newEvent.status = false;
    // };
    // $scope.today();   
    
     insertValues();


    function insertValues(){

      var title = document.getElementById("title");
      // var location = document.getElementById("location");
      var start_date = document.getElementById("start_date");
      var end_date = document.getElementById("end_date");
      var start_time = document.getElementById("start_time");
      var end_time = document.getElementById("end_time");
      var description = document.getElementById("description");
      var priority = document.getElementById("priority");

      
      $scope.title = title.dataset.title;

      $state.current.title = "{"+ globalEvent_id + "} - " + $scope.title + " ";


      $scope.event_id = title.dataset.eventid;
      // $scope.location = location.dataset.location
      $scope.start_date = start_date.dataset.start_date;
      $scope.end_date = end_date.dataset.end_date;

      if(title.dataset.istimeset == 1){

        $scope.start_time.value = start_time.dataset.start_time;
        $scope.end_time.value = end_time.dataset.end_time;

        $scope.start_time.selected = {time: $scope.start_time.value};
        $scope.end_time.selected = {time: $scope.end_time.value};

      }

      $scope.description = description.dataset.description;
      $scope.priority.value = priority.dataset.priority;




      $scope.UpdatedEndDate = $scope.end_date;

      $scope.UpdatedStartDate = $scope.start_date;


       // console.log(title.dataset.checkstatus);

       // $scope.Updatelocation = $scope.location;

       if(title.dataset.checkStatus == "1"){
          $scope.checkStatus = true;
          $scope.status = true;
       }else{
          $scope.checkStatus = false;
          $scope.status = false;
       }

       // console.log($scope);

      
     // $scope.country.selected = {country_name: $scope.location };      
      $scope.priority.selected = {p:$scope.priority.value};

    }

    $scope.functionChangeStatus = function(getStatus){
      // console.log(getStatus);

      if(getStatus == true || getStatus == 1){
        
        // $scope.status = 1;
        $scope.checkStatus = true;
        
      }else{

        // $scope.status = 0;
        $scope.checkStatus = false;
      }
      // console.log(getStatus,$scope.status,$scope.checkStatus)
    }



    $scope.notBlackListed = function(value) {
      var blacklist = ['bad@domain.com','verybad@domain.com'];
      return blacklist.indexOf(value) === -1;
    }

    $scope.val = 15;
    var updateModel = function(val){
      $scope.$apply(function(){
        $scope.val = val;
      });
    };
    angular.element("#slider").on('slideStop', function(data){
      updateModel(data.value);
    });

    $scope.select2Number = [
      {text:'First',  value:'One'},
      {text:'Second', value:'Two'},
      {text:'Third',  value:'Three'}
    ];

    $scope.list_of_string = ['tag1', 'tag2']
    $scope.select2Options = {
        'multiple': true,
        'simple_tags': true,
        'tags': ['tag1', 'tag2', 'tag3', 'tag4']  // Can be empty list.
    };

    angular.element("#LinkInput").bind('click', function (event) {
      event.stopPropagation();
    });

    $scope.datePicker = function (start, end, label) {
      
    }

    //handling ajax request for add the event


    $scope.updateEvent= function(){

      // console.log($scope.UpdatedStartDate,$scope.UpdatedEndDate);

      if(typeof $scope.title == 'undefined'){          
          return false;
      }

      var s = new Date($scope.UpdatedStartDate);
      var e = new Date($scope.UpdatedEndDate);

      var checkDate = 0;

      //compare year
      if(e.getFullYear() < s.getFullYear()){
          $scope.editrequired_date = "End date cannot be before start date";
          console.log($scope.editrequired_date);
          return false; 
      }else{
        checkDate = 1;
      }

      if((e.getFullYear() == s.getFullYear() ) && (e.getMonth() < s.getMonth())){
        $scope.editrequired_date = "End date cannot be before start date";
        console.log($scope.editrequired_date);
        return false;          
      }else{
         checkDate = 1;
      }

      if( (e.getFullYear() == s.getFullYear() ) && (e.getMonth() == s.getMonth()) && (e.getDate() < s.getDate())){
        $scope.editrequired_date = "End date cannot be before start date";
        console.log($scope.editrequired_date);
        return false;          
      }else{
         checkDate = 1;
      }

      if(checkDate == 1){
        $scope.editrequired_date = null;
      }
      
        var upstatus = 0;        
        if( $scope.status == true  ){
          $scope.status = "True"; 
          upstatus = 1; 
        }else{
          $scope.status = "false";  
          upstatus = 0;      
        }        

        if(typeof $scope.start_time.selected == 'undefined'){
            var ust = null
        }else{
            var ust = $scope.start_time.selected.time;
        }

        if(typeof $scope.end_time.selected == 'undefined'){
            var uet = null
        }else{
          var uet = $scope.end_time.selected.time;
        }



        // console.log(ust,uet);

      var event1 = {
            title: $scope.title,
            // location: $scope.Updatelocation,
            description: $scope.description,
            start_date: $scope.UpdatedStartDate, //$scope.start_date,
            end_date: $scope.UpdatedEndDate,//$scope.end_date,
            start_time: ust,
            end_time: uet,
            priority: $scope.priority.selected.p,
            status: $scope.status,            
            event_id: $scope.event_id
            //status: $scope.status,            
      };

      event1.event_id = $scope.event_id; 

      // console.log(event1);

      // return false;
      var getBookData = updateBook(event1);

      getBookData.then(function (msg) {
        //GetAllBooks();
        // alert(msg.data);

        var resultObject = search(event1.event_id, eventList);


        var index =  eventList.indexOf(resultObject);

        event1.status = upstatus;

        var sd =  event1.start_date.substring(0, event1.start_date.indexOf('T')+0);
        var ed =  event1.end_date.substring(0, event1.end_date.indexOf('T')-0);

        if(sd != ""){
            event1.start_date = sd;            
        }

        if(ed != ""){
            event1.end_date = ed;  
        }

        if(event1.start_time == null && event1.end_time == null){
            event1.isTimeSet = '0' ; 
        }else{
            event1.isTimeSet = '1' ; 
        }

        eventList[index] = event1;

        $state.go('app.event_list', {}, {reload: true}); 

      }, function () {

          alert('Error in updating Event record');

      });      
     
    };

    function search(nameKey, myArray){
        for (var i=0; i < myArray.length; i++) {
            if (myArray[i].event_id === nameKey) {
                return myArray[i];
            }
        }
    }

    function updateBook(event){
      // console.log(event.event_id,"sending");
           var response = $http({
            method: "post",
            url: "/Event/UpdateEvent",
            data: JSON.stringify(event),
            dataType: "json"
        });
        return response;
    }


    // $scope.changedLocation = function(countryName){
        
    //     $scope.Updatelocation = countryName;
    // }

    // $scope.$watch("start_date", function(newValue, oldValue) {
    //    console.log("I've changed : ", start_date);
    // });

    $scope.changedStartDate = function(a){
      var date = JSON.stringify(a);
      var dateStr = JSON.parse(date);
      // console.log(a,date);
      $scope.UpdatedStartDate = dateStr;

    }

    $scope.changedEndDate = function(b){
      var date1 = JSON.stringify(b);
      var dateStr = JSON.parse(date1);
      // console.log(b,date1);
      $scope.UpdatedEndDate = dateStr;

    }


        $scope.AddUpdateBook = function () {
        var event = {
            title: $scope.title,
            // location: $scope.location,
            description: $scope.description,
            start_date: $scope.start_date,
            end_date: $scope.end_date,
            start_time: $scope.start_time,
            end_time: $scope.end_time,
            status: $scope.status,
            //status: $scope.status,
            
        };
        var getBookAction = $scope.Action;

        if (getBookAction == "Update") {
            event.event_id = $scope.event_id;
            //console.log(Book.event_id );
            var getBookData = updateBook(event);
            getBookData.then(function (msg) {
                //GetAllBooks();
                alert(msg.data);


              // $stateParams.edit1 = event.event_id ;
              //$state.go('app.event_list');
              $state.go('app.event_list', {}, {reload: true});

                //$state.reload();

              $scope.divBook = false;
                

            }, function () {
                alert('Error in updating book record');
            });
        } else {
            var getBookData = crudAJService.AddBook(event);
            getBookData.then(function (msg) {
               // GetAllBooks();
                alert(msg.data);
                $scope.divBook = false;
            }, function () {
                alert('Error in adding book record');
            });
        }
    }





}]);



// this is a lazy load controller, 
// so start with "app." to register this controller

app.filter('propsFilter', function() {
    return function(items, props) {
        var out = [];

        if (angular.isArray(items)) {
          items.forEach(function(item) {
            var itemMatches = false;

            var keys = Object.keys(props);
            for (var i = 0; i < keys.length; i++) {
              var prop = keys[i];
              var text = props[prop].toLowerCase();
              if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                itemMatches = true;
                break;
              }
            }

            if (itemMatches) {
              out.push(item);
            }
          });
        } else {
          // Let the output be the input untouched
          out = items;
        }

        return out;
    };
})

app.controller('SelectCtrl', function($scope, $http, $timeout) {
        $scope.disabled = undefined;
        $scope.searchEnabled = undefined;

        $scope.enable = function() {
        $scope.disabled = false;
        };

        $scope.disable = function() {
        $scope.disabled = true;
        };

        $scope.enableSearch = function() {
        $scope.searchEnabled = true;
        }

        $scope.disableSearch = function() {
        $scope.searchEnabled = false;
        }

        $scope.clear = function() {
          $scope.person.selected = undefined;
          $scope.address.selected = undefined;
          $scope.country.selected = undefined;
          $scope.event_start_time.selected = undefined;
          $scope.event_end_time.selected = undefined;
        };

        $scope.someGroupFn = function (item){

        if (item.name[0] >= 'A' && item.name[0] <= 'M')
            return 'From A - M';

        if (item.name[0] >= 'N' && item.name[0] <= 'Z')
            return 'From N - Z';

        };

        $scope.personAsync = {selected : "wladimir@email.com"};
        $scope.peopleAsync = [];

        $timeout(function(){
        $scope.peopleAsync = [
            { name: 'Adam',      email: 'adam@email.com',      age: 12, country: 'United States' },
            { name: 'Amalie',    email: 'amalie@email.com',    age: 12, country: 'Argentina' },
            { name: 'Estefanía', email: 'estefania@email.com', age: 21, country: 'Argentina' },
            { name: 'Adrian',    email: 'adrian@email.com',    age: 21, country: 'Ecuador' },
            { name: 'Wladimir',  email: 'wladimir@email.com',  age: 30, country: 'Ecuador' },
            { name: 'Samantha',  email: 'samantha@email.com',  age: 30, country: 'United States' },
            { name: 'Nicole',    email: 'nicole@email.com',    age: 43, country: 'Colombia' },
            { name: 'Natasha',   email: 'natasha@email.com',   age: 54, country: 'Ecuador' },
            { name: 'Michael',   email: 'michael@email.com',   age: 15, country: 'Colombia' },
            { name: 'Nicolás',   email: 'nicole@email.com',    age: 43, country: 'Colombia' }
          ];
        },3000);

        $scope.counter = 0;
        $scope.someFunction = function (item, model){
        $scope.counter++;
        $scope.eventResult = {item: item, model: model};
        };

        $scope.removed = function (item, model) {
        $scope.lastRemoved = {
            item: item,
            model: model
        };
        };

        $scope.person = {};
        $scope.people = [
        { name: 'Adam',      email: 'adam@email.com',      age: 12, country: 'United States' },
        { name: 'Amalie',    email: 'amalie@email.com',    age: 12, country: 'Argentina' },
        { name: 'Estefanía', email: 'estefania@email.com', age: 21, country: 'Argentina' },
        { name: 'Adrian',    email: 'adrian@email.com',    age: 21, country: 'Ecuador' },
        { name: 'Wladimir',  email: 'wladimir@email.com',  age: 30, country: 'Ecuador' },
        { name: 'Samantha',  email: 'samantha@email.com',  age: 30, country: 'United States' },
        { name: 'Nicole',    email: 'nicole@email.com',    age: 43, country: 'Colombia' },
        { name: 'Natasha',   email: 'natasha@email.com',   age: 54, country: 'Ecuador' },
        { name: 'Michael',   email: 'michael@email.com',   age: 15, country: 'Colombia' },
        { name: 'Nicolás',   email: 'nicolas@email.com',   age: 43, country: 'Colombia' }
        ];

        $scope.availableColors = ['Red','Green','Blue','Yellow','Magenta','Maroon','Umbra','Turquoise'];

        $scope.multipleDemo = {};
        $scope.multipleDemo.colors = ['Blue','Red'];
        $scope.multipleDemo.selectedPeople = [$scope.people[5], $scope.people[4]];
        $scope.multipleDemo.selectedPeopleWithGroupBy = [$scope.people[8], $scope.people[6]];
        $scope.multipleDemo.selectedPeopleSimple = ['samantha@email.com','wladimir@email.com'];


        $scope.address = {};
        $scope.refreshAddresses = function(address) {
        var params = {address: address, sensor: false};
        return $http.get(
          'http://maps.googleapis.com/maps/api/geocode/json',
          {params: params}
        ).then(function(response) {
          $scope.addresses = response.data.results;
        });
        };

        $scope.priorities = 
          [
          {p:'High'},
          {p:'Medium'},
          {p:'Low'}
          ];

        $scope.event_start_time = {};
        $scope.event_end_time = {};        
        $scope.times = [
          {time:'12:00 AM'},
          {time:'12:30 AM'},
          {time:'01:00 AM'},
          {time:'01:30 AM'},
          {time:'02:00 AM'},
          {time:'02:30 AM'},
          {time:'03:00 AM'},
          {time:'03:30 AM'},
          {time:'04:00 AM'},
          {time:'04:30 AM'},
          {time:'05:00 AM'},
          {time:'05:30 AM'},
          {time:'06:00 AM'},
          {time:'06:30 AM'},
          {time:'07:00 AM'},
          {time:'07:30 AM'},
          {time:'08:00 AM'},
          {time:'08:30 AM'},
          {time:'09:00 AM'},
          {time:'09:30 AM'},
          {time:'10:00 AM'},
          {time:'10:30 AM'},
          {time:'11:00 AM'},
          {time:'11:30 AM'},
          {time:'12:00 PM'},
          {time:'12:30 PM'},          
          {time:'01:00 PM'},
          {time:'01:30 PM'},
          {time:'02:00 PM'},
          {time:'02:30 PM'},
          {time:'03:00 PM'},
          {time:'03:30 PM'},
          {time:'04:00 PM'},
          {time:'04:30 PM'},
          {time:'05:00 PM'},
          {time:'05:30 PM'},
          {time:'06:00 PM'},
          {time:'06:30 PM'},
          {time:'07:00 PM'},
          {time:'07:30 PM'},
          {time:'08:00 PM'},
          {time:'08:30 PM'},
          {time:'09:00 PM'},
          {time:'09:30 PM'},
          {time:'10:00 PM'},
          {time:'10:30 PM'},
          {time:'11:00 PM'},
          {time:'11:30 PM'},
          {time:'12:00 PM'},
          {time:'12:30 PM'},

        ];

        $scope.country = {};

        $scope.countries = [ // Taken from https://gist.github.com/unceus/6501985
        {country_name: 'Åland Islands' },
        {country_name: 'Afghanistan' },
        {country_name: 'Albania' },
        {country_name: 'Algeria' },
        {country_name: 'American Samoa' },
        {country_name: 'Andorra' },
        {country_name: 'Angola' },
        {country_name: 'Anguilla' },
        {country_name: 'Antarctica' },
        {country_name: 'Antigua and Barbuda' },
        {country_name: 'Argentina' },
        {country_name: 'Armenia' },
        {country_name: 'Aruba' },
        {country_name: 'Australia' },
        {country_name: 'Austria' },
        {country_name: 'Azerbaijan' },
        {country_name: 'Bahamas' },
        {country_name: 'Bahrain' },
        {country_name: 'Bangladesh' },
        {country_name: 'Barbados' },
        {country_name: 'Belarus' },
        {country_name: 'Belgium' },
        {country_name: 'Belize' },
        {country_name: 'Benin' },
        {country_name: 'Bermuda' },
        {country_name: 'Bhutan' },
        {country_name: 'Bolivia' },
        {country_name: 'Bosnia and Herzegovina' },
        {country_name: 'Botswana' },
        {country_name: 'Bouvet Island' },
        {country_name: 'Brazil' },
        {country_name: 'British Indian Ocean Territory' },
        {country_name: 'Brunei Darussalam' },
        {country_name: 'Bulgaria' },
        {country_name: 'Burkina Faso' },
        {country_name: 'Burundi' },
        {country_name: 'Cambodia' },
        {country_name: 'Cameroon' },
        {country_name: 'Canada' },
        {country_name: 'Cape Verde' },
        {country_name: 'Cayman Islands' },
        {country_name: 'Central African Republic' },
        {country_name: 'Chad' },
        {country_name: 'Chile' },
        {country_name: 'China' },
        {country_name: 'Christmas Island' },
        {country_name: 'Cocos (Keeling) Islands' },
        {country_name: 'Colombia' },
        {country_name: 'Comoros' },
        {country_name: 'Congo' },
        {country_name: 'Congo, The Democratic Republic of the' },
        {country_name: 'Cook Islands' },
        {country_name: 'Costa Rica' },
        {country_name: 'Cote D\'Ivoire' },
        {country_name: 'Croatia' },
        {country_name: 'Cuba' },
        {country_name: 'Cyprus' },
        {country_name: 'Czech Republic' },
        {country_name: 'Denmark' },
        {country_name: 'Djibouti' },
        {country_name: 'Dominica' },
        {country_name: 'Dominican Republic' },
        {country_name: 'Ecuador' },
        {country_name: 'Egypt' },
        {country_name: 'El Salvador' },
        {country_name: 'Equatorial Guinea' },
        {country_name: 'Eritrea' },
        {country_name: 'Estonia' },
        {country_name: 'Ethiopia' },
        {country_name: 'Falkland Islands (Malvinas)' },
        {country_name: 'Faroe Islands' },
        {country_name: 'Fiji' },
        {country_name: 'Finland' },
        {country_name: 'France' },
        {country_name: 'French Guiana' },
        {country_name: 'French Polynesia' },
        {country_name: 'French Southern Territories' },
        {country_name: 'Gabon' },
        {country_name: 'Gambia' },
        {country_name: 'Georgia' },
        {country_name: 'Germany' },
        {country_name: 'Ghana' },
        {country_name: 'Gibraltar' },
        {country_name: 'Greece' },
        {country_name: 'Greenland' },
        {country_name: 'Grenada' },
        {country_name: 'Guadeloupe' },
        {country_name: 'Guam' },
        {country_name: 'Guatemala' },
        {country_name: 'Guernsey' },
        {country_name: 'Guinea' },
        {country_name: 'Guinea-Bissau' },
        {country_name: 'Guyana' },
        {country_name: 'Haiti' },
        {country_name: 'Heard Island and Mcdonald Islands' },
        {country_name: 'Holy See (Vatican City State)' },
        {country_name: 'Honduras' },
        {country_name: 'Hong Kong' },
        {country_name: 'Hungary' },
        {country_name: 'Iceland' },
        {country_name: 'India' },
        {country_name: 'Indonesia' },
        {country_name: 'Iran, Islamic Republic Of' },
        {country_name: 'Iraq' },
        {country_name: 'Ireland' },
        {country_name: 'Isle of Man' },
        {country_name: 'Israel' },
        {country_name: 'Italy' },
        {country_name: 'Jamaica' },
        {country_name: 'Japan' },
        {country_name: 'Jersey' },
        {country_name: 'Jordan' },
        {country_name: 'Kazakhstan' },
        {country_name: 'Kenya' },
        {country_name: 'Kiribati' },
        {country_name: 'Korea, Democratic People\'s Republic of' },
        {country_name: 'Korea, Republic of' },
        {country_name: 'Kuwait' },
        {country_name: 'Kyrgyzstan' },
        {country_name: 'Lao People\'s Democratic Republic' },
        {country_name: 'Latvia' },
        {country_name: 'Lebanon' },
        {country_name: 'Lesotho' },
        {country_name: 'Liberia' },
        {country_name: 'Libyan Arab Jamahiriya' },
        {country_name: 'Liechtenstein' },
        {country_name: 'Lithuania' },
        {country_name: 'Luxembourg' },
        {country_name: 'Macao' },
        {country_name: 'Macedonia, The Former Yugoslav Republic of' },
        {country_name: 'Madagascar' },
        {country_name: 'Malawi' },
        {country_name: 'Malaysia' },
        {country_name: 'Maldives' },
        {country_name: 'Mali' },
        {country_name: 'Malta' },
        {country_name: 'Marshall Islands' },
        {country_name: 'Martinique' },
        {country_name: 'Mauritania' },
        {country_name: 'Mauritius' },
        {country_name: 'Mayotte' },
        {country_name: 'Mexico' },
        {country_name: 'Micronesia, Federated States of' },
        {country_name: 'Moldova, Republic of' },
        {country_name: 'Monaco' },
        {country_name: 'Mongolia' },
        {country_name: 'Montserrat' },
        {country_name: 'Morocco' },
        {country_name: 'Mozambique' },
        {country_name: 'Myanmar' },
        {country_name: 'Namibia' },
        {country_name: 'Nauru' },
        {country_name: 'Nepal' },
        {country_name: 'Netherlands' },
        {country_name: 'Netherlands Antilles' },
        {country_name: 'New Caledonia' },
        {country_name: 'New Zealand' },
        {country_name: 'Nicaragua' },
        {country_name: 'Niger' },
        {country_name: 'Nigeria' },
        {country_name: 'Niue' },
        {country_name: 'Norfolk Island' },
        {country_name: 'Northern Mariana Islands' },
        {country_name: 'Norway' },
        {country_name: 'Oman' },
        {country_name: 'Pakistan' },
        {country_name: 'Palau' },
        {country_name: 'Palestinian Territory, Occupied' },
        {country_name: 'Panama' },
        {country_name: 'Papua New Guinea' },
        {country_name: 'Paraguay' },
        {country_name: 'Peru' },
        {country_name: 'Philippines' },
        {country_name: 'Pitcairn' },
        {country_name: 'Poland' },
        {country_name: 'Portugal' },
        {country_name: 'Puerto Rico' },
        {country_name: 'Qatar' },
        {country_name: 'Reunion' },
        {country_name: 'Romania' },
        {country_name: 'Russian Federation' },
        {country_name: 'Rwanda' },
        {country_name: 'Saint Helena' },
        {country_name: 'Saint Kitts and Nevis' },
        {country_name: 'Saint Lucia' },
        {country_name: 'Saint Pierre and Miquelon' },
        {country_name: 'Saint Vincent and the Grenadines' },
        {country_name: 'Samoa' },
        {country_name: 'San Marino' },
        {country_name: 'Sao Tome and Principe' },
        {country_name: 'Saudi Arabia' },
        {country_name: 'Senegal' },
        {country_name: 'Serbia and Montenegro' },
        {country_name: 'Seychelles' },
        {country_name: 'Sierra Leone' },
        {country_name: 'Singapore' },
        {country_name: 'Slovakia' },
        {country_name: 'Slovenia' },
        {country_name: 'Solomon Islands' },
        {country_name: 'Somalia' },
        {country_name: 'South Africa' },
        {country_name: 'South Georgia and the South Sandwich Islands' },
        {country_name: 'Spain' },
        {country_name: 'Sri Lanka' },
        {country_name: 'Sudan' },
        {country_name: 'Suriname' },
        {country_name: 'Svalbard and Jan Mayen' },
        {country_name: 'Swaziland' },
        {country_name: 'Sweden' },
        {country_name: 'Switzerland' },
        {country_name: 'Syrian Arab Republic' },
        {country_name: 'Taiwan, Province of China' },
        {country_name: 'Tajikistan' },
        {country_name: 'Tanzania, United Republic of' },
        {country_name: 'Thailand' },
        {country_name: 'Timor-Leste' },
        {country_name: 'Togo' },
        {country_name: 'Tokelau' },
        {country_name: 'Tonga' },
        {country_name: 'Trinidad and Tobago' },
        {country_name: 'Tunisia' },
        {country_name: 'Turkey' },
        {country_name: 'Turkmenistan' },
        {country_name: 'Turks and Caicos Islands' },
        {country_name: 'Tuvalu' },
        {country_name: 'Uganda' },
        {country_name: 'Ukraine' },
        {country_name: 'United Arab Emirates' },
        {country_name: 'United Kingdom' },
        {country_name: 'United States' },
        {country_name: 'United States Minor Outlying Islands' },
        {country_name: 'Uruguay' },
        {country_name: 'Uzbekistan' },
        {country_name: 'Vanuatu' },
        {country_name: 'Venezuela' },
        {country_name: 'Vietnam' },
        {country_name: 'Virgin Islands, British' },
        {country_name: 'Virgin Islands, U.S.' },
        {country_name: 'Wallis and Futuna' },
        {country_name: 'Western Sahara' },
        {country_name: 'Yemen' },
        {country_name: 'Zambia' },
        {country_name: 'Zimbabwe'}
        ];
});
