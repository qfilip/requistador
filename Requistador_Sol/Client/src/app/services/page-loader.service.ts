import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable()
export class PageLoaderService {
    constructor() { }
    
    private loading$: Subject<boolean> = new BehaviorSubject(false);
    private message$: Subject<string> = new BehaviorSubject(null);
    private progressValue$: Subject<number> = new BehaviorSubject(null);

    show(message: string = null, progressValue: number = null) {
        this.loading$.next(true);
        this.message$.next(message);
        this.progressValue$.next(progressValue);
    }

    hide() {
        this.loading$.next(false);
        this.message$.next(null);
        this.progressValue$.next(null);
    }

    get pageLoaderState() {
        return this.loading$.asObservable();
    }

    get pageLoaderMessage() {
        return this.message$.asObservable();
    }

    setPageLoaderMessage(value: string) {
        this.message$.next(value);
    }

    get pageLoaderProgressValue() {
        return this.progressValue$.asObservable();
    }

    setPageLoaderProgressValue(value: number) {
        this.progressValue$.next(value);
    }
}
