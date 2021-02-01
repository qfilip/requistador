import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

import { DialogComponent } from './components/common/dialog/dialog.component';
import { PageLoaderComponent } from './components/common/page-loader/page-loader.component';

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { CocktailTableComponent } from './components/entries/cocktail/cocktail-table/cocktail-table.component';
import { CocktailDetailsComponent } from './components/entries/cocktail/cocktail-details/cocktail-details.component';
import { EntryHomeComponent } from './components/entries/entry-home/entry-home.component';

import { CocktailController } from './controllers/cocktail.controller';
import { IngredientController } from './controllers/ingretient.controller';

import { PageLoaderService } from './services/page-loader.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NotificationComponent } from './components/common/notification/app-notification.component';
import { NotificationService } from './services/notification.service';
import { IngredientTableComponent } from './components/entries/ingredient/ingredient-table/ingredient-table.component';

@NgModule({
  declarations: [
    // common
    DialogComponent,
    PageLoaderComponent,
    NotificationComponent,

    // components
    AppComponent,
    HomeComponent,
    CocktailTableComponent,
    CocktailDetailsComponent,
    EntryHomeComponent,
    IngredientTableComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
      // common
      PageLoaderService,
      NotificationService,

      // controllers
      CocktailController,
      IngredientController
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
