import { Component, OnInit } from '@angular/core';
import { INavigationBox } from './home.component.models';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
    constructor() { }

    navLinks: INavigationBox[];
    linkText: string;
    
    ngOnInit() {
        this.generateLinks();
    }

    displayLinkText(text: string) {
        this.linkText = text;
    }

    hideLinkText() {
        this.linkText = null;
    }


    private generateLinks() {
        const mkLink = (text: string, icon: string, path: string) => {
            return {text: text, icon: icon, path: path } as INavigationBox;
        }
        this.navLinks = [
            mkLink('Entries', '<i class="las la-list-ul"></i>',''),
            mkLink('Messages', '<i class="las la-info"></i>',''),
            mkLink('System Config', '<i class="las la-cog"></i>',''),
            mkLink('About', '<i class="las la-question"></i>',''),
        ];
        console.table(this.navLinks);
    }
}
