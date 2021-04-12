import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AnnotationModule } from "@groupdocs.examples.angular/annotation";

import { CustomAnnotationComponent } from './custom-annotation/custom-annotation.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [AppComponent, CustomAnnotationComponent],
  imports: [BrowserModule,
    AnnotationModule.forRoot("http://localhost:8080"),
    FontAwesomeModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
