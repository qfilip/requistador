import { Component, OnInit, ViewChild } from '@angular/core';
import { AdminController } from 'src/app/controllers/admin.controller';
import { IDialogOptions } from 'src/app/models/interfaces/IDialogOptions';
import { NotificationService } from 'src/app/services/notification.service';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { IAppStateDto } from 'src/app/_generated/interfaces';
import * as utils from '../../utils/file.functions';
import { DialogComponent } from '../common/dialog/dialog.component';

@Component({
    selector: 'admin-panel',
    templateUrl: './admin-panel.component.html',
    styleUrls: ['./admin-panel.component.scss']
})
export class AdminPanelComponent implements OnInit {
    @ViewChild('logDialog') logDialog: DialogComponent;

    constructor(
        private controller: AdminController,
        private pageLoader: PageLoaderService,
        private notification: NotificationService
    ) { }
    
    appConfigs: IAppStateDto;
    logFile: string;

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


    openLogFile(filename: string) {
        this.pageLoader.show();
        this.controller.getLogFile(filename)
        .subscribe(
            result => {
                this.fileResultHandler(result);
                this.pageLoader.hide();
            },
            error => {
                this.notification.error('Failed to fetch data');
                this.pageLoader.hide();
            }
        );
    }


    private fileResultHandler(result: IAppStateDto) {
        utils.FileFunctions.byte2text(result.logFile, (x: string | ArrayBuffer) => {
            const o = { header: 'Log File Content' } as IDialogOptions;
            this.logFile = x as string;
            this.logDialog.open(o);
        });
    }
}
