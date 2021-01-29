import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EntryHomeComponent } from './components/entries/entry-home/entry-home.component';
import { HomeComponent } from './components/home/home.component';


const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'entries', component: EntryHomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
