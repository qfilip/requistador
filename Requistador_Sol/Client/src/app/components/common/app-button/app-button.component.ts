import { Component, Input, OnInit } from '@angular/core';
import { eButtonStyle } from 'src/app/models/enums/eButtonStyle';

@Component({
    selector: 'app-button',
    templateUrl: './app-button.component.html',
    styleUrls: ['./app-button.component.scss']
})
export class AppButtonComponent {
    @Input() label: string = 'Ok';
    @Input() styleClass: eButtonStyle = eButtonStyle.Green;
    
    _eButtonStyle = eButtonStyle;

    constructor() { }

    isButtonStyleTypeOf(buttonStyle: eButtonStyle) {
        return buttonStyle === this.styleClass;
    }
}
