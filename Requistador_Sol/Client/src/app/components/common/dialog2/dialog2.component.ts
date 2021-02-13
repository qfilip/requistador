import { Component, Input, OnInit } from '@angular/core';
import { eDialogAnimation } from 'src/app/models/enums/eDialogAnimation';
import { IDialogOptions } from 'src/app/models/interfaces/IDialogOptions';

@Component({
    selector: 'app-dialog2',
    templateUrl: './dialog2.component.html',
    styleUrls: ['./dialog2.component.scss']
})
export class Dialog2Component implements OnInit {
    constructor() { }

    visible: boolean;
    options: IDialogOptions;
    _eDialogAnimation = eDialogAnimation;

    ngOnInit() {
        this.setDialogOptions({} as IDialogOptions);
    }

    open(options: IDialogOptions) {
        this.setDialogOptions(options);
        this.visible = true;
    }
    
    
    onConfirm() {
        this.options.acceptFn(); 
        this.visible = false;
    }

    onDeny() {
        this.visible = false;
    };

    private setDialogOptions(options: IDialogOptions) {
        this.options = {
            header: options.header ?? 'Confirm',
            acceptFn: options.acceptFn ?? function() {},
            okLabel: options.okLabel ?? 'Ok',
            cancelLabel: options.cancelLabel ?? 'Cancel',
            cancelVisible: options.cancelVisible ?? true,
            dialogAnimation: options.dialogAnimation ?? eDialogAnimation.Sign3D
        } as IDialogOptions;
    }
}
