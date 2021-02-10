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
import { LoginComponent } from './components/account/login/login.component';
import { RegisterComponent } from './components/account/register/register.component';
import { AccountController } from './controllers/account.controller';
import { IdentityModule } from './modules/identity/identity.module';
import { TerminalComponent } from './components/other/terminal/terminal.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';

@NgModule({
  declarations: [
    // common components
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
    LoginComponent,
    RegisterComponent,
    AdminPanelComponent,
    
    // other components
    TerminalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    IdentityModule
  ],
  providers: [
      // common
      PageLoaderService,
      NotificationService,

      // controllers
      CocktailController,
      IngredientController,
      AccountController
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
