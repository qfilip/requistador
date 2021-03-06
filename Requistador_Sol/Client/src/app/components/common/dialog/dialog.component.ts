import { Component, OnInit } from '@angular/core';
import { IDialogOptions } from 'src/app/models/interfaces/IDialogOptions';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogComponent implements OnInit {

    visible: boolean;
    options: IDialogOptions;

    ngOnInit(): void {
        this.setDialogOptions({} as IDialogOptions);
    }

    open(options: IDialogOptions) {
        this.setDialogOptions(options);
        this.visible = true;
    }

    ok() {
        this.options.acceptFn(); 
        this.visible = false;
    }

    close() {
        this.visible = false;
    }

    private setDialogOptions(options: IDialogOptions) {
        this.options = {
            header: options.header ?? 'Confirm',
            acceptFn: options.acceptFn ?? function() {},
            okLabel: options.okLabel ?? 'Ok',
            cancelLabel: options.cancelLabel ?? 'Cancel',
            cancelVisible: options.cancelVisible ?? true
        } as IDialogOptions;
    }

}
