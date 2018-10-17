/**
 * groupdocs.annotation Plugin
 * Copyright (c) 2018 Aspose Pty Ltd
 * Licensed under MIT.
 * @author Aspose Pty Ltd
 * @version 1.0.0
 */
 
//////////////////////////////////////////////////
// Fix required by Edge browser to support prepend function
//////////////////////////////////////////////////  
 (function (arr) {
  arr.forEach(function (item) {
    if (item.hasOwnProperty('prepend')) {
      return;
    }
    Object.defineProperty(item, 'prepend', {
      configurable: true,
      enumerable: true,
      writable: true,
      value: function prepend() {
        var argArr = Array.prototype.slice.call(arguments),
          docFrag = document.createDocumentFragment();

        argArr.forEach(function (argItem) {
          var isNode = argItem instanceof Node;
          docFrag.appendChild(isNode ? argItem : document.createTextNode(String(argItem)));
        });

        this.insertBefore(docFrag, this.firstChild);
      }
    });
  });
})([Element.prototype, Document.prototype, DocumentFragment.prototype]);

$(document).ready(function () {
    //////////////////////////////////////////////////
    // enter text event
    //////////////////////////////////////////////////  
    $("#gd-panzoom").bind("keyup", ".gd-replace-text-area-text", function (event) {
        var id = $(event.target).data("id");
        $.each(annotationsList, function (index, elem) {
            if (elem.id == id) {
                elem.text = $(event.target).val();
            } else {
                return true;
            }
        });
    });
});

