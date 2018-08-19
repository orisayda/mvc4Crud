'use strict';

angular.module('myFactory', [])
	.service('myFactory',function () {

		var varTitle = 'Change Title Dynamically Demo';
        this.getTitle = function () {
            return varTitle;
        };

        console.log("start");

        
        this.setTitle = function (tit) {
            varTitle = tit;
        };


	});

