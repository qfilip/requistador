import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { INavigationBox } from './home.component.models';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
    constructor(private router: Router) { }

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
            mkLink('Entries', '<i class="lab la-edge"></i>','entries'),
            mkLink('Requests', '<i class="las la-list-ul"></i>',''),
            mkLink('System Config', '<i class="las la-cog"></i>','adminpanel'),
            mkLink('About', '<i class="las la-question"></i>',''),
        ];
    }

    navigateTo(route: string) {
        const navigateTo = `/${route}`;
        this.router.navigate([navigateTo]);
    }
}
