/**
 * groupdocs.annotation Plugin
 * Copyright (c) 2018 Aspose Pty Ltd
 * Licensed under MIT.
 * @author Aspose Pty Ltd
 * @version 1.0.0
 */

/*
******************************************************************
******************************************************************
GLOBAL VARIABLES
******************************************************************
******************************************************************
*/
var annotatedDocumentGuid = "";
var annotation = {
	id: "",
	type: "",
	left: 0,
	top: 0,
	width: 0,
	height: 0,
	pageNumber: 0,	
	documentType: "",
	svgPath: "",
	text: "",
	font: "Arial",
	fontSize: 10,
	comments: []
};
var annotationType = null;
var annotationsList = [];
var annotationsCounter = 0;
var rows = null;
var svgList = {};

$(document).ready(function(){

    /*
    ******************************************************************
    NAV BAR CONTROLS
    ******************************************************************
    */    
    
    //////////////////////////////////////////////////
    // Disable default download event
    //////////////////////////////////////////////////
    $('#gd-btn-download').off('click');	
	
	//////////////////////////////////////////////////
    // Add SVG to all pages DIVs
    //////////////////////////////////////////////////
    $.initialize(".gd-page-image", function () {
        // ensure that the closed comments tab doesn't 
        // have active class when another document is opened
        if ($(".gd-annotations-comments-wrapper").hasClass("active")) {
            $(".gd-annotations-comments-wrapper").toggleClass("active");
        }
	    // set text rows data to null
		rows = null;
		// append svg element to each page, this is required to draw svg based annotations
		$('div.gd-page').each(function(index, page){
			// initiate svg object
			if(svgList == null) {
				svgList	= {};
			}			
			if (page.id.indexOf("thumbnails") >= 0){
				return true;
			} else {
				if(!(page.id in svgList)){
					$(page).addClass("gd-disable-select");
					// add svg object to the list for further use
					var draw = SVG(page.id).size(page.offsetWidth, page.offsetHeight);
					svgList[page.id] = draw;
					draw = null;					
				} else {
					return true;
				}
			}			
		});
		//check if document contains annotations
		if ($(this).parent().parent().attr("id").search("thumbnails") == -1) {
			for(var i = 0; i < documentData.length; i++){
				if (documentData[i].annotations != null && documentData[i].annotations.length > 0){ 
					$.each(documentData[i].annotations, function(index, annotationData){
						if(annotationData != null && annotationData.pageNumber == documentData[i].number && annotationData.imported != true){
							importAnnotation(annotationData);
							annotationData.imported = true;
						}
					});
				}
			}
		}		
	});
	
	//////////////////////////////////////////////////
    // Open comment bar event
    //////////////////////////////////////////////////
	$(".gd-annotations-comments-toggle").on('click', function(){
		$(".gd-annotations-comments-wrapper").toggleClass("active");
	});
	
	
    //////////////////////////////////////////////////
    // Fix for tooltips of the dropdowns
    //////////////////////////////////////////////////
    $('#gd-download-val-container').on('click', function(e){
        if($(this).hasClass('open')){
            $('#gd-btn-download-value').parent().find('.gd-tooltip').css('display', 'none');
        }else{
            $('#gd-btn-download-value').parent().find('.gd-tooltip').css('display', 'initial');
        }
    });

	//////////////////////////////////////////////////
    // Open document event
    //////////////////////////////////////////////////
	$('.gd-modal-body').on('click', '.gd-filetree-name', function(e){
		// make annotations list empty for the new document
		annotationsList = [];
		svgList = null;
		$("#gd-annotation-comments").html("");	
		$('#gd-annotations-comments-toggle').prop('checked', false);
	});
	
	//////////////////////////////////////////////////
    // activate currently selected annotation tool
    //////////////////////////////////////////////////
    $('.gd-tools-container').on('click', function(e){	
		var currentlyActive = null;
		$(".gd-tool-field").each(function(index, tool){
			if($(tool).is( ".active" )) {				
				$(tool).removeClass("active");
				currentlyActive = $(tool)[0];
			}			
		});
		if(e.target != currentlyActive) {
			$(e.target).addClass("active");	
			annotationType = $(e.target).data("type");
		} else {
			annotationType = null;
		}				
    });
		
	//////////////////////////////////////////////////
    // add annotation event
    //////////////////////////////////////////////////
	var userClick = ('ontouchstart' in document.documentElement)  ? 'touchstart' : 'mousedown';
    $('#gd-panzoom').on(userClick, 'svg', function(e){
		if($(e.target).prop("tagName") == "IMG" || $(e.target).prop("tagName") == "svg"){
			// initiate annotation object if null
			if(annotation == null){
				annotation =  {
								id: "",
								type: "",
								left: 0,
								top: 0,
								width: 0,
								height: 0,
								pageNumber: 0,								
								documentType: "",
								svgPath: "",
								text: "",
								font: "Arial",
								fontSize: 10,
								comments: []
							};
			}
			// set annotation type
			annotation.type = annotationType;
			annotation.pageNumber = parseInt($($(e.target).parent()[0]).attr("id").replace ( /[^\d.]/g, '' ));
			// add annotation			
			switch (annotationType){
				case "text":
					++annotationsCounter;					
					getTextCoordinates(annotation.pageNumber, function(){	
						$.fn.drawTextAnnotation($(e.target).parent()[0], annotationsList, annotation, annotationsCounter, "text", e);							
						annotation = null;
					});					
					break;
				case "area":					
					++annotationsCounter;
					$.fn.drawTextAnnotation($(e.target).parent()[0], annotationsList, annotation, annotationsCounter, "area", e);							
					annotation = null;
					break;
				case "point":					
					++annotationsCounter;
					$.fn.drawSvgAnnotation($(e.target).parent()[0], "point");
					$.fn.drawSvgAnnotation.drawPoint(e);							
					annotation = null;
					break;
				case "textStrikeout":					
					++annotationsCounter;
					getTextCoordinates(annotation.pageNumber, function(){	
						$.fn.drawTextAnnotation($(e.target).parent()[0], annotationsList, annotation, annotationsCounter, "textStrikeout", e);							
						annotation = null;
					});
					break;
				case "polyline":					
					++annotationsCounter;
					$.fn.drawSvgAnnotation($(e.target).parent()[0], "polyline");
					$.fn.drawSvgAnnotation.drawPolyline(e);							
					annotation = null;
					break;
				case "textField":					
					++annotationsCounter;						
					$.fn.drawFieldAnnotation.drawTextField($(e.target).parent()[0], annotationsList, annotation, annotationsCounter, "textField", e);							
					annotation = null;	
					break;
				case "watermark":					
					++annotationsCounter;					
					$.fn.drawFieldAnnotation.drawTextField($(e.target).parent()[0], annotationsList, annotation, annotationsCounter, "watermark", e);							
					annotation = null;	
					break;
				case "textReplacement":					
					++annotationsCounter;
					getTextCoordinates(annotation.pageNumber, function(){	
						$.fn.drawTextAnnotation($(e.target).parent()[0], annotationsList, annotation, annotationsCounter, "textReplacement", e);							
						annotation = null;
					});
					break;
				case "arrow":					
					++annotationsCounter;
					$.fn.drawSvgAnnotation($(e.target).parent()[0], "arrow");
					$.fn.drawSvgAnnotation.drawArrow(e);							
					annotation = null;
					break;
				case "textRedaction":
					++annotationsCounter;
					getTextCoordinates(annotation.pageNumber, function(){	
						$.fn.drawTextAnnotation($(e.target).parent()[0], annotationsList, annotation, annotationsCounter, "textRedaction", e);							
						annotation = null;
					});
					break;
				case "resourcesRedaction":					
					++annotationsCounter;
					$.fn.drawTextAnnotation($(e.target).parent()[0], annotationsList, annotation, annotationsCounter, "resourcesRedaction", e);							
					annotation = null;
					break;
				case "textUnderline":					
					++annotationsCounter;
					getTextCoordinates(annotation.pageNumber, function(){	
						$.fn.drawTextAnnotation($(e.target).parent()[0], annotationsList, annotation, annotationsCounter, "textUnderline", e);							
						annotation = null;
					});
					break;
				case "distance":					
					++annotationsCounter;
					$.fn.drawSvgAnnotation($(e.target).parent()[0], "distance");
					$.fn.drawSvgAnnotation.drawDistance(e);							
					annotation = null;
					break;
			}				
			// enable save button on the dashboard
			if($("#gd-nav-save").hasClass("gd-save-disabled")) {
				$("#gd-nav-save").removeClass("gd-save-disabled");
				$("#gd-nav-save").on('click', function(){
					annotate();
				});
			}
		}
    });
	
	//////////////////////////////////////////////////
    // enter comment text event
    //////////////////////////////////////////////////
    $('.gd-comments-sidebar-expanded').on('click', 'div.gd-comment-text', function(e){
		$(e.target).parent().parent().parent().find(".gd-comment-time").last().html(new Date($.now()).toUTCString());
		$("#gd-save-comments").removeClass("gd-save-button-disabled");		
	});
	
	//////////////////////////////////////////////////
    // save comment event
    //////////////////////////////////////////////////
	$('.gd-comments-sidebar-expanded').on('click', '#gd-save-comments', saveComment);
	
	//////////////////////////////////////////////////
    // reply comment event
    //////////////////////////////////////////////////
	$('.gd-comments-sidebar-expanded').on('click', '.gd-comment-reply', function(e){
		$(".gd-comment-reply").before(getCommentHtml);		
	});
	
	//////////////////////////////////////////////////
    // cancel comment event
    //////////////////////////////////////////////////
	$('.gd-comments-sidebar-expanded').on('click', '.gd-comment-cancel', function(e){
		$(".gd-comment-box-sidebar").find(".gd-annotation-comment").last().find(".gd-comment-text").html("");		
	});
	
	//////////////////////////////////////////////////
    // delete comment event
    //////////////////////////////////////////////////
	$('.gd-comments-sidebar-expanded').on('click', '.gd-delete-comment', function(e){
		// check if there is no more comments for the annotation
		if($(".gd-comment-box-sidebar").find(".gd-annotation-comment").length == 1){
			// delete annotation
			deleteAnnotation(e);
			$("#gd-annotation-comments").html("");
		} else {
			// delete comment
			var annotationId = $(e.target).parent().parent().parent().parent().parent().data("annotationId");
			for(var i = 0; i < annotationsList.length; i++){
				if(annotationId = annotationsList[i].id){
					var text = $(e.target).parent().parent().parent().parent().find(".gd-comment-text").html();					
					annotationsList[i].comments = $.grep(annotationsList[i].comments, function(value) {
																return value.text != text;
															});					
				}
			}
			$(e.target).parent().parent().parent().parent().remove();
		}
	});	
	
	//////////////////////////////////////////////////
    // annotation click event
    //////////////////////////////////////////////////
    $('#gd-panzoom').on('click', '.gd-annotation', function(e){
		if(!$(".gd-annotations-comments-wrapper").hasClass("active")){
			$(".gd-annotations-comments-wrapper").toggleClass("active");
		}
		if(e.target.tagName != "I" && e.target.tagName != "INPUT" && e.target.tagName != "TEXTAREA"){
			$("#gd-annotation-comments").html("");
			$('#gd-annotations-comments-toggle').prop('checked', true);
			var annotationId = null;
			if (typeof $(e.target).attr("id") != "undefined"){
				// get cuurent annotation id
				annotationId = parseInt($(e.target).attr("id").replace( /[^\d.]/g, '' ));			
				// get and append all comments for the annotation
				$("#gd-annotation-comments").append(getCommentBaseHtml);
				$(".gd-comment-box-sidebar").data("annotationId", annotationId);
				for(var i = 0; i < annotationsList.length; i++){
					if(annotationsList[i].id == annotationId){
						if(annotationsList[i].comments != null && annotationsList[i].comments.length != 0 ){
							for(var n = 0; n < annotationsList[i].comments.length; n++){
								$(".gd-comment-reply").before(getCommentHtml);
								$(".gd-comment-time").last().html(annotationsList[i].comments[n].time);
								$(".gd-comment-text").last().html(annotationsList[i].comments[n].text);
								$(".gd-comment-text").data("saved", true);						 
								$(".gd-comment-user-name").last().val(annotationsList[i].comments[n].userName);
							}
						} else {
							$(".gd-comment-reply").before(getCommentHtml);
						}
						return;
					} else {
						continue;
					}
				}
			}
		} else {
			return;
		}
	});
	
    //////////////////////////////////////////////////
    // Download event
    //////////////////////////////////////////////////
    $('#gd-btn-download-value > li').on('click', function(e){
        download($(this));
    });
});

