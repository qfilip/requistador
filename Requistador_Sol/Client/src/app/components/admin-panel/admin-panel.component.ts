import { Component, OnInit } from '@angular/core';
import { AdminController } from 'src/app/controllers/admin.controller';
import { NotificationService } from 'src/app/services/notification.service';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { IAppStateDto } from 'src/app/_generated/interfaces';

@Component({
    selector: 'admin-panel',
    templateUrl: './admin-panel.component.html',
    styleUrls: ['./admin-panel.component.scss']
})
export class AdminPanelComponent implements OnInit {

    constructor(
        private controller: AdminController,
        private pageLoader: PageLoaderService,
        private notification: NotificationService
    ) { }
    
    appConfigs: IAppStateDto;


    ngOnInit(): void {
        this.getConfigs();
    }


    private getConfigs() {
        this.pageLoader.show();
        this.controller.getAppConfiguration()
        .subscribe(
            result => {
                this.appConfigs = result;
                this.pageLoader.hide();
            },
            error => {
                this.notification.error('Failed to fetch data');
                this.pageLoader.hide();
            }
        );
    }
}
