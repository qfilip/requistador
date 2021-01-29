import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { DialogComponent } from './components/common/dialog/dialog.component';
import { PageLoaderComponent } from './components/common/page-loader/page-loader.component';
import { PageLoaderService } from './services/page-loader.service';
import { AppButtonComponent } from './components/common/app-button/app-button.component';
import { CocktailTableComponent } from './components/entries/cocktail/cocktail-table/cocktail-table.component';
import { CocktailDetailsComponent } from './components/entries/cocktail/cocktail-details/cocktail-details.component';
import { EntryHomeComponent } from './components/entries/entry-home/entry-home.component';

@NgModule({
  declarations: [
    // common
    DialogComponent,
    PageLoaderComponent,

    // components
    AppComponent,
    HomeComponent,
    AppButtonComponent,
    CocktailTableComponent,
    CocktailDetailsComponent,
    EntryHomeComponent,
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