/*
******************************************************************
FUNCTIONS
******************************************************************
*/

/**
 * Get current mouse coordinates
 * @param {Object} event - click event
 */
function getMousePosition(event) {
	var mouse = {
		x: 0,
		y: 0
	};
	var ev = event || window.event; //Moz || IE
	if (ev.pageX || ev.touches[0].pageX) { //Moz
		mouse.x = (typeof ev.pageX != "undefined") ? ev.pageX : ev.touches[0].pageX;
		mouse.y = (typeof ev.pageY != "undefined") ? ev.pageY : ev.touches[0].pageY;
	} else if (ev.clientX) { //IE
		mouse.x = ev.clientX + document.body.scrollLeft;
		mouse.y = ev.clientY + document.body.scrollTop;
	}
	return mouse;
}
		
/**
 * Get current page text coordinates
 * @param {int} pageNumber - the page number of which you need information
 */
function getTextCoordinates(pageNumber, callback) {  
    var url = getApplicationPath('textCoordinates');      
    // current document guid is taken from the viewer.js globals
    var data = {
        guid: documentGuid,
        password: password,   
		pageNumber: pageNumber
    };
    // get text data for the document page
    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(data),
        contentType: 'application/json',
        success: function(returnedData) {
            $('#gd-modal-spinner').hide();
            var result = "";
            if(returnedData.message != undefined){                
                if(returnedData.message.toLowerCase().indexOf("password") != -1){                  
                    $("#gd-password-required").html(returnedData.message);
                    $("#gd-password-required").show();
                } else {
                    // open error popup
                    printMessage(returnedData.message);
                }
                return;
            }			
			// set rows data
            rows = returnedData; 
			rows.sort(function(row1, row2) {
				// Ascending: first row top less than the previous
				return row1.lineTop - row2.lineTop;
			});
			$.each(rows, function(index, row){
				row.textCoordinates.sort(function(row1, row2) {
					// Ascending: first row top less than the previous
					return row1 - row2;
				});
			});
        },
        error: function(xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            console.log(err.Message);
			// open error popup
			printMessage(err.message);
        }
    }).done(function(){
        if(typeof callback == "function") {
            callback();
        }
    });
}

