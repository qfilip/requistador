import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

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
import { ShellModule } from './modules/shell/shell.module';
import { AdminController } from './controllers/admin.controller';
import { LogfileNamePipe } from './pipes/logfile-name.pipe';
import { DialogComponent } from './components/common/dialog/dialog.component';

@NgModule({
  declarations: [
    // common components
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
    TerminalComponent,

    // pipes
    LogfileNamePipe,

    DialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    
    // homecooking exports
    IdentityModule,
    ShellModule
  ],
  providers: [
      // common
      PageLoaderService,
      NotificationService,

      // controllers
      CocktailController,
      IngredientController,
      AccountController,
      AdminController
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
