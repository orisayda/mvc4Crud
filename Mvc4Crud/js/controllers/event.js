'use strict';

/* Controllers */

  // Form controller
app.controller('FormEventController', ['$scope','$http','$state', function($scope, $http, $state) {
    
    $scope.newEvent = {};
    $scope.addError = null;

    $scope.required_priority = null;
    $scope.required_date = null;
    $scope.required_title = null;

    $state.current.title = "Create Task";

    var monthNames = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
      ];
    
    $scope.today = function() {
      
      var d = new Date();
      // console.log(d.getDate(),monthNames[d.getMonth()],d.getFullYear());
      var startdate = d.getDate() +'-'+ monthNames[d.getMonth()] +'-'+ d.getFullYear();
      $scope.newEvent.start_date = startdate;
      $scope.newEvent.end_date = startdate;
      // $scope.newEvent.status = false;
    };

    $scope.today();   
    
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

    $scope.pChange = function(){
      $scope.required_priority = null;
    }

    // $scope.eChange = function(){
    //   $scope.required_date = null;
    // }

    //handling ajax request for add the event


    $scope.addEvent = function(){ 



      if(typeof $scope.newEvent.event_title == 'undefined' || $scope.newEvent.event_title == ""){     
          $scope.required_title = "Please enter the task title";   
          console.log($scope.required_title);
          return false;
      }else{
          $scope.required_title = null;
      }

      if(typeof $scope.newEvent.priority == 'undefined'){
          $scope.required_priority = "Please Select the task priority";
          return false;
      }      
        

      // console.log($scope.newEvent.start_date,$scope.newEvent.end_date);

      var s = new Date($scope.newEvent.start_date);
      var e = new Date($scope.newEvent.end_date);     

      // console.log(e.getFullYear(),s.getFullYear(),monthNames[e.getMonth()],monthNames[s.getMonth()],e.getDate(),s.getDate());
      var checkDate = 0;

      //compare year
      if(e.getFullYear() < s.getFullYear()){
          $scope.required_date = "End date cannot be before start date";
          console.log($scope.required_date);
          return false; 
      }else{
        checkDate = 1;
      }

      if((e.getFullYear() == s.getFullYear() ) && (e.getMonth() < s.getMonth())){
        $scope.required_date = "End date cannot be before start date";
        console.log($scope.required_date);
        return false;          
      }else{
         checkDate = 1;
      }

      if( (e.getFullYear() == s.getFullYear() ) && (e.getMonth() == s.getMonth()) && (e.getDate() < s.getDate())){
        $scope.required_date = "End date cannot be before start date";
        console.log($scope.required_date);
        return false;          
      }else{
         checkDate = 1;
      }

      if(checkDate == 1){
        $scope.required_date = null;
      }


      if (typeof $scope.newEvent.start_time == 'undefined'){
          var start_time = null;
      }else{
          var start_time = $scope.newEvent.start_time.time;
      }

      if (typeof $scope.newEvent.start_time == 'undefined'){
          var end_time = null;
      }else{
          var end_time = $scope.newEvent.end_time.time;
      }
      
      $http.post('/Event/Add',{
                              title:         $scope.newEvent.event_title,
                              // location:      $scope.newEvent.event_location.country_name,
                              start_date:    $scope.newEvent.start_date,
                              start_time:    start_time,
                              end_date:      $scope.newEvent.end_date,
                              end_time:      end_time,
                              all_time:      $scope.newEvent.all_time,
                              priority:      $scope.newEvent.priority.p,
                              description:   $scope.newEvent.description,
                              status:        $scope.newEvent.status
                            })
      .then(function(response){
        // console.log(response);
          if(response.data[0].success != 1){

            $scope.addError = response.error_msg;

          }else{

           
            var addItem = [];

            addItem.push({
                event_id:     response.data[0].eventData.event_id,
                title:        response.data[0].eventData.title,
                description:  response.data[0].eventData.description,
                start_date:   response.data[0].eventData.start_date,
                end_date:     response.data[0].eventData.end_date,
                start_time:   response.data[0].eventData.returnStartTime,
                end_time:     response.data[0].eventData.returnEndTime,
                status:       response.data[0].eventData.status,
                priority:     response.data[0].eventData.priority,
                isTimeSet:    response.data[0].eventData.isTimeSet
            });

            // console.log(addItem);

            eventList.splice(0, 0, addItem[0]); 

            // console.log(eventList);

            $state.go('app.event_list');

          }
      },function(x){
      
        $scope.addError = 'server Error';
      
      });
    };

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
        {country_name: 'Afghanistan', code: 'AF'},
        {country_name: 'Åland Islands', code: 'AX'},
        {country_name: 'Albania', code: 'AL'},
        {country_name: 'Algeria', code: 'DZ'},
        {country_name: 'American Samoa', code: 'AS'},
        {country_name: 'Andorra', code: 'AD'},
        {country_name: 'Angola', code: 'AO'},
        {country_name: 'Anguilla', code: 'AI'},
        {country_name: 'Antarctica', code: 'AQ'},
        {country_name: 'Antigua and Barbuda', code: 'AG'},
        {country_name: 'Argentina', code: 'AR'},
        {country_name: 'Armenia', code: 'AM'},
        {country_name: 'Aruba', code: 'AW'},
        {country_name: 'Australia', code: 'AU'},
        {country_name: 'Austria', code: 'AT'},
        {country_name: 'Azerbaijan', code: 'AZ'},
        {country_name: 'Bahamas', code: 'BS'},
        {country_name: 'Bahrain', code: 'BH'},
        {country_name: 'Bangladesh', code: 'BD'},
        {country_name: 'Barbados', code: 'BB'},
        {country_name: 'Belarus', code: 'BY'},
        {country_name: 'Belgium', code: 'BE'},
        {country_name: 'Belize', code: 'BZ'},
        {country_name: 'Benin', code: 'BJ'},
        {country_name: 'Bermuda', code: 'BM'},
        {country_name: 'Bhutan', code: 'BT'},
        {country_name: 'Bolivia', code: 'BO'},
        {country_name: 'Bosnia and Herzegovina', code: 'BA'},
        {country_name: 'Botswana', code: 'BW'},
        {country_name: 'Bouvet Island', code: 'BV'},
        {country_name: 'Brazil', code: 'BR'},
        {country_name: 'British Indian Ocean Territory', code: 'IO'},
        {country_name: 'Brunei Darussalam', code: 'BN'},
        {country_name: 'Bulgaria', code: 'BG'},
        {country_name: 'Burkina Faso', code: 'BF'},
        {country_name: 'Burundi', code: 'BI'},
        {country_name: 'Cambodia', code: 'KH'},
        {country_name: 'Cameroon', code: 'CM'},
        {country_name: 'Canada', code: 'CA'},
        {country_name: 'Cape Verde', code: 'CV'},
        {country_name: 'Cayman Islands', code: 'KY'},
        {country_name: 'Central African Republic', code: 'CF'},
        {country_name: 'Chad', code: 'TD'},
        {country_name: 'Chile', code: 'CL'},
        {country_name: 'China', code: 'CN'},
        {country_name: 'Christmas Island', code: 'CX'},
        {country_name: 'Cocos (Keeling) Islands', code: 'CC'},
        {country_name: 'Colombia', code: 'CO'},
        {country_name: 'Comoros', code: 'KM'},
        {country_name: 'Congo', code: 'CG'},
        {country_name: 'Congo, The Democratic Republic of the', code: 'CD'},
        {country_name: 'Cook Islands', code: 'CK'},
        {country_name: 'Costa Rica', code: 'CR'},
        {country_name: 'Cote D\'Ivoire', code: 'CI'},
        {country_name: 'Croatia', code: 'HR'},
        {country_name: 'Cuba', code: 'CU'},
        {country_name: 'Cyprus', code: 'CY'},
        {country_name: 'Czech Republic', code: 'CZ'},
        {country_name: 'Denmark', code: 'DK'},
        {country_name: 'Djibouti', code: 'DJ'},
        {country_name: 'Dominica', code: 'DM'},
        {country_name: 'Dominican Republic', code: 'DO'},
        {country_name: 'Ecuador', code: 'EC'},
        {country_name: 'Egypt', code: 'EG'},
        {country_name: 'El Salvador', code: 'SV'},
        {country_name: 'Equatorial Guinea', code: 'GQ'},
        {country_name: 'Eritrea', code: 'ER'},
        {country_name: 'Estonia', code: 'EE'},
        {country_name: 'Ethiopia', code: 'ET'},
        {country_name: 'Falkland Islands (Malvinas)', code: 'FK'},
        {country_name: 'Faroe Islands', code: 'FO'},
        {country_name: 'Fiji', code: 'FJ'},
        {country_name: 'Finland', code: 'FI'},
        {country_name: 'France', code: 'FR'},
        {country_name: 'French Guiana', code: 'GF'},
        {country_name: 'French Polynesia', code: 'PF'},
        {country_name: 'French Southern Territories', code: 'TF'},
        {country_name: 'Gabon', code: 'GA'},
        {country_name: 'Gambia', code: 'GM'},
        {country_name: 'Georgia', code: 'GE'},
        {country_name: 'Germany', code: 'DE'},
        {country_name: 'Ghana', code: 'GH'},
        {country_name: 'Gibraltar', code: 'GI'},
        {country_name: 'Greece', code: 'GR'},
        {country_name: 'Greenland', code: 'GL'},
        {country_name: 'Grenada', code: 'GD'},
        {country_name: 'Guadeloupe', code: 'GP'},
        {country_name: 'Guam', code: 'GU'},
        {country_name: 'Guatemala', code: 'GT'},
        {country_name: 'Guernsey', code: 'GG'},
        {country_name: 'Guinea', code: 'GN'},
        {country_name: 'Guinea-Bissau', code: 'GW'},
        {country_name: 'Guyana', code: 'GY'},
        {country_name: 'Haiti', code: 'HT'},
        {country_name: 'Heard Island and Mcdonald Islands', code: 'HM'},
        {country_name: 'Holy See (Vatican City State)', code: 'VA'},
        {country_name: 'Honduras', code: 'HN'},
        {country_name: 'Hong Kong', code: 'HK'},
        {country_name: 'Hungary', code: 'HU'},
        {country_name: 'Iceland', code: 'IS'},
        {country_name: 'India', code: 'IN'},
        {country_name: 'Indonesia', code: 'ID'},
        {country_name: 'Iran, Islamic Republic Of', code: 'IR'},
        {country_name: 'Iraq', code: 'IQ'},
        {country_name: 'Ireland', code: 'IE'},
        {country_name: 'Isle of Man', code: 'IM'},
        {country_name: 'Israel', code: 'IL'},
        {country_name: 'Italy', code: 'IT'},
        {country_name: 'Jamaica', code: 'JM'},
        {country_name: 'Japan', code: 'JP'},
        {country_name: 'Jersey', code: 'JE'},
        {country_name: 'Jordan', code: 'JO'},
        {country_name: 'Kazakhstan', code: 'KZ'},
        {country_name: 'Kenya', code: 'KE'},
        {country_name: 'Kiribati', code: 'KI'},
        {country_name: 'Korea, Democratic People\'s Republic of', code: 'KP'},
        {country_name: 'Korea, Republic of', code: 'KR'},
        {country_name: 'Kuwait', code: 'KW'},
        {country_name: 'Kyrgyzstan', code: 'KG'},
        {country_name: 'Lao People\'s Democratic Republic', code: 'LA'},
        {country_name: 'Latvia', code: 'LV'},
        {country_name: 'Lebanon', code: 'LB'},
        {country_name: 'Lesotho', code: 'LS'},
        {country_name: 'Liberia', code: 'LR'},
        {country_name: 'Libyan Arab Jamahiriya', code: 'LY'},
        {country_name: 'Liechtenstein', code: 'LI'},
        {country_name: 'Lithuania', code: 'LT'},
        {country_name: 'Luxembourg', code: 'LU'},
        {country_name: 'Macao', code: 'MO'},
        {country_name: 'Macedonia, The Former Yugoslav Republic of', code: 'MK'},
        {country_name: 'Madagascar', code: 'MG'},
        {country_name: 'Malawi', code: 'MW'},
        {country_name: 'Malaysia', code: 'MY'},
        {country_name: 'Maldives', code: 'MV'},
        {country_name: 'Mali', code: 'ML'},
        {country_name: 'Malta', code: 'MT'},
        {country_name: 'Marshall Islands', code: 'MH'},
        {country_name: 'Martinique', code: 'MQ'},
        {country_name: 'Mauritania', code: 'MR'},
        {country_name: 'Mauritius', code: 'MU'},
        {country_name: 'Mayotte', code: 'YT'},
        {country_name: 'Mexico', code: 'MX'},
        {country_name: 'Micronesia, Federated States of', code: 'FM'},
        {country_name: 'Moldova, Republic of', code: 'MD'},
        {country_name: 'Monaco', code: 'MC'},
        {country_name: 'Mongolia', code: 'MN'},
        {country_name: 'Montserrat', code: 'MS'},
        {country_name: 'Morocco', code: 'MA'},
        {country_name: 'Mozambique', code: 'MZ'},
        {country_name: 'Myanmar', code: 'MM'},
        {country_name: 'Namibia', code: 'NA'},
        {country_name: 'Nauru', code: 'NR'},
        {country_name: 'Nepal', code: 'NP'},
        {country_name: 'Netherlands', code: 'NL'},
        {country_name: 'Netherlands Antilles', code: 'AN'},
        {country_name: 'New Caledonia', code: 'NC'},
        {country_name: 'New Zealand', code: 'NZ'},
        {country_name: 'Nicaragua', code: 'NI'},
        {country_name: 'Niger', code: 'NE'},
        {country_name: 'Nigeria', code: 'NG'},
        {country_name: 'Niue', code: 'NU'},
        {country_name: 'Norfolk Island', code: 'NF'},
        {country_name: 'Northern Mariana Islands', code: 'MP'},
        {country_name: 'Norway', code: 'NO'},
        {country_name: 'Oman', code: 'OM'},
        {country_name: 'Pakistan', code: 'PK'},
        {country_name: 'Palau', code: 'PW'},
        {country_name: 'Palestinian Territory, Occupied', code: 'PS'},
        {country_name: 'Panama', code: 'PA'},
        {country_name: 'Papua New Guinea', code: 'PG'},
        {country_name: 'Paraguay', code: 'PY'},
        {country_name: 'Peru', code: 'PE'},
        {country_name: 'Philippines', code: 'PH'},
        {country_name: 'Pitcairn', code: 'PN'},
        {country_name: 'Poland', code: 'PL'},
        {country_name: 'Portugal', code: 'PT'},
        {country_name: 'Puerto Rico', code: 'PR'},
        {country_name: 'Qatar', code: 'QA'},
        {country_name: 'Reunion', code: 'RE'},
        {country_name: 'Romania', code: 'RO'},
        {country_name: 'Russian Federation', code: 'RU'},
        {country_name: 'Rwanda', code: 'RW'},
        {country_name: 'Saint Helena', code: 'SH'},
        {country_name: 'Saint Kitts and Nevis', code: 'KN'},
        {country_name: 'Saint Lucia', code: 'LC'},
        {country_name: 'Saint Pierre and Miquelon', code: 'PM'},
        {country_name: 'Saint Vincent and the Grenadines', code: 'VC'},
        {country_name: 'Samoa', code: 'WS'},
        {country_name: 'San Marino', code: 'SM'},
        {country_name: 'Sao Tome and Principe', code: 'ST'},
        {country_name: 'Saudi Arabia', code: 'SA'},
        {country_name: 'Senegal', code: 'SN'},
        {country_name: 'Serbia and Montenegro', code: 'CS'},
        {country_name: 'Seychelles', code: 'SC'},
        {country_name: 'Sierra Leone', code: 'SL'},
        {country_name: 'Singapore', code: 'SG'},
        {country_name: 'Slovakia', code: 'SK'},
        {country_name: 'Slovenia', code: 'SI'},
        {country_name: 'Solomon Islands', code: 'SB'},
        {country_name: 'Somalia', code: 'SO'},
        {country_name: 'South Africa', code: 'ZA'},
        {country_name: 'South Georgia and the South Sandwich Islands', code: 'GS'},
        {country_name: 'Spain', code: 'ES'},
        {country_name: 'Sri Lanka', code: 'LK'},
        {country_name: 'Sudan', code: 'SD'},
        {country_name: 'Suriname', code: 'SR'},
        {country_name: 'Svalbard and Jan Mayen', code: 'SJ'},
        {country_name: 'Swaziland', code: 'SZ'},
        {country_name: 'Sweden', code: 'SE'},
        {country_name: 'Switzerland', code: 'CH'},
        {country_name: 'Syrian Arab Republic', code: 'SY'},
        {country_name: 'Taiwan, Province of China', code: 'TW'},
        {country_name: 'Tajikistan', code: 'TJ'},
        {country_name: 'Tanzania, United Republic of', code: 'TZ'},
        {country_name: 'Thailand', code: 'TH'},
        {country_name: 'Timor-Leste', code: 'TL'},
        {country_name: 'Togo', code: 'TG'},
        {country_name: 'Tokelau', code: 'TK'},
        {country_name: 'Tonga', code: 'TO'},
        {country_name: 'Trinidad and Tobago', code: 'TT'},
        {country_name: 'Tunisia', code: 'TN'},
        {country_name: 'Turkey', code: 'TR'},
        {country_name: 'Turkmenistan', code: 'TM'},
        {country_name: 'Turks and Caicos Islands', code: 'TC'},
        {country_name: 'Tuvalu', code: 'TV'},
        {country_name: 'Uganda', code: 'UG'},
        {country_name: 'Ukraine', code: 'UA'},
        {country_name: 'United Arab Emirates', code: 'AE'},
        {country_name: 'United Kingdom', code: 'GB'},
        {country_name: 'United States', code: 'US'},
        {country_name: 'United States Minor Outlying Islands', code: 'UM'},
        {country_name: 'Uruguay', code: 'UY'},
        {country_name: 'Uzbekistan', code: 'UZ'},
        {country_name: 'Vanuatu', code: 'VU'},
        {country_name: 'Venezuela', code: 'VE'},
        {country_name: 'Vietnam', code: 'VN'},
        {country_name: 'Virgin Islands, British', code: 'VG'},
        {country_name: 'Virgin Islands, U.S.', code: 'VI'},
        {country_name: 'Wallis and Futuna', code: 'WF'},
        {country_name: 'Western Sahara', code: 'EH'},
        {country_name: 'Yemen', code: 'YE'},
        {country_name: 'Zambia', code: 'ZM'},
        {country_name: 'Zimbabwe', code: 'ZW'}
        ];
});
