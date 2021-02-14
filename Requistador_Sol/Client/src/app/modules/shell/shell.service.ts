import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, Subject } from "rxjs";
import { IApiAdminRequest } from "src/app/_generated/interfaces";
import { IShellScript } from "./models/interfaces/IShellScript";

@Injectable()
export class ShellService {
    private scripts: IShellScript[] = [];
    private scripts$: Subject<IShellScript[]> = new BehaviorSubject([]);

    private adminRequest$: Subject<IApiAdminRequest> = new BehaviorSubject(null);

    registerScript(script: IShellScript) {
        const registered = this.scripts
        .map(x => x.name)
        .includes(script.name);

        if(registered) {
            return;
        }
        
        this.scripts.push(script);
        this.scripts$.next(this.scripts);
    }


    get onRegisteredScript(): Observable<IShellScript[]> {
        return this.scripts$.asObservable();
    }


    invokeAdminRequest(value: IApiAdminRequest) {
        this.adminRequest$.next(value);
    }

    get onAdminRequest(): Observable<IApiAdminRequest> {
        return this.adminRequest$.asObservable();
    }

}