/**
 * Compare and set text annotation position according to the text position
  * @param {int} mouseX - current mouse X position
  * @param {int} mouseY - current mouse Y position
 */
function setTextAnnotationCoordinates(mouseX, mouseY) { 
	var correctCoordinates = {
		x: 0, 
		y: 0,
		height: 0
	};
	// check if the mouse position is less or bigger than the first or last text row
	if(mouseY < rows[0].lineTop) {
		mouseY = rows[0].lineTop;
	} else if (mouseY > rows[rows.length - 1].lineTop){
		mouseY = rows[rows.length - 1].lineTop;
	}
	// get most suitable row (vertical position)
	for(var i = 0; i < rows.length; i++){		
		if(mouseY >= rows[i].lineTop && mouseY <= rows[i + 1].lineTop){
			// set row top position and height
			correctCoordinates.y = rows[i].lineTop;
			correctCoordinates.height = rows[i].lineHeight;
			// get most suitable symbol coordinates in the row (horizontal position)
			for(var n = 0; n < rows[i].textCoordinates.length; n++){
				if(mouseX >= rows[i].textCoordinates[n] && mouseX < rows[i].textCoordinates[n + 1]){
					correctCoordinates.x = rows[i].textCoordinates[n];
					break;
				} else {
					continue;
				}
			}
			break;
		} else {
			continue
		}
	}
	return correctCoordinates;
}


