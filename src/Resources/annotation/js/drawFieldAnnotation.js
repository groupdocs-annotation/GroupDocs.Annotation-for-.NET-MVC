/**
 * groupdocs.annotation Plugin
 * Copyright (c) 2018 Aspose Pty Ltd
 * Licensed under MIT.
 * @author Aspose Pty Ltd
 * @version 1.0.0
 */

 $(document).ready(function(){
	
	var userMouseClick = ('ontouch' in document.documentElement)  ? 'touch' : 'click';
	var userMouseUp = ('ontouchend' in document.documentElement)  ? 'touchend' : 'mouseup';
	//////////////////////////////////////////////////
    // delete text field click event
    //////////////////////////////////////////////////  
	$('#gd-panzoom').on(userMouseClick, '.gd-text-field-delete', function(e){
		var id = parseInt($(this.parentElement.parentElement).find(".annotation").attr("id").replace ( /[^\d.]/g, '' ));
		var annotationToRemove = $.grep(annotationsList, function(obj){return obj.id === id;})[0];	
		annotationsList.splice($.inArray(annotationToRemove, annotationsList),1);
		$(".gd-annotation").each(function(index, element){
			if(!$(element).hasClass("svg")){
				if(parseInt($(element).find(".annotation").attr("id").replace( /[^\d.]/g, '' )) == id){
					$(element).remove();
				} else {
					return true;
				}
			}
		});	
	});

	//////////////////////////////////////////////////
    // change font family or font size event
    //////////////////////////////////////////////////  
	$("#gd-panzoom").on("input", ".gd-typewriter-font, .gd-typewriter-font-size", function(e){
		var textArea = $(this.parentElement.parentElement).find("textarea")[0];
		var id = parseInt($(this.parentElement.parentElement).find(".annotation").attr("id").replace ( /[^\d.]/g, '' ));
		var font = "Arial";
		var fontSize = 10;
		var currentAnnotation = $.grep(annotationsList, function(e){ return e.id == id; });
		if($(this).attr("class") == "gd-typewriter-font"){
			currentAnnotation[0].font = this.value;
			$(textArea).css("font-family", currentAnnotation[0].font);
			
		} else {
			currentAnnotation[0].fontSize = parseInt(this.value);
			$(textArea).css("font-size", currentAnnotation[0].fontSize);
		}
	});	

	//////////////////////////////////////////////////
    // resize text area event
    //////////////////////////////////////////////////  
	$("#gd-panzoom").bind(userMouseUp, ".gd-typewriter-text",  function(event){
		var id = $(event.target).data("id");
		$.each(annotationsList, function(index, elem){
			if(elem.id == id){
				elem.width = $(event.target).width();
				elem.height = $(event.target).height();			
			} else {
				return true;
			}
		});			
	});

	//////////////////////////////////////////////////
    // enter text event
    //////////////////////////////////////////////////  
	$("#gd-panzoom").bind("keyup", ".gd-typewriter-text",  function(event){
		var id = $(event.target).data("id");
		$.each(annotationsList, function(index, elem){
			if(elem.id == id){
				elem.text = $(event.target).val();					
			} else {
				return true;
			}
		});			
	});
 });
 
