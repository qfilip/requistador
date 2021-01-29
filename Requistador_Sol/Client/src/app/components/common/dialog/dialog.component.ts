import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-dialog',
    templateUrl: './dialog.component.html',
    styleUrls: ['./dialog.component.scss']
})
export class DialogComponent implements OnInit {

    constructor() { }

    visible: boolean;
    header: string;

    okLabel: string;
    cancelLabel: string;

    cancelVisible: boolean;

    ngOnInit(): void {
    }
    open() { this.visible = true; }
    onConfirm() { this.visible = false; }
    onDeny() {};

}