/**
 * Annotate current document
 */
function annotate() {   
	// set current document guid - used to check if the other document were opened   
    var url = getApplicationPath('annotate');     
	annotationsList[0].documentType = getDocumentFormat(documentGuid).format;	
    // current document guid is taken from the viewer.js globals
    var data = {
        guid: documentGuid.replace(/\\/g, "//"),
        password: password,
		htmlMode: false,
        annotationsData: annotationsList
    };
    // annotate the document
    $.ajax({
        type: 'POST',
        url: url,
        data: JSON.stringify(data),
        contentType: 'application/json',
        success: function(returnedData) {
            $('#gd-modal-spinner').hide();
            var result = "";
            if(returnedData.message != undefined){
                // if password for document is incorrect return to previouse step and show error
                if(returnedData.message.toLowerCase().indexOf("password") != -1){                  
                    $("#gd-password-required").html(returnedData.message);
                    $("#gd-password-required").show();
                } else {
                    // open error popup
                    printMessage(returnedData.message);
                }
                return;
            }			
            annotatedDocumentGuid = returnedData.guid;  
			result = '<div id="gd-modal-annotated">Document annotated successfully</div>';
			toggleModalDialog(true, 'Annotation', result);
        },
        error: function(xhr, status, error) {
			var err = eval("(" + xhr.responseText + ")");
			console.log(err.Message);
			// open error popup
            printMessage(err.message);
		}
    });
}

/**
 * delete annotation
 * @param {Object} event - delete annotation button click event data
 */
function deleteAnnotation(event){
	// get annotation id
	var annotationId = $(event.target).parent().parent().parent().parent().parent().data("annotationId");
	// get annotation data to delete
	var annotationToRemove = $.grep(annotationsList, function(obj){return obj.id === annotationId;})[0];	
	// delete annotation from the annotations list
	annotationsList.splice($.inArray(annotationToRemove, annotationsList),1);
	// delete annotation from the page
	$(".gd-annotation").each(function(index, element){
		var id = null;
		if($(element).hasClass("svg")){
			id = parseInt($(element).attr("id").replace( /[^\d.]/g, '' ));			
		} else {
			id = parseInt($(element).find(".annotation").attr("id").replace( /[^\d.]/g, '' ));
		}
		if(id == annotationId){			
			if(typeof $(element).attr("id") != "undefined" && $(element).attr("id").search("distance") != -1){
				// remove text element for distance annotation
				$.each($(element).parent().find("text"), function(index, text){
					if($(text).data("id") == id){
						$(text).remove();
					}
				});
			}
			$(element).remove();
		} else {
			return true;
		}
	});
	$("#gd-save-comments").addClass("gd-save-button-disabled");	
}

/**
 * Save comment to the annotation
 */