(function ($) {

    /**
	* Create private variables.
	**/
    var mouse = {
        x: 0,
        y: 0
    };

    var startX = 0;
    var startY = 0;
    var element = null;
    var annotationInnerHtml = null;
    var lineInnerHtml = null;
    var currentPrefix = "";
    var idNumber = null;
    var userMouseUp = ('ontouchend' in document.documentElement) ? 'touchend' : 'mouseup';
    var userMouseMove = ('ontouchmove' in document.documentElement) ? 'touchmove' : 'mousemove';

    /**
	 * Draw text annotation
	 * @param {Object} canvas - document page to add annotation
	 * @param {Array} annotationsList - List of all annotations
	 * @param {Object} annotation - Current annotation
	 * @param {int} annotationsCounter - Current annotation number
	 * @param {String} prefix - Current annotation prefix
	 * @param {Object} ev - Current event
	 */
    $.fn.drawTextAnnotation = function (canvas, annotationsList, annotation, annotationsCounter, prefix, ev) {
        currentPrefix = prefix;		
        // get current annotation id
        idNumber = annotationsCounter;
        // get mouse position
        mouse = getMousePosition(ev)
        // draw the annotation
        if (element == null && ($(ev.target).prop("tagName") == "IMG" || $(ev.target).prop("tagName") == "svg")) {
            annotation.id = idNumber;
            // set mouse cursor style
            canvas.style.cursor = "crosshair";
            // set start position
            startX = mouse.x;
            startY = mouse.y;
            // create HTML markup for the annotation element
            element = document.createElement('div');
            element.className = 'gd-annotation';
            element.innerHTML = getHtmlResizeHandles();
            // calculate start point coordinates according to the document page
            var canvasTopOffset = $(canvas).offset().top;
            var x = mouse.x - $(canvas).offset().left - (parseInt($(canvas).css("margin-left")) * 2);
            var y = mouse.y - (canvasTopOffset + (parseInt($(canvas).css("margin-top")) * 2));
            // draw the annotation
            switch (currentPrefix) {
                case "area":
                    element.style.left = x + "px";
                    element.style.top = y + "px";
                    annotationInnerHtml = getAnnotationHtml();
                    break;
                case "textStrikeout":
                    // get text coordinates data - required since the annotation of this type can be added only over the text
                    var startCoordinates = setTextAnnotationCoordinates(x, y);
                    element.style.left = startCoordinates.x + "px";
                    element.style.top = startCoordinates.y + "px";
                    element.style.height = startCoordinates.height + "px";
                    annotationInnerHtml = getTextLineAnnotationHtml();
                    lineInnerHtml = getLineHtml();
                    break;
                case "resourcesRedaction":
                    element.style.left = x + "px";
                    element.style.top = y + "px";
                    annotationInnerHtml = getAnnotationHtml();
                    break;
                case "textUnderline":
                    // get text coordinates data - required since the annotation of this type can be added only over the text
                    var startCoordinates = setTextAnnotationCoordinates(x, y);
                    element.style.left = startCoordinates.x + "px";
                    element.style.top = startCoordinates.y + "px";
                    element.style.height = startCoordinates.height + "px";
                    annotationInnerHtml = getTextLineAnnotationHtml();
                    lineInnerHtml = getLineHtml();
                    break;
                default:
                    // get text coordinates data - required since the annotation of this type can be added only over the text
                    var startCoordinates = setTextAnnotationCoordinates(x, y - 2);
                    element.style.left = startCoordinates.x + "px";
                    element.style.top = startCoordinates.y + "px";
                    element.style.height = startCoordinates.height + "px";
                    annotationInnerHtml = getAnnotationHtml();
                    break;
            }
            // set annotation data
            annotation.left = parseFloat(element.style.left.replace("px", ""));
            annotation.top = parseFloat(element.style.top.replace("px", ""));
            // append line element if the annotation is text strikeout or underline
            element.appendChild(annotationInnerHtml);
            if (lineInnerHtml != null) {
                element.appendChild(lineInnerHtml);
            }
            // prepend annotation element into the document page
            canvas.prepend(element);
            annotationsList.push(annotation);
            makeResizable(annotation);
        } else {
            // drop all data when draw is finished
            canvas.onmousemove = null;
            canvas.style.cursor = "default";
            if ($(ev.target).prop("tagName") == "IMG") {
                annotationsList[annotationsList.length - 1].width = parseFloat(element.style.width.replace("px", ""));
                annotationsList[annotationsList.length - 1].height = parseFloat(element.style.height.replace("px", ""));
            }
            element = null;
            annotationInnerHtml = null;
            lineInnerHtml = null;
        }
        // set mouse up event
        // this handler used to get annotation width and height after draw process
        $(canvas).on(userMouseUp, function (e) {
            if (element != null && e.target.tagName != "TEXTAREA") {
                if (currentPrefix == "textReplacement") {
                    element.appendChild(getTextReplaceAnnotationHtml(idNumber));
                }
                canvas.onmousemove = null;
                canvas.onmouseup = null;
                canvas.style.cursor = "default";
                if ($(ev.target).prop("tagName") == "IMG") {
                    annotationsList[annotationsList.length - 1].width = parseFloat(element.style.width.replace("px", ""));
                    annotationsList[annotationsList.length - 1].height = parseFloat(element.style.height.replace("px", ""));
                }
                addComment(annotationsList[annotationsList.length - 1]);
                annotationInnerHtml = null;
                lineInnerHtml = null;
                element = null;
            }
        });

        // set mouse move event
        // this handler used to get annotation width and height while draw process
        $(canvas).on(userMouseMove, function (e) {
            mouse = getMousePosition(e);
            if (element !== null) {
                if (currentPrefix == "text") {
                    element.style.width = Math.abs(mouse.x - startX) + "px";
                } else {
                    element.style.width = Math.abs(mouse.x - startX) + "px";
                    element.style.height = Math.abs(mouse.y - startY) + "px";
                }
                // set annotation data
                annotation.width = parseFloat(element.style.width.replace("px", ""));
                annotation.height = parseFloat(element.style.height.replace("px", ""));
                if (annotationInnerHtml) {
                    annotationInnerHtml.style.width = parseFloat(element.style.width) + "px";
                    annotationInnerHtml.style.height = parseFloat(element.style.height) + "px";
                }
            }
        });
    }

    /**
	 * Import text annotation
	 * @param {Object} canvas - document page to add annotation
	 * @param {Array} annotationsList - List of all annotations
	 * @param {Object} annotation - Current annotation
	 * @param {int} annotationsCounter - Current annotation number	
	 * @param {String} prefix - Current annotation prefix
	 */
    $.fn.importTextAnnotation = function (canvas, annotationsList, annotation, annotationsCounter, prefix) {
        // check if the annotation with the same coordinates are already added.
        // we use this since the GroupDocs.Annotation library returns all annotation comments from the Word document as a new annotation		
        currentPrefix = prefix;
        idNumber = annotationsCounter;
        annotation.id = idNumber;
        // prepare annotation HTML markup
        element = document.createElement('div');
        element.className = 'gd-annotation';
        element.innerHTML = getHtmlResizeHandles();

        switch (currentPrefix) {
            case "textStrikeout":
                element.style.left = annotation.left + "px";
                element.style.top = annotation.top + "px";
                element.style.height = annotation.height + "px"
                annotationInnerHtml = getTextLineAnnotationHtml();
                annotationInnerHtml.style.height = annotation.height + "px";
                annotationInnerHtml.style.width = annotation.width + "px";
                lineInnerHtml = getLineHtml();
                break;
            case "textUnderline":
                element.style.left = annotation.left + "px";
                element.style.top = annotation.top + "px";
                element.style.height = annotation.height + "px";
                element.style.width = annotation.width + "px";
                annotationInnerHtml = getTextLineAnnotationHtml();
                annotationInnerHtml.style.height = annotation.height + "px";
                annotationInnerHtml.style.width = annotation.width + "px";
                lineInnerHtml = getLineHtml();
                break;
            default:
                element.style.left = annotation.left + "px";
                element.style.top = annotation.top + "px";
                element.style.height = annotation.height + "px";
                element.style.width = annotation.width + "px";
                annotationInnerHtml = getAnnotationHtml();
                annotationInnerHtml.style.height = annotation.height + "px";
                annotationInnerHtml.style.width = annotation.width + "px";
                break;
        }
        // draw imported annotation
        element.appendChild(annotationInnerHtml);
        if (lineInnerHtml != null) {
            element.appendChild(lineInnerHtml);
			lineInnerHtml = null;
        }
        canvas.prepend(element);
        // add annotation into the annotations list
        annotationsList.push(annotation);
        makeResizable(annotation);
        addComment(annotation);       
    }

    function getAnnotationHtml() {
        var annotationHtml = document.createElement('div');
        annotationHtml.className = 'gd-' + currentPrefix + '-annotation annotation';
        annotationHtml.id = 'gd-' + currentPrefix + '-annotation-' + idNumber;
        return annotationHtml;
    }

    function getTextReplaceAnnotationHtml() {
        var annotationHtml = document.createElement('div');
        annotationHtml.className = "gd-replace-text-area annotation";
        annotationHtml.id = 'gd-' + currentPrefix + '-annotation-' + idNumber;
        annotationHtml.innerHTML = '<div class="gd-replace-tab">Replace</div>' +
									'<textarea class="gd-replace-text-area-text mousetrap" data-id="' + idNumber + '">replace text</textarea>';

        return annotationHtml;
    }

    function getTextLineAnnotationHtml() {
        var annotationHtml = document.createElement('div');
        annotationHtml.className = 'gd-' + currentPrefix + '-annotation gd-text-annotation annotation';
        annotationHtml.id = 'gd-' + currentPrefix + '-annotation-' + idNumber;
        return annotationHtml;
    }

    function getLineHtml() {
        var annotationHtml = document.createElement('div');
        annotationHtml.className = 'gd-' + currentPrefix + '-line';
        return annotationHtml;
    }
})(jQuery);