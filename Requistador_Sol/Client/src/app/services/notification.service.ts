import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable()
export class NotificationService {
    constructor() { }
    
    private notification$: Subject<INotification> = new BehaviorSubject(null);
    
    success(message: string, duration = 3000) {
        this.notify(message, eNotificationType.Success, duration);
    }

    info(message: string, duration = 3000) {
        this.notify(message, eNotificationType.Info, duration);
    }

    warning(message: string, duration = 3000) {
        this.notify(message, eNotificationType.Warning, duration);
    }

    error(message: string, duration = 3000) {
        this.notify(message, eNotificationType.Error, duration);
    }

    private notify(message: string, type: eNotificationType, duration: number) {
        this.notification$.next({
            message: message,
            type: type,
            duration: duration
        } as INotification);
    }

    get notification() {
        return this.notification$.asObservable();
    }
}

export interface INotification {
    message: string,
    type: eNotificationType,
    duration: number
}

export enum eNotificationType {
    Success = 0,
    Info = 1,
    Warning = 2,
    Error = 3
}