function saveComment(){
	$(".gd-annotation-comment").each(function(index, currentComment){
		// set saved flag
		if(!$(currentComment).find(".gd-comment-text").data("saved")){
			$(currentComment).find(".gd-comment-text").data("saved", true);
			var annotationId = $(currentComment).parent().data("annotationId");	
			// get current annotation from the annotations list
			var annotationToAddComments = $.grep(annotationsList, function(obj){return obj.id === annotationId;})[0];			
			// initiate comment object
			var comment = {
				time: null,
				text: "",
				userName: ""
			};			
			// set comment data
			comment.time = $(currentComment).find(".gd-comment-time").html();
			comment.text = $(currentComment).find(".gd-comment-text").html();
			comment.userName = $(currentComment).find(".gd-comment-user-name").val() == "" ? "Anonym A." : $(currentComment).find(".gd-comment-user-name").val();
			// check if the same comment is already added - used for import annotations
			var existedComment = $.grep(annotationToAddComments.comments, function(e){ return e.text.trim() == comment.text.trim(); });
			// add comment
			if(existedComment.length == 0){				
				annotationToAddComments.comments.push(comment);
			}				
			annotationToAddComments = null;
			comment = null;					
		} else {			
			return true;
		}
	});	
}

/**
* Add comment into the comments bar
* @param {Object} currentAnnotation - currently added annotation
*/
function addComment(currentAnnotation){
    $("#gd-annotation-comments").html("");
	// check if annotation contains comments
	if(currentAnnotation.comments != null && currentAnnotation.comments.length > 0){		
		$.each(currentAnnotation.comments, function(index, comment){
			if (index == 0){
				$("#gd-annotation-comments").append(getCommentBaseHtml);
			}
			$(".gd-comment-box-sidebar").data("annotationId", currentAnnotation.id);
			$(".gd-comment-reply").before(getCommentHtml(comment));
		});		
	} else {	
	    $('#gd-annotations-comments-toggle').prop('checked', true);
	    if (!$(".gd-annotations-comments-wrapper").hasClass("active")) {
	        $(".gd-annotations-comments-wrapper").toggleClass("active");
	    }
		currentAnnotation.comments = [];
		$("#gd-annotation-comments").append(getCommentBaseHtml);
		$(".gd-comment-box-sidebar").data("annotationId", currentAnnotation.id);
		$(".gd-comment-reply").before(getCommentHtml);
	}
}
 
/**
 * Make current annotation draggble and resizable
 * @param {Object} currentAnnotation - currently added annotation
 */
function makeResizable (currentAnnotation){	
	var annotationType = currentAnnotation.type;	
	$(".gd-annotation").each(function(imdex, element){
		if(!$(element).hasClass("svg")){
			if(parseInt($(element).find(".annotation").attr("id").replace ( /[^\d.]/g, '' )) == currentAnnotation.id){
				// enable dragging and resizing features for current image
				$(element).draggable({
					// set restriction for image dragging area to current document page
					containment: "#gd-page-" + currentAnnotation.pageNumber,	
					stop: function(event, image){			
						if(annotationType == "text" || annotationType == "textStrikeout"){
							var coordinates = setTextAnnotationCoordinates(image.position.left, image.position.top)
							currentAnnotation.left = coordinates.x;
							currentAnnotation.top = coordinates.y;					
						} else {
							currentAnnotation.left = image.position.left;
							currentAnnotation.top = image.position.top;					
						}
					},		
				}).resizable({
					// set restriction for image resizing to current document page
					containment: "#gd-page-" + currentAnnotation.pageNumber,				
					stop: function(event, image){
						currentAnnotation.width = image.size.width;
						currentAnnotation.height = image.size.height;
						currentAnnotation.left = image.position.left;
						currentAnnotation.top = image.position.top;
					},
					// set image resize handles
					handles: {						
						'ne': '.ui-resizable-ne',
						'se': '.ui-resizable-se',
						'sw': '.ui-resizable-sw',
						'nw': '.ui-resizable-nw'					
					},
					grid: [10, 10],				
					resize: function (event, image) {
						$(event.target).find(".gd-" + annotationType + "-annotation").css("width", image.size.width);
						$(event.target).find(".gd-" + annotationType + "-annotation").css("height", image.size.height);	
						$(event.target).find(".gd-" + annotationType + "-annotation").css("left", image.position.left);
						$(event.target).find(".gd-" + annotationType + "-annotation").css("top", image.position.top);					
					}				
				});		
			}
		} else {
			return true;
		}
	});		
}

/**
 * Import already existed annotations
 * @param {Object} annotationData - existed annotation
 */
