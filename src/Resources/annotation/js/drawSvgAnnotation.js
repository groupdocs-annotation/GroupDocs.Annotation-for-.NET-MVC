/**
 * groupdocs.annotation Plugin
 * Copyright (c) 2018 Aspose Pty Ltd
 * Licensed under MIT.
 * @author Aspose Pty Ltd
 * @version 1.0.0
 */

(function( $ ) {

	/**
	* Create private variables.
	**/
	var mouse = {
        x: 0,
        y: 0,       
    };
    var element = null;	
	var pointSvgSize = 25;
	var svgCircleRadius = 22;
	
	var zoomCorrection = {
		x: 0,
		y: 0
	};
	
	var canvas = null;
	var zoomLevel = 1;
	var currentAnnotation = null;
	var canvasTopOffset = null;
	var currentPrefix = "";
	var userMouseUp = ('ontouchend' in document.documentElement)  ? 'touchend' : 'mouseup';
	var userMouseMove = ('ontouchmove' in document.documentElement)  ? 'touchmove' : 'mousemove';
	
	/**
	 * Draw svg annotation	
	 */
	$.fn.drawSvgAnnotation = function(documentPage, prefix) {	
		// get current data required to draw and positioning the annotation
		canvas = documentPage;
		zoomLevel = (typeof $(canvas).css("zoom") == "undefined") ? 1 : $(canvas).css("zoom");
		currentAnnotation = annotation;
		// get coordinates correction - this is required since the document page is zoomed
		zoomCorrection.x = ($(canvas).offset().left * zoomLevel) - $(canvas).offset().left;
		zoomCorrection.y = ($(canvas).offset().top * zoomLevel) - $(canvas).offset().top;
		canvasTopOffset = $(canvas).offset().top * zoomLevel;
		currentPrefix = prefix;
	}

	/**
	* Extend plugin
	**/
	$.extend(true, $.fn.drawSvgAnnotation, {
		
		/**
		* Draw point annotation
		*/
		drawPoint: function(event){			
			mouse = getMousePosition(event);
			// get current x and y coordinates
			var x = mouse.x - ($(canvas).offset().left * zoomLevel) - (parseInt($(canvas).css("margin-left")) * 2);
			var y = mouse.y - canvasTopOffset - (parseInt($(canvas).css("margin-top")) * 2);	
			// set annotation data
			currentAnnotation.id = annotationsCounter;	
			currentAnnotation.left = x;
			currentAnnotation.top = y;	
			currentAnnotation.width = pointSvgSize;
			currentAnnotation.height = pointSvgSize;
			// add annotation into the annotations list
			annotationsList.push(currentAnnotation);
			addComment(currentAnnotation);	
			// draw the point SVG
			var circle = svgList[canvas.id].circle(svgCircleRadius);
			circle.attr({
				'fill': 'red',			
				'stroke': 'black',
				'stroke-width': 2,
				'cx': x,
				'cy': y,
				'id': 'gd-point-annotation-' + annotationsCounter,
				'class': 'gd-annotation annotation svg'
			})			
		},
		
		/**
		* Draw polyline annotation
		*/
		drawPolyline: function(event){
			mouse = getMousePosition(event);	
			// get x and y coordinates
			var x = mouse.x - ($(canvas).offset().left * zoomLevel) - (parseInt($(canvas).css("margin-left")) * 2);
			var y = mouse.y - canvasTopOffset - (parseInt($(canvas).css("margin-top")) * 2);	
			currentAnnotation.id = annotationsCounter;		
			// set polyline draw options
			const option = {
				'stroke': 'red',
				'stroke-width': 2,
				'fill-opacity': 0,
				'id': 'gd-polyline-annotation-' + annotationsCounter,
				'class': 'gd-annotation annotation svg'						  
			};
			// initiate svg object
			var line = null;
			line = svgList[canvas.id].polyline().attr(option);			
			line.draw(event);			
			// set mouse move event handler
			svgList[canvas.id].on(userMouseMove, event => {
			  if (line) {
				// draw line to next point coordinates
				line.draw('point', event);
			  }
			})
			// set mouse up event handler
			svgList[canvas.id].on(userMouseUp, event => {
				if (line && currentPrefix == "polyline") {
					// stop draw
					line.draw('stop', event);				
					// set annotation data
					currentAnnotation.left = x;
					currentAnnotation.top = y;	
					currentAnnotation.width = line.width();
					currentAnnotation.height = line.height();
					currentAnnotation.svgPath = "M";	
					var previousX = 0;
					var previousY = 0;
					// prepare SVG path string, important note: all point coordinates except the first one are not coordinates, 
					//but the number of pixels that need to be added or subtracted from the previous point in order to obtain the amount of displacement.
					// This is required by the GRoupDocs.Annotation library to draw the SVG path
                	if (line.node.points.numberOfItems) { // for safari
                        for (i = 0; i < line.node.points.numberOfItems; i++) {
							var point = line.node.points.getItem(i);
                            if (i == 0) {
                                currentAnnotation.svgPath = currentAnnotation.svgPath + point.x + "," + point.y + "l";
                            } else {
                                previousX = point.x - previousX;
                                previousY = point.y - previousY;
                                currentAnnotation.svgPath = currentAnnotation.svgPath + previousX + "," + previousY + "l";
                            }
                            previousX = point.x;
                            previousY = point.y;
                        }
					} else { // for other browsers
                        $.each(line.node.points, function (index, point) {
                            if (index == 0) {
                                currentAnnotation.svgPath = currentAnnotation.svgPath + point.x + "," + point.y + "l";
                            } else {
                                previousX = point.x - previousX;
                                previousY = point.y - previousY;
                                currentAnnotation.svgPath = currentAnnotation.svgPath + previousX + "," + previousY + "l";
                            }
                            previousX = point.x;
                            previousY = point.y;
                        });
                    }
					currentAnnotation.svgPath = currentAnnotation.svgPath.slice(0,-1);
					// add annotation into the annotations list
					annotationsList.push(currentAnnotation);
					// add comments
					addComment(currentAnnotation);	
					line = null;
				}
			});		
		},
		
		/**
		* Draw arrow annotation
		*/
		drawArrow: function(event){
			mouse = getMousePosition(event);	
			// get coordinates
			var x = mouse.x - ($(canvas).offset().left * zoomLevel) - (parseInt($(canvas).css("margin-left")) * 2);
			var y = mouse.y - canvasTopOffset - (parseInt($(canvas).css("margin-top")) * 2);	
			currentAnnotation.id = annotationsCounter;	
			// set draw options
			const option = {
				'stroke': 'red',
				'stroke-width': 2,
				'fill-opacity': 0,
				'id': 'gd-arrow-annotation-' + annotationsCounter,
				'class': 'gd-annotation annotation svg'
							  
			};
			// draw start point
			var path = null;
			path = svgList[canvas.id].path("M" + x + "," + y + " L" + x + "," + y).attr(option);			
			// set mouse move event handler
			svgList[canvas.id].on(userMouseMove, event => {
				if (path) {
					// get current coordinates after mouse move
					mouse = getMousePosition(event);
					var endX = mouse.x - ($(canvas).offset().left * zoomLevel) - (parseInt($(canvas).css("margin-left")) * 2);
					var endY = mouse.y - canvasTopOffset - (parseInt($(canvas).css("margin-top")) * 2);
					// update svg with the end point and draw line between
					path.plot("M" + x + "," + y + " L" + endX + "," + endY);
					// add arrow marker at the line end
					path.marker('end', 20, 20, function(add) {
						var arrow = "M0,7 L0,13 L12,10 z";
						add.path(arrow);
						
						this.fill('red');
					});	
				}
			})
			// set mouse up event handler
			svgList[canvas.id].on(userMouseUp, event => {
				if (path && currentPrefix == "arrow") {	
					// set annotation data
					currentAnnotation.left = x;
					currentAnnotation.top = y;	
					currentAnnotation.width = path.width();
					currentAnnotation.height = path.height();
					
					currentAnnotation.svgPath = path.attr("d");				
					annotationsList.push(currentAnnotation);
					addComment(currentAnnotation);		
					path = null;
				}
			});		
		},
		
		/**
		* Draw distance annotation
		*/
		drawDistance: function(event){
			// get coordinates
			mouse = getMousePosition(event);	
			var x = mouse.x - ($(canvas).offset().left * zoomLevel) - (parseInt($(canvas).css("margin-left")) * 2);
			var y = mouse.y - canvasTopOffset - (parseInt($(canvas).css("margin-top")) * 2);	
			currentAnnotation.id = annotationsCounter;	
			// set draw options
			const option = {
				'stroke': 'red',
				'stroke-width': 2,
				'fill-opacity': 0,
				'id': 'gd-distance-annotation-' + annotationsCounter,
				'class': 'gd-annotation annotation svg'
							  
			};
			// set text options
			const textOptions = {
				'font-size': "12px",
				'data-id': currentAnnotation.id 
			};
			// draw start point
			var path = null;
			path = svgList[canvas.id].path("M" + x + "," + y + " L" + x + "," + y).attr(option);	
			// add text svg element - used to display the distance value
			var text = null;
			text = svgList[canvas.id].text("0px").attr(textOptions);
			// set mouse move event
			svgList[canvas.id].on(userMouseMove, event => {
				if (path) {
					// get end coordinates
					mouse = getMousePosition(event);
					var endX = mouse.x - ($(canvas).offset().left * zoomLevel) - (parseInt($(canvas).css("margin-left")) * 2);
					var endY = mouse.y - canvasTopOffset - (parseInt($(canvas).css("margin-top")) * 2);
					// draw the last point and the line between
					path.plot("M" + x + "," + y + " L" + endX + "," + endY);
					// update text value and draw it in accordance with the svg
					text.path("M" + x + "," + (y - 3) + " L" + endX + "," + (endY - 3)).move(path.width() / 2, y).tspan(Math.round(path.width()) + "px");
					// add start and end arrows
					path.marker('start', 20, 20, function(add) {
						var arrow = "M12,7 L12,13 L0,10 z";						
						add.path(arrow);
						add.rect(1, 20).cx(0).fill('red')						
						this.fill('red');
					});						
					path.marker('end', 20, 20, function(add) {
						var arrow = "M0,7 L0,13 L12,10 z";
						add.path(arrow);
						add.rect(1, 20).cx(11).fill('red')
						this.fill('red');
						currentAnnotation.text = Math.round(path.width()) + "px";
					});	
				}
			})
			// set mouse up event
			svgList[canvas.id].on(userMouseUp, event => {
				if (path) {
					currentAnnotation.left = x;
					currentAnnotation.top = y;	
					currentAnnotation.width = path.width();
					currentAnnotation.height = path.height();
					
					currentAnnotation.svgPath = path.attr("d");				
					annotationsList.push(currentAnnotation);
					addComment(currentAnnotation);		
					path = null;
				}
			});		
		},
		
		/**
		* Import point annotation
		*/
		importPoint: function(annotation){
			annotation.id = annotationsCounter;
			// add annotation into the annotations list
			annotationsList.push(annotation);
			// add comments
			addComment(annotation);	
			// draw imported annotation
			var circle = svgList[canvas.id].circle(svgCircleRadius);
			circle.attr({
				'fill': 'red',			
				'stroke': 'black',
				'stroke-width': 2,
				'cx': annotation.left,
				'cy': annotation.top,
				'id': 'gd-point-annotation-' + annotationsCounter,
				'class': 'gd-annotation annotation svg'
			});					
		},
		
		/**
		* Import polyline annotation
		*/
		importPolyline: function(annotation){				
			annotation.id = annotationsCounter;
			annotationsList.push(annotation);
			addComment(annotation);				
			const option = {
				'stroke': 'red',
				'stroke-width': 2,
				'fill-opacity': 0,
				'id': 'gd-polyline-annotation-' + annotationsCounter,
				'class': 'gd-annotation annotation svg'						  
			}
			var line = null;
			var svgPath = "";			
			// recalculate path points coordinates from the offset values back to the coordinates values - why we need this described above in the draw polyline action
			var points = annotation.svgPath.replace("M", "").split('l');
			var x = parseFloat(points[0].split(",")[0]);
			var y = parseFloat(points[0].split(",")[1]);
			svgPath = points[0];
			$.each(points, function(index, point){
				if(index != 0){
					svgPath = svgPath + " " + (x + parseFloat(point.split(",")[0])) + "," + (y + parseFloat(point.split(",")[1]));
					x = (x + parseFloat(point.split(",")[0]));
					y = (y + parseFloat(point.split(",")[1]));
				} else {
					return true;
				}
			});
			// draw imported annotation
			line = svgList[canvas.id].polyline(svgPath).attr(option);			
				
		},
		
		/**
		* Import arrow annotation
		*/
		importArrow: function(annotation){			
			currentAnnotation.id = annotationsCounter;	
			annotationsList.push(annotation);
			addComment(annotation);
			const option = {
				'stroke': 'red',
				'stroke-width': 2,
				'fill-opacity': 0,
				'id': 'gd-arrow-annotation-' + annotationsCounter,
				'class': 'gd-annotation annotation svg'
							  
			}			
			// draw imported annotation
			var arrow = svgList[canvas.id].path("M" + annotation.left + "," + annotation.top + " L" + (annotation.left + annotation.width) + "," + (annotation.top + annotation.height)).attr(option);
			arrow.marker('end', 20, 20, function(add) {
						var arrow = "M0,7 L0,13 L12,10 z";
						add.path(arrow);
						
						this.fill('red');
					});	
			annotationsList[annotationsList.length - 1].svgPath = "M" + annotation.left + "," + annotation.top + " L" + (annotation.left + annotation.width) + "," + (annotation.top + annotation.height);
		},   
		
		/**
		* import distance annotation
		*/
		importDistance: function(annotation){			
		    currentAnnotation.id = annotationsCounter;
		    if (annotation.comments != null) {
		        annotation.comments[0].text = annotation.comments[0].text.replace(annotation.width + "px", "");
		        annotation.comments = $.grep(annotation.comments, function (value) {
		            return value.text != "  ";
		        });
		    }
			annotation.text = Math.round(annotation.width) + "px";
			annotationsList.push(annotation);		
			addComment(annotation);			
			const option = {
				'stroke': 'red',
				'stroke-width': 2,
				'fill-opacity': 0,
				'id': 'gd-distance-annotation-' + annotationsCounter,
				'class': 'gd-annotation annotation svg'
							  
			};
			const textOptions = {
				'font-size': "12px",
				'data-id': currentAnnotation.id 
			};
			var text = null;
			// prepare svg path coordinates
			var svgPath = annotation.svgPath.split("L")[0];
			var points = annotation.svgPath.replace("M", "").split('L');
			var x = parseFloat(points[0].split(",")[0]);
			var y = parseFloat(points[0].split(",")[1]);
			// recalculate path points coordinates from the offset values back to the coordinates values
			$.each(points, function(index, point){
				if(index != 0){					
					svgPath = svgPath + " L" + (x + parseFloat(point.split(",")[0])) + "," + (y + parseFloat(point.split(",")[1]));									
				} else {					
					return true;
				}
			});
			// draw the distance annotation
			var distance = svgList[canvas.id].path(svgPath).attr(option);
			// draw text with the distance data
			text = svgList[canvas.id].text(annotation.width + "px").attr(textOptions)
			text.path(svgPath).tspan(Math.round(annotation.width) + "px").dx(annotation.width / 2).dy(-5);
			// add start and end arrows
			distance.marker('start', 20, 20, function(add) {
						var arrow = "M12,7 L12,13 L0,10 z";						
						add.path(arrow);
						add.rect(1, 20).cx(0).fill('red')						
						this.fill('red');
					});						
			distance.marker('end', 20, 20, function(add) {
				var arrow = "M0,7 L0,13 L12,10 z";
				add.path(arrow);
				add.rect(1, 20).cx(11).fill('red')
				this.fill('red');				
			});	
			annotationsList[annotationsList.length - 1].svgPath = svgPath;			
		},
	});
	
	// This is custom extension of polyline, which doesn't draw the circle
	SVG.Element.prototype.draw.extend('polyline', {

		init:function(e){
			// When we draw a polygon, we immediately need 2 points.
			// One start-point and one point at the mouse-position
			this.set = new SVG.Set();
			var p = null;
			if(isNaN(this.startPoint.x) && isNaN(this.startPoint.y)) {
				p = {
					x: e.touches[0].clientX,
					y: e.touches[0].clientY
				};
			} else {
				p = this.startPoint;
			}
			
			arr = [
				[p.x - zoomCorrection.x, p.y - zoomCorrection.y],
				[p.x - zoomCorrection.x, p.y - zoomCorrection.y]
			];

			this.el.plot(arr);
		},

		// The calc-function sets the position of the last point to the mouse-position (with offset)
		calc:function (e) {
			var arr = this.el.array().valueOf();
			arr.pop();

			if (e) {
				// fix for mobiles
				var x = (typeof e.clientX != "undefined") ? e.clientX : e.changedTouches[0].clientX;
				var y = (typeof e.clientY != "undefined") ? e.clientY : e.changedTouches[0].clientY;
				var p = this.transformPoint(x, y);	
				p.x = p.x - zoomCorrection.x;
				p.y = p.y - zoomCorrection.y;
				arr.push(this.snapToGrid([p.x, p.y]));
			}

			this.el.plot(arr);

		},

		point:function(e){

			if (this.el.type.indexOf('poly') > -1) {
				// fix for mobiles
				var x = (typeof e.clientX != "undefined") ? e.clientX : e.touches[0].clientX;
				var y = (typeof e.clientY != "undefined") ? e.clientY : e.touches[0].clientY;
				// Add the new Point to the point-array
				var p = this.transformPoint(x, y),
				arr = this.el.array().valueOf();
				p.x = p.x - zoomCorrection.x;
				p.y = p.y - zoomCorrection.y;				
				arr.push(this.snapToGrid([p.x, p.y]));

				this.el.plot(arr);

				// Fire the `drawpoint`-event, which holds the coordinates of the new Point
				this.el.fire('drawpoint', {event:e, p:{x:p.x, y:p.y}, m:this.m});

				return;
			}

			// We are done, if the element is no polyline or polygon
			this.stop(e);

		},

		clean:function(){
			// Remove all circles
			this.set.each(function () {
			  this.remove();
			});

			this.set.clear();

			delete this.set;

		},
	});
})(jQuery);