(function( $ ) {

	/**
	* Create private variables.
	**/
	var mouse = {
        x: 0,
        y: 0       
    };
	var zoomCorrection = {
		x: 0,
		y: 0
	};
	var startX = 0;
	var startY = 0;
    var element = null;
	var annotationInnerHtml = null;
	var currentPrefix = "";
	var idNumber = null;
	var zoomLevel = 1;
	var canvas = null;

	/**
	 * Draw field annotation	
	 */
	$.fn.drawFieldAnnotation = function(documentPage) {
		canvas = documentPage;
		zoomLevel = (typeof $(canvas).css("zoom") == "undefined") ? 1 : $(canvas).css("zoom");
	}
	
	/**
	* Extend plugin
	**/
	$.extend(true, $.fn.drawFieldAnnotation, {
		
		/**
		 * Draw text field annotation
		 * @param {Array} annotationsList - List of all annotations
		 * @param {Object} annotation - Current annotation
		 * @param {int} annotationsCounter - Current annotation number
		 * @param {String} prefix - Current annotation prefix
		 * @param {Object} ev - Current event
		 */
		drawTextField: function(annotationsList, annotation, annotationsCounter, prefix, ev) {
			// close comments bar
			$('#gd-annotations-comments-toggle').prop('checked', false);
			// get mouse position
			mouse = getMousePosition(ev);
			currentPrefix = prefix;
			idNumber = annotationsCounter;
			// calculate coordinates
			var canvasTopOffset = $(canvas).offset().top * zoomLevel;
			var x = mouse.x - ($(canvas).offset().left * zoomLevel) - (parseInt($(canvas).css("margin-left")) * 2);
			var y = mouse.y - canvasTopOffset - (parseInt($(canvas).css("margin-top")) * 2);
			zoomCorrection.x = ($(canvas).offset().left * zoomLevel) - $(canvas).offset().left;
			zoomCorrection.y = ($(canvas).offset().top * zoomLevel) - $(canvas).offset().top;
			annotation.id = annotationsCounter;			
			// set start point coordinates
			startX = mouse.x;
			startY = mouse.y;
			// prepare annotation HTML element
			element = document.createElement('div');
			element.className = 'gd-annotation';  			
			element.innerHTML = getTextFieldAnnotationHtml(annotationsCounter);	
			var canvasTopOffset = $(canvas).offset().top * zoomLevel;
			element.style.left = x + "px";
			element.style.top = y + "px";
			// draw annotation
			canvas.prepend(element);	
			$(".gd-typewriter-text").click(function (e) {
				e.stopPropagation()
				$(this).focus();
			})		
			// set annotation data
			annotation.width = $(element).find("textarea").width();
			annotation.height = $(element).find("textarea").height();	
			annotation.left = parseInt(element.style.left.replace("px", ""));
			var dashboardHeight = parseInt($(element).find(".gd-text-area-toolbar").css("margin-bottom")) + parseInt($(element).find(".gd-text-area-toolbar").css("height"));
			annotation.top = parseInt(element.style.top.replace("px", "")) + dashboardHeight;			
			annotationsList.push(annotation);	
			makeResizable(annotation);			
		},

		/**
		 * Import text field annotation
		 * @param {Object} canvas - document page to add annotation
		 * @param {Array} annotationsList - List of all annotations
		 * @param {Object} annotation - Current annotation
		 * @param {int} annotationsCounter - Current annotation number
		 * @param {String} prefix - Current annotation prefix
		 */
		importTextField: function(canvas, annotationsList, annotation, annotationsCounter, prefix) {
			$('#gd-annotations-comments-toggle').prop('checked', false);			
			currentPrefix = prefix;
			annotationsCounter;			
			annotation.id = annotationsCounter;				
			element = document.createElement('div');
			element.className = 'gd-annotation';  			
			element.innerHTML = getTextFieldAnnotationHtml(annotationsCounter, annotation.text, annotation.font, annotation.fontSize);				
			element.style.left = annotation.left + "px";			
				
			canvas.prepend(element);	
			$(".gd-typewriter-text").click(function (e) {
				e.stopPropagation()
				$(this).focus();
			})		
			var toolbarHeight = $(element).find(".gd-text-area-toolbar").height() + parseInt($(element).find(".gd-text-area-toolbar").css("margin-bottom")) + parseInt($(element).find(".gd-text-area-toolbar").css("padding"));
			element.style.top = annotation.top - toolbarHeight + "px";				
			annotationsList.push(annotation);	
			makeResizable(annotation);		
			$(".gd-typewriter-text").each(function(index, typeWriter){
				if($(typeWriter).data("id") == annotationsCounter){
					$(typeWriter).css("font-family", annotation.font);
					$(typeWriter).css("font-size", annotation.fontSize);
				}
			});					
		} 		
	});
	
	function getTextFieldAnnotationHtml(id, text, font, fontSize){
		text = (text == null || text == "") ? "Enter annotation text here" : text;
		font = (font == null || font == "") ? "Arial" : font;
		fontSize = (fontSize == null || fontSize == "") ? "10" : fontSize;
		var annotationToolBarHtml = '<div class="gd-text-area-toolbar">'+									
										'<input type="text" class="gd-typewriter-font" value="' + font + '">'+
										'<input type="number" value="' + fontSize + '" class="gd-typewriter-font-size">'+
										'<i class="fa fa-trash-o gd-text-field-delete"></i>'+
									'</div>';
		var annotationTextFieldHtml = '<textarea class="gd-typewriter-text mousetrap annotation" id="gd-' + currentPrefix + '-annotation-' + id + '" data-id="' + id + '">' + text + '</textarea>';
		return annotationToolBarHtml + annotationTextFieldHtml;
	}
	
})(jQuery);