import { Component, ViewChild } from '@angular/core';
import { DialogComponent } from './components/common/dialog/dialog.component';
import { eButtonStyle } from './models/enums/eButtonStyle';
import { IDialogOptions } from './models/interfaces/IDialogOptions';
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
        const o = {
            acceptFn: this.alertMe
        } as IDialogOptions
        
        this.dialog.open(o);
    }

    private alertMe() {
        alert('Hi');
    }

    buttonAction() {
        alert('Button works');
    }
    
    buttonRed = eButtonStyle.Red;
    buttonGreen = eButtonStyle.Green;
    buttonOrange = eButtonStyle.Orange;
}

