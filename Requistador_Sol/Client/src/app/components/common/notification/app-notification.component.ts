import { Component, ElementRef, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { eNotificationType, INotification, NotificationService } from 'src/app/services/notification.service';

@Component({
    selector: 'app-notification',
    templateUrl: './app-notification.component.html',
    styleUrls: ['./app-notification.component.scss']
})
export class NotificationComponent implements OnInit, OnDestroy {

    @ViewChild('notificationContainer') container: ElementRef<HTMLDivElement>;
    
    constructor(
        private service: NotificationService,
        private renderer: Renderer2) { }
    
    unsubscribe: Subject<any> = new Subject();

    private defaultClassList: string[];
    private specificClassList: { [key: string]: string[] };
    private classColors: { [key: string]: string };

    ngOnInit(): void {
        this.generateClassLists();
        this.manageSubscriptions();
    }


    private manageSubscriptions() {
        this.service.notification
            .pipe(takeUntil(this.unsubscribe))
            .subscribe(x => {
                if(x) {
                    this.render(x);
                }
            });
    }


    private generateClassLists() {
        this.defaultClassList = [
            'w3-panel',
            'w3-topbar',
            'w3-bottombar',
        ];
        
        this.specificClassList = {};
        this.classColors = {};
        
        const kvPairs = [
            { key: 'Success', value: 'green'},
            { key: 'Info', value: 'blue'},
            { key: 'Warning', value: 'yellow'},
            { key: 'Error', value: 'red'},
        ];
        
        kvPairs.forEach(kv => {
            const list = [`w3-pale-${kv.value}`, `w3-border-${kv.value}`]
            
            this.specificClassList[kv.key] = list;
            this.classColors[kv.key] = kv.value;
        });
    }


    private render(x: INotification) {
        let notificationBox = this.renderer.createElement('div');
        let innerWrapper = this.renderer.createElement('div');
        let header = this.renderer.createElement('div');
        let content = this.renderer.createElement('div');
        
        const colorKey = this.getColorKey(x);

        let boxClasses = 
            [].concat(this.defaultClassList, this.getSpecificClassList(colorKey));

        boxClasses.forEach(x => this.renderer.addClass(notificationBox, x));
        
        this.renderer.setStyle(notificationBox, 'transition', `opacity ${x.duration}ms`);
        this.renderer.setStyle(notificationBox, 'opacity', '1');

        let headerClasses = this.getHeaderClassList(colorKey);
        headerClasses.forEach(x => this.renderer.addClass(header, x));

        const headerText = this.renderer.createText(colorKey);
        this.renderer.appendChild(header, headerText);

        const text = this.renderer.createText(x.message);
        this.renderer.appendChild(content, text);

        this.renderer.appendChild(this.container.nativeElement, notificationBox);
        this.renderer.appendChild(notificationBox, innerWrapper);
        this.renderer.appendChild(innerWrapper, header);
        this.renderer.appendChild(innerWrapper, content);

        setTimeout(() => {
            this.renderer.setStyle(notificationBox, 'opacity', '0');
            setTimeout(() => {
                this.renderer.removeChild(this.container.nativeElement, notificationBox);
            }, x.duration);
        }, x.duration);
    }


    private getColorKey(x: INotification) {
        let key;
        switch (x.type) {
            case eNotificationType.Success:
                key = 'Success';
                break;
            case eNotificationType.Info:
                key = 'Info';
                break;
            case eNotificationType.Warning:
                key = 'Warning';
                break;
            case eNotificationType.Error:
                key = 'Error'
                break;
            default:
                break;
        }


        return key;
    }


    private getHeaderClassList(colorKey: string) {
        const color = this.classColors[colorKey];
        return [`w3-text-${color}`, `text-b`];
    }


    private getSpecificClassList(colorKey: string) {
        return this.specificClassList[colorKey];
    }


    private makeId() {
        return Math.random().toString(36).substr(2, 9);
    }


    ngOnDestroy() {
        this.unsubscribe.next();
        this.unsubscribe.complete();
    }
}
