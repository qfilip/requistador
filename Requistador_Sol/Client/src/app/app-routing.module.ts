import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/account/login/login.component';
import { RegisterComponent } from './components/account/register/register.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { EntryHomeComponent } from './components/entries/entry-home/entry-home.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './modules/identity/guards';


const routes: Routes = [
    {
        path: '',
        component: HomeComponent,
        // canActivate: [AuthGuard]
    },
    {
        path: 'home',
        component: HomeComponent,
        // canActivate: [AuthGuard]
    },
    {
        path: 'entries',
        component: EntryHomeComponent,
        // canActivate: [AuthGuard]
    },
    {
        path: 'adminpanel',
        component: AdminPanelComponent,
        // canActivate: [AuthGuard]
    },

    // account
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
