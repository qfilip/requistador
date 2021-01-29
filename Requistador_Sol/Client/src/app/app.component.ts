import { Component, ViewChild } from '@angular/core';
import { DialogComponent } from './components/common/dialog/dialog.component';
import { PageLoaderService } from './services/page-loader.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    @ViewChild('dialog') dialog: DialogComponent;
    constructor(private pageLoader: PageLoaderService) {}
    title = 'Client';
    
    openPgLoader() {
        this.pageLoader.show('Loading yay', 30);
        setTimeout(() => {
            this.pageLoader.hide()
        }, 3000);
    }

    openDialog() {
        this.dialog.open();
    }
}

