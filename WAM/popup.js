

 var console=chrome.extension.getBackgroundPage().console;
 
 var app= {
	 
	 init: function(){
		 
		// var $title=document.getElementById("title");
		// var $urls=document.getElementById("urls");
		 var $titleInput=document.getElementById("sendinfo");
		 
		 $titleInput.addEventListener("click",function(){
			// console.log($title.value);
			 var aTabs = [];
			 chrome.tabs.getAllInWindow(null, function(tabs) {
	            tabs.forEach(function(tab){
					aTabs.push({title:tab.title, url:tab.url});
                  //  taburl=tab.url;	
					//tabtitle=tab.title;
					//console.log("Title is:",tabtitle);
	                //console.log("Url is:",taburl);
						
                });
				console.log(aTabs);
				
				$.ajax({
				  type: "POST",
				  url:"http://127.0.0.1:8080/tabs/",
				  data: aTabs,
				  success: success,
				  dataType: "application/json"
				  
				});
				
				$.post("http://127.0.0.1:8080/tabs/", function( data ) {
                $( ".aTabs" ).html( data );
                });
				
            });	
		 });
	 }
 };
 
 document.addEventListener("DOMContentLoaded", function(){
	 app.init();
	 
 });
 



 