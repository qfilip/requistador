import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { DialogComponent } from './components/common/dialog/dialog.component';
import { PageLoaderComponent } from './components/common/page-loader/page-loader.component';
import { PageLoaderService } from './services/page-loader.service';

@NgModule({
  declarations: [
    // common
    DialogComponent,
    PageLoaderComponent,

    // components
    AppComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
      // common
      PageLoaderService,

      // controllers
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