function importAnnotation(annotationData){
	annotation = annotationData;
	// draw imported annotation over the document page
	switch (annotation.type){
			case "text":
				++annotationsCounter;					
				$.fn.importTextAnnotation($("#gd-page-" + annotationData.pageNumber)[0], annotationsList, annotation, annotationsCounter, "text");		
				annotation = null;
				break;
			case "area":					
				++annotationsCounter;
				$.fn.importTextAnnotation($("#gd-page-" + annotationData.pageNumber)[0], annotationsList, annotation, annotationsCounter, "area");							
				annotation = null;
				break;
			case "point":					
				++annotationsCounter;
				$.fn.drawSvgAnnotation($("#gd-page-" + annotationData.pageNumber)[0], "point");
				$.fn.drawSvgAnnotation.importPoint(annotation);							
				annotation = null;
				break;
			case "textStrikeout":					
				++annotationsCounter;				
				$.fn.importTextAnnotation($("#gd-page-" + annotationData.pageNumber)[0], annotationsList, annotation, annotationsCounter, "textStrikeout");							
				annotation = null;				
				break;
			case "polyline":					
				++annotationsCounter;
				$.fn.drawSvgAnnotation($("#gd-page-" + annotationData.pageNumber)[0], "polyline");
				$.fn.drawSvgAnnotation.importPolyline(annotation);							
				annotation = null;
				break;
			case "textField":					
				++annotationsCounter;						
				$.fn.drawFieldAnnotation.importTextField($("#gd-page-" + annotationData.pageNumber)[0], annotationsList, annotation, annotationsCounter, "textField");							
				annotation = null;	
				break;
			case "watermark":					
				++annotationsCounter;					
				$.fn.drawFieldAnnotation.importTextField($("#gd-page-" + annotationData.pageNumber)[0], annotationsList, annotation, annotationsCounter, "watermark");							
				annotation = null;	
				break;
			case "textReplacement":					
				++annotationsCounter;					
				$.fn.importTextAnnotation($("#gd-page-" + annotationData.pageNumber)[0], annotationsList, annotation, annotationsCounter, "textReplacement");							
				annotation = null;				
				break;
			case "arrow":					
				++annotationsCounter;
				$.fn.drawSvgAnnotation($("#gd-page-" + annotationData.pageNumber)[0], "arrow");
				$.fn.drawSvgAnnotation.importArrow(annotation);							
				annotation = null;
				break;
			case "textRedaction":
				++annotationsCounter;				
				$.fn.importTextAnnotation($("#gd-page-" + annotationData.pageNumber)[0], annotationsList, annotation, annotationsCounter, "textRedaction");							
				annotation = null;				
				break;
			case "resourcesRedaction":					
				++annotationsCounter;
				$.fn.importTextAnnotation($("#gd-page-" + annotationData.pageNumber)[0], annotationsList, annotation, annotationsCounter, "resourcesRedaction");							
				annotation = null;
				break;
			case "textUnderline":					
				++annotationsCounter;				
				$.fn.importTextAnnotation($("#gd-page-" + annotationData.pageNumber)[0], annotationsList, annotation, annotationsCounter, "textUnderline");							
				annotation = null;			
				break;
			case "distance":					
				++annotationsCounter;
				$.fn.drawSvgAnnotation($("#gd-page-" + annotationData.pageNumber)[0], "distance");
				$.fn.drawSvgAnnotation.importDistance(annotation);							
				annotation = null;
				break;
		}		
		// enable save button on the dashboard
		if($("#gd-nav-save").hasClass("gd-save-disabled")) {
			$("#gd-nav-save").removeClass("gd-save-disabled");
			$("#gd-nav-save").on('click', function(){
				annotate();
			});
		}		
}

/**
 * Get HTML of the resize handles - used to add resize handles to the added annotation
 */
function getHtmlResizeHandles(){
    return  '<div class="ui-resizable-handle ui-resizable-ne"></div>'+
        '<div class="ui-resizable-handle ui-resizable-se"></div>'+
        '<div class="ui-resizable-handle ui-resizable-sw"></div>'+
        '<div class="ui-resizable-handle ui-resizable-nw"></div>';
}

function getCommentHtml(comment){
		var time = (typeof comment.time != "undefined") ? comment.time : "";
		var text = (typeof comment.text != "undefined") ? comment.text : "";		
		var userName = (typeof comment.userName != "undefined") ? comment.userName : "";
		return 	'<div class="gd-annotation-comment">'+
					'<div class="gd-comment-avatar">'+
						'<span class="gd-blanc-avatar-icon">'+
							'<i class="fa fa-commenting-o fa-flip-horizontal" aria-hidden="true"></i>'+
							'<p class="gd-comment-time">'+
								time +
							'</p>'+
							'<div class="gd-delete-comment">'+
								'<i class="fa fa-trash-o" aria-hidden="true"></i>'+
							'</div>'+
						'</span>'+
					'</div>	'+
					'<div class="gd-comment-text-wrapper mousetrap">'+
						'<span class="comment-box-pointer"></span>'+
						'<div class="gd-comment-text mousetrap" contenteditable="true" data-saved="false">' + text + '</div>'+
						'<input type="text" placeholder="User name" class="gd-comment-user-name" value="' + userName + '">'+
					'</div>'+
				'</div>';
}

/**
 * Get HTML markup of the comment
 */
function getCommentBaseHtml(){
	return '<div class="gd-comment-box-sidebar" data-annotationId="0">'+
				// comments will be here
				'<a class="gd-save-button gd-comment-reply" href="#">reply</a>'+
				'<a class="gd-save-button gd-comment-cancel" href="#">cancel</a>'+
			'</div>';
}

