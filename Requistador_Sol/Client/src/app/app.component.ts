import { Component, ViewChild } from '@angular/core';
import { DialogComponent } from './components/common/dialog/dialog.component';
import { IDialogOptions } from './models/interfaces/IDialogOptions';
import { NotificationService } from './services/notification.service';
import { PageLoaderService } from './services/page-loader.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    constructor(private service: NotificationService) {}
    title = 'Client';
    
    notify() {
        this.service.success('works', 5000);
        this.service.info('works', 5000);
        this.service.warning('works', 5000);
        this.service.error('works', 5000);
    }
}

