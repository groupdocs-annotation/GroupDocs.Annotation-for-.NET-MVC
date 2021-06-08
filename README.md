![Groupdocs document & pdf annotator](https://raw.githubusercontent.com/groupdocs-annotation/groupdocs-annotation.github.io/master/resources/image/banner.png "GroupDocs.Annotation")
# GroupDocs.Annotation for .NET MVC Example
###### version 1.19.0

[![Build status](https://ci.appveyor.com/api/projects/status/tj9md854bn7mqw44/branch/master?svg=true)](https://ci.appveyor.com/project/egorovpavel/groupdocs-annotation-for-net-mvc/branch/master)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/c27a7fb14b20448b86aa9e6e881f977f)](https://www.codacy.com/app/egorovpavel/GroupDocs.Annotation-for-.NET-MVC?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=groupdocs-annotation/GroupDocs.Annotation-for-.NET-MVC&amp;utm_campaign=Badge_Grade)
[![GitHub license](https://img.shields.io/github/license/groupdocs-annotation/GroupDocs.Annotation-for-.NET-MVC.svg)](https://github.com/groupdocs-annotation/GroupDocs.Annotation-for-.NET-MVC/blob/master/LICENSE)

## System Requirements
- .NET Framework 4.5
- Visual Studio 2015


## Annotate & write on document with .NET MVC

**GroupDocs.Annotations for .NET** is a powerful library that provides flexible API which allows you to **annotate PDF**, DOCX, PPT, XLS, and over 90 document formats without external dependencies and/or additional document conversions such us (DOCX to PDF or PPT to PDF). With GroupDocs.Annotation API you can write on documents using various annotation tools such as arrow annotation, text annotation or even draw on a document with help of freehand annotation drawing tool.

With GroupDocs.Annotation for .NET MVC application, you can annotate and **write on document** using our modern and responsive web UI interface. Thanks to flexible and highly customizable configuration it can be used as standalone application or can be integrated into your project within few simple steps.

**Note:** without a license application will run in trial mode, purchase [GroupDocs.Annotation for .NET license](https://purchase.groupdocs.com/order-online-step-1-of-8.aspx) or request [GroupDocs.Annotation for .NET temporary license](https://purchase.groupdocs.com/temporary-license).


## Demo Video

<p align="center">
  <a title="Document annotation for .NET " href="https://www.youtube.com/watch?v=TVb7GwSNDqw"> 
    <img src="https://raw.githubusercontent.com/groupdocs-annotation/groupdocs-annotation.github.io/master/resources/image/document-annotation-demo.gif" width="100%" style="width:100%;">
  </a>
</p>


## Features 
<p>
<img src="https://raw.githubusercontent.com/groupdocs-annotation/groupdocs-annotation.github.io/master/resources/image/Text_annotation.png?v=1" align="left" width="430"/>
<br/><br/><br/>
  <b>Text annotation</b>
<div>Add text annotations in any document. Specify font size, set colors, add comments and collaborate.</div>
<br/><br/><br/><br/>
</p>
<br/>
<p>
<img src="https://raw.githubusercontent.com/groupdocs-annotation/groupdocs-annotation.github.io/master/resources/image/Free-hand-annotation.png?v=1" align="left" width="430"/>
<br/><br/><br/>
  <b>Freehand Drawing</b>
<div>Draw on a document using a freehand drawing tool. Easily highlight specific areas on your document page.</div>
<br/><br/><br/>
</p>
<br/>
<p>
<img src="https://raw.githubusercontent.com/groupdocs-annotation/groupdocs-annotation.github.io/master/resources/image/Blackout-annotation.png?v=1" align="left" width="430"/>
<br/><br/><br/>
  <b>Blackout & Redaction</b>
<div>Blackout and redact sensitive or personally identifiable information on your document.</div>
<br/><br/><br/><br/>
</p>
<br/>
<p>
<img src="https://raw.githubusercontent.com/groupdocs-annotation/groupdocs-annotation.github.io/master/resources/image/Collaborate-annotation.png?v=1" align="left" width="430"/>
<br/><br/><br/>
  <b>Comments</b>
<div>Collaborate and comment on any annotation. Start a discussion right in a document without database dependency/integration.</div>
<br/><br/><br/><br/><br/><br/>
</p>
<hr/>

### More features

- Clean, modern and intuitive design
- Easily switchable colour theme (create your own colour theme in 5 minutes)
- Responsive design
- Mobile support (open application on any mobile device)
- Support over 50 documents and image formats
- Image mode
- Fully customizable navigation panel
- Annotate password protected documents
- Download original documents
- Download annotated documents
- Upload documents
- Annotate document with such annotation types: 
   * **Text** – highlights and comments selected text
   * **Area** – marks an area with a rectangle and adds notes to it
   * **Point** – sticks comments to any point in a document
   * **TextStrikeout** – marks text with a strikethrough styling
   * **Polyline** – draws shapes and freehand lines
   * **TextField** – adds rectangle with a text inside
   * **Watermark** - Horizontal textual watermark 
   * **TextReplacement** – replaces original text with user’s text
   * **Arrow** – draws an arrow on a document
   * **TextRedaction** – fills black rectangle with fixed position (used if you want to hide some text)
   * **ResourcesRedaction**  – fills black rectangle with fixed position
   * **TextUnderline** – marks text with a underline styling
   * **Distance** – measures a distance between objects in a document
- Draw annotation over the document page
- Add comment or reply
- Print document
- Smooth page navigation
- Smooth document scrolling
- Preload pages for faster document rendering
- Multi-language support for displaying errors
- Cross-browser support (Safari, Chrome, Opera, Firefox)
- Cross-platform support (Windows, Linux, MacOS)

## How to run

You can run this sample by one of following methods

#### Build from source

Download [source code](https://github.com/groupdocs-annotation/GroupDocs.Annotation-for-.NET-MVC/archive/master.zip) from github or clone this repository.

```bash
git clone https://github.com/groupdocs-annotation/GroupDocs.Annotation-for-.NET-MVC
```

Open solution in the VisualStudio.
Update common parameters in `web.config` and example related properties in the `configuration.yml` to meet your requirements.

Open http://localhost:8080/annotation in your favorite browser

#### Docker image
Use [docker](https://hub.docker.com/u/groupdocs) image.

```bash
mkdir DocumentSamples
mkdir Licenses
docker run -p 8080:8080 --env application.hostAddress=localhost -v `pwd`/DocumentSamples:/home/groupdocs/app/DocumentSamples -v `pwd`/Licenses:/home/groupdocs/app/Licenses groupdocs/annotation
## Open http://localhost:8080/annotation in your favorite browser.
```

## Configuration
For all methods above you can adjust settings in `configuration.yml`. By default in this sample will lookup for license file in `./Licenses` folder, so you can simply put your license file in that folder or specify relative/absolute path by setting `licensePath` value in `configuration.yml`. 

### Annotation configuration options

| Option                             | Type    |   Default value   | Description                                                                                                                                  |
| ---------------------------------- | ------- |:-----------------:|:-------------------------------------------------------------------------------------------------------------------------------------------- |
| **`filesDirectory`**               | String  | `DocumentSamples` | Files directory path. Indicates where uploaded and predefined files are stored. It can be absolute or relative path                          |
| **`fontsDirectory`**               | String  |                   | Path to custom fonts directory.                                                                                                              |
| **`defaultDocument`**              | String  |                   | Absolute path to default document that will be loaded automaticaly.                                                                          |
| **`preloadPageCount`**             | Integer |        `0`        | Indicate how many pages from a document should be loaded, remaining pages will be loaded on page scrolling.Set `0` to load all pages at once |
| **`textAnnotation`**               | Boolean |      `true`       | Enable/disable Text annotation                                                                                                               |
| **`areaAnnotation`**               | Boolean |      `true`       | Enable/disable Area annotation                                                                                                               |
| **`areaAnnotation`**               | Boolean |      `true`       | Enable/disable Point annotation                                                                                                              |
| **`pointAnnotation`**              | Boolean |      `true`       | Enable thumbnails preview                                                                                                                    |
| **`textStrikeoutAnnotation`**      | Boolean |      `true`       | Enable/disable TextStrikeout annotation                                                                                                      |
| **`polylineAnnotation`**           | Boolean |      `true`       | Enable/disable Polyline annotation                                                                                                           |
| **`textFieldAnnotation`**          | Boolean |      `true`       | Enable/disable TextField annotation                                                                                                          |
| **`watermarkAnnotation`**          | Boolean |      `true`       | Enable/disable Watermark annotation                                                                                                          |
| **`textReplacementAnnotation`**    | Boolean |      `true`       | Enable/disable TextReplacement annotation                                                                                                    |
| **`arrowAnnotation`**              | Boolean |      `true`       | Enable/disable Arrow annotation                                                                                                              |
| **`textRedactionAnnotation`**      | Boolean |      `true`       | Enable/disable TextRedaction annotation                                                                                                      |
| **`resourcesRedactionAnnotation`** | Boolean |      `true`       | Enable/disable ResourcesRedaction annotation                                                                                                 |
| **`textUnderlineAnnotation`**      | Boolean |      `true`       | Enable/disable TextUnderline annotation                                                                                                      |
| **`distanceAnnotation`**           | Boolean |      `true`       | Enable/disable Distance annotation                                                                                                           |
| **`downloadOriginal`**             | Boolean |      `true`       | Enable/disable original document downloading                                                                                                 |
| **`downloadAnnotated`**            | Boolen  |      `true`       | Enable/disable signed document downloading                                                                                                   |
| **`zoom`**                         | Boolean |      `true`       | Enable/disable zoom                                                                                                                          |
| **`fitWidth`**                     | Boolean |      `true`       | Enable/disable fit width. Set true to zoom document pages fit width                                                                          | 


### Configuration
For all methods above you can adjust settings in `configuration.yml`. By default in this sample will lookup for license file in `./Licenses` folder, so you can simply put your license file in that folder or specify relative/absolute path by setting `licensePath` value in `configuration.yml`. 

## Troubleshooting
### How to set custom baseURL
BaseURL is fetched from address bar however you can set custom baseURL by adding *forRoot* parameter at [app.module.ts](https://github.com/groupdocs-annotation/GroupDocs.Annotation-for-NET-MVC/blob/master/src/client/apps/viewer/src/app/app.module.ts#L9)

**Example:**
```js
ViewerModule.forRoot("http://localhost:8080")
```

## License
The MIT License (MIT). 

Please have a look at the LICENSE.md for more details

## GroupDocs Annotation on other platforms & frameworks

- JAVA Dropwiard [Document & PDF annotator](https://github.com/groupdocs-annotation/GroupDocs.Annotation-for-Java-Dropwiard) 
- JAVA Spring [Document & PDF annotator](https://github.com/groupdocs-annotation/GroupDocs.Annotation-for-Java-Spring) 
- .NET WebForms [Document & PDF annotator](https://github.com/groupdocs-annotation/GroupDocs.Annotation-for-.NET-WebForms)


[Home](https://www.groupdocs.com/) | [Product Page](https://products.groupdocs.com/annotation/net) | [Documentation](https://docs.groupdocs.com/display/annotationnet/Home) | [API Reference](https://apireference.groupdocs.com/net/annotation) | [Examples](https://github.com/groupdocs-annotation/GroupDocs.Annotation-for-.NET) | [Blog](https://blog.groupdocs.com/category/annotation/) | [Free Support](https://forum.groupdocs.com/c/annotation) | [Temporary License](https://purchase.groupdocs.com/temporary-license)