/**
 * Download document
 * @param {Object} button - Clicked download button
 */
function download (button){
    var annotated = false;  
	var documentName = documentGuid.match(/[-_\w]+[.][\w]+$/i)[0];
    if($(button).attr("id") == "gd-annotated-download"){
        annotated = true;       
    } 
    if(typeof documentName != "undefined" && documentName != ""){
         // Open download dialog
         window.location.assign(getApplicationPath("downloadDocument/?path=") + documentName + "&annotated=" + annotated);
    } else {
         // open error popup
         printMessage("Please open document first");
    }
}

/*
******************************************************************
******************************************************************
GROUPDOCS.ANNOTATION PLUGIN
******************************************************************
******************************************************************
*/
(function( $ ) {
    /*
    ******************************************************************
    STATIC VALUES
    ******************************************************************
    */
    var gd_navbar = '#gd-navbar';

    /*
    ******************************************************************
    METHODS
    ******************************************************************
    */
    var methods = {
        init : function( options ) {
            // set defaults
            var defaults = {
                textAnnotation: true,
                areaAnnotation: true,
                pointAnnotation: true,
                textStrikeoutAnnotation: true,
                polylineAnnotation: true,
                textFieldAnnotation: true,
                watermarkAnnotation: true,
                textReplacementAnnotation: true,
                arrowAnnotation: true,
                textRedactionAnnotation: true,
                resourcesRedactionAnnotation: true,
                textUnderlineAnnotation: true,
                distanceAnnotation: true,
                downloadOriginal:  true,
                downloadAnnotated: true
            };

            options = $.extend(defaults, options);
			 
			getHtmlDownloadPanel();
			$('#gd-navbar').append(getHtmlSavePanel);
			// assembly annotation tools side bar html base
			$(".wrapper").append(getHtmlAnnotationsBarBase);
			// assembly annotation comments side bar html base
			$(".wrapper").append(getHtmlAnnotationCommentsBase);
			
			// assembly annotations tools side bar
			if(options.textAnnotation){
                $("#gd-annotations-tools").append(getHtmlTextAnnotationElement);
            }
			
			if(options.areaAnnotation){
                $("#gd-annotations-tools").append(getHtmlAreaAnnotationElement);
            }
			
			if(options.pointAnnotation){
                $("#gd-annotations-tools").append(getHtmlPointAnnotationElement);
            }
			
			if(options.textStrikeoutAnnotation){
                $("#gd-annotations-tools").append(getHtmlTextStrikeoutAnnotationElement);
            }
			
			if(options.polylineAnnotation){
                $("#gd-annotations-tools").append(getHtmlPolylineAnnotationElement);
            }
			
			if(options.textFieldAnnotation){
                $("#gd-annotations-tools").append(getHtmlTextFieldAnnotationElement);
            }
			
			if(options.watermarkAnnotation){
                $("#gd-annotations-tools").append(getHtmlWatermarkdAnnotationElement);
            }
			
			if(options.textReplacementAnnotation){
                $("#gd-annotations-tools").append(getHtmlTextReplacementAnnotationElement);
            }
			
			if(options.arrowAnnotation){
                $("#gd-annotations-tools").append(getHtmlArrowAnnotationElement);
            }
			
			if(options.textRedactionAnnotation){
                $("#gd-annotations-tools").append(getHtmlTextRedactionAnnotationElement);
            }
			
			if(options.resourcesRedactionAnnotation){
                $("#gd-annotations-tools").append(getHtmlResourcesRedactionAnnotationElement);
            }
			
			if(options.textUnderlineAnnotation){
                $("#gd-annotations-tools").append(getHtmlTextUnderlineAnnotationElement);
            }
			
			if(options.distanceAnnotation){
                $("#gd-annotations-tools").append(getHtmlDistanceAnnotationElement);
            }
			
			// assembly nav bar
			if(options.downloadOriginal){
                $("#gd-btn-download-value").append(getHtmlDownloadOriginalElement());
            }

            if(options.downloadAnnotated){
                $("#gd-btn-download-value").append(getHtmlDownloadAnnotatedElement());
            }
        }
    };

    /*
    ******************************************************************
    INIT PLUGIN
    ******************************************************************
    */
    $.fn.annotation = function( method ) {
        if ( methods[method] ) {
            return methods[method].apply( this, Array.prototype.slice.call( arguments, 1 ));
        } else if ( typeof method === 'object' || ! method ) {
            return methods.init.apply( this, arguments );
        } else {
            $.error( 'Method' +  method + ' does not exist on jQuery.annotation' );
        }
    };

    /*
    ******************************************************************
    HTML MARKUP
    ******************************************************************
    */
	function getHtmlAnnotationsBarBase(){
		return '<div class=gd-annotations-bar-wrapper>'+
					// open/close trigger button BEGIN
					'<input id="gd-annotations-toggle" class="gd-annotations-toggle" type="checkbox" />'+
					'<label for="gd-annotations-toggle" class="gd-lbl-toggle"></label>'+
					// open/close trigger button END
					// annotations tools
					'<div class="gd-tools-container gd-embed-annotation-tools gd-ui-draggable">'+
						// annotations tools list BEGIN
						'<ul class="gd-tools-list" id="gd-annotations-tools">'+
							// annotation tools will be here
						'</ul>'+
						// annotations tools list END
					'</div>'+
				'</div>';
	}

	function getHtmlAnnotationCommentsBase(){
		return '<div class=gd-annotations-comments-wrapper>'+
					// open/close trigger button BEGIN
					'<input id="gd-annotations-comments-toggle" class="gd-annotations-comments-toggle" type="checkbox" />'+
					'<label for="gd-annotations-comments-toggle" class="gd-lbl-comments-toggle"></label>'+
					// open/close trigger button END
					'<div class="gd-comments-sidebar-expanded gd-ui-tabs gd-ui-widget gd-ui-widget-content gd-ui-corner-all">'+						
						'<div id="gd-tab-comments" class="gd-comments-content">'+							
							'<div class="gd-viewport">'+
								'<h3 class="gd-com-heading gd-colon">Comments for annotations:</h3>'+
								'<div class="gd-overview" id="gd-annotation-comments">'+									
									// annotation comments will be here
								'</div>'+
							'</div>'+							
							'<a  id="gd-save-comments" class="gd-save-button gd-save-button-disabled" href="#">save</a>'+
						'</div>'+
					'</div>'+
				'</div>';
	}
	
	function getHtmlTextAnnotationElement(){
		return 	'<li>'+     
					'<button class="gd-tool-field gd-text-box" data-type="text">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">text</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlAreaAnnotationElement(){
		return '<li>'+
					'<button class="gd-tool-field gd-area-box" data-type="area">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">area</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlPointAnnotationElement(){
		return '<li>'+
					'<button class="gd-tool-field gd-point-box" data-type="point">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">point</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlTextStrikeoutAnnotationElement(){
		return '<li>'+     
					'<button class="gd-tool-field gd-strike-box" data-type="textStrikeout">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">strikeout</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlPolylineAnnotationElement(){
		return '<li>'+
					'<button class="gd-tool-field gd-polyline-box" data-type="polyline">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">polyline</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlTextFieldAnnotationElement(){
		return '<li>'+
					'<button class="gd-tool-field gd-highlight-box" data-type="textField">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">text field</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlWatermarkdAnnotationElement(){
		return '<li>'+
					'<button class="gd-tool-field gd-watermark-box" data-type="watermark">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">watermark</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlTextReplacementAnnotationElement(){
		return '<li>'+
					'<button class="gd-tool-field gd-replace-box" data-type="textReplacement">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">text replacement</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlArrowAnnotationElement(){
		return '<li>'+
					'<button class="gd-tool-field gd-arrow-tool" data-type="arrow">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">arrow</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlTextRedactionAnnotationElement(){
		return '<li>'+ 
					'<button class="gd-tool-field gd-redtext-box" data-type="textRedaction">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">text redaction</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlResourcesRedactionAnnotationElement(){
		return '<li>'+
					'<button class="gd-tool-field gd-redarea-box" data-type="resourcesRedaction">'+ 
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">resource redaction</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlTextUnderlineAnnotationElement(){
		return '<li>'+
					'<button class="gd-tool-field gd-underline-tool" data-type="textUnderline">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">underline text</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlDistanceAnnotationElement(){
		return '<li>'+
					'<button class="gd-tool-field gd-ruler-tool" data-type="distance">'+
						'<span class="gd-popupdiv-hover gd-tool-field-tooltip">distance</span>'+
					'</button>'+
				'</li>';
	}
	
	function getHtmlSavePanel(){
        return '<li id="gd-nav-save" class="gd-save-disabled"><i class="fa fa-floppy-o"></i><span class="gd-tooltip">Save</span></li>';
    }
	
	function getHtmlDownloadPanel(){
        var downloadBtn = $("#gd-btn-download");
        var defaultHtml = downloadBtn.html();
        var downloadDropDown = '<li class="gd-nav-toggle" id="gd-download-val-container">'+
									'<span id="gd-download-value">' +
										'<i class="fa fa-download"></i>' +
										'<span class="gd-tooltip">Download</span>' +
									'</span>'+
									'<span class="gd-nav-caret"></span>'+
									'<ul class="gd-nav-dropdown-menu gd-nav-dropdown" id="gd-btn-download-value">'+
										// download types will be here
									'</ul>'+
								'</li>';
        downloadBtn.html(downloadDropDown);
    }
	
	function getHtmlDownloadOriginalElement(){
        return '<li id="gd-original-download">Download Original</li>';
    }

    function getHtmlDownloadAnnotatedElement(){
        return '<li id="gd-annotated-download">Download Annotated</li>';
    }
	
})(jQuery);