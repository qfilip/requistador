import { Component, OnInit } from '@angular/core';
import { eDialogAnimation } from 'src/app/models/enums/eDialogAnimation';
import { IDialogOptions } from 'src/app/models/interfaces/IDialogOptions';

@Component({
    selector: 'app-dialog',
    templateUrl: './dialog.component.html',
    styleUrls: ['./dialog.component.scss']
})
export class DialogComponent implements OnInit {

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
    
    
    onConfirm() { this.visible = false; }
    onDeny() {};
    
    isAnimationTypeOf(animationType: eDialogAnimation) {
        console.log('inner: ', animationType);
        console.log('option: ', this.options);
        return animationType === this.options.dialogAnimation;
    }

    private setDialogOptions(options: IDialogOptions) {
        this.options = {
            header: options.header ?? 'Confirm',
            acceptFn: options.acceptFn ?? this.acceptFnDefault,
            okLabel: options.okLabel ?? 'Ok',
            cancelLabel: options.cancelLabel ?? 'Cancel',
            cancelVisible: options.cancelVisible ?? true,
            dialogAnimation: options.dialogAnimation ?? eDialogAnimation.Sign3D
        } as IDialogOptions;
    }

    private acceptFnDefault() {
        this.visible = false;
    }
}
