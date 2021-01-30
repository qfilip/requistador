import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { PageLoaderService } from 'src/app/services/page-loader.service';

@Component({
    selector: 'page-loader',
    templateUrl: './page-loader.component.html',
    styleUrls: ['./page-loader.component.scss']
})
export class PageLoaderComponent implements OnInit {

    constructor(private pageLoaderService: PageLoaderService) {}

    loading: boolean;
    message: string;
    progressValue: number;
    progressValueStyle: string;
    unsubscribe: Subject<any> = new Subject();

    ngOnInit(): void {
        this.manageSubscriptions();
    }


    private manageSubscriptions() {
        this.pageLoaderService.state
            .pipe(takeUntil(this.unsubscribe))
            .subscribe(loading => {
                this.loading = loading;
            });

        this.pageLoaderService.message
            .pipe(takeUntil(this.unsubscribe))
            .subscribe(message => {
                if (!!message) {
                    this.message = message;
                }
            });

        this.pageLoaderService.progressValue
            .pipe(takeUntil(this.unsubscribe))
            .subscribe(progressValue => {
                if (!!progressValue) {
                    this.progressValue = progressValue;
                    this.progressValueStyle = `${progressValue}%`;
                }
            });
    }

    ngOnDestroy() {
        this.unsubscribe.next();
        this.unsubscribe.complete();
    }

}
