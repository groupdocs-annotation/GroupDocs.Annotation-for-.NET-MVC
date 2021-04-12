import { Component } from '@angular/core';
import { ActiveAnnotationService, AnnotationAppComponent, AnnotationConfigService, AnnotationService, CommentAnnotationService, RemoveAnnotationService } from '@groupdocs.examples.angular/annotation';
import {
  AddDynamicComponentService,
  CommonModals,
  FileCredentials,
  FileModel, Formatting,
  HostingDynamicComponentService,
  ModalService,
  NavigateService, PagePreloadService, PasswordService,
  TopTabActivatorService, UploadFilesService,
  Utils,
  WindowService,
  ZoomService
} from "@groupdocs.examples.angular/common-components";

@Component({
  selector: 'custom-annotation-app',
  templateUrl: './custom-annotation.component.html',
  styleUrls: ['./custom-annotation.component.less']
})
export class CustomAnnotationComponent extends AnnotationAppComponent {
  public loading;

  constructor(_annotationService: AnnotationService,
    _modalService: ModalService,
    _navigateService: NavigateService,
    _tabActivatorService: TopTabActivatorService,
    _hostingComponentsService: HostingDynamicComponentService,
    _addDynamicComponentService: AddDynamicComponentService,
    _activeAnnotationService: ActiveAnnotationService,
    _removeAnnotationService: RemoveAnnotationService,
    _commentAnnotationService: CommentAnnotationService,
    uploadFilesService: UploadFilesService,
    pagePreloadService: PagePreloadService,
    passwordService: PasswordService,
    _windowService: WindowService,
    _zoomService: ZoomService,
    configService: AnnotationConfigService) {
    super(
      _annotationService,
      _modalService,
      _navigateService,
      _tabActivatorService,
      _hostingComponentsService,
      _addDynamicComponentService,
      _activeAnnotationService,
      _removeAnnotationService,
      _commentAnnotationService,
      uploadFilesService,
      pagePreloadService,
      passwordService,
      _windowService,
      _zoomService,
      configService
    );

    /**
     *  Put your document retrieval logic here
     *  this example:
     *     1. extracts url from query string
     *     2. downloads document
     *     3. opens downloaded file
     */
    const params = new URLSearchParams(window.location.search);
    const url = params.get('url');
    if (url) {
      const filename = url.split('/').pop();
      this.loading = true;
      _annotationService.upload(null, url, false).subscribe(() => {
        this.loading = false;
        this.selectFile(filename, '', '');
      });
    }
  }
}
