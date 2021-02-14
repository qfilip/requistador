import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, Subject } from "rxjs";
import { IApiAdminRequestDto } from "src/app/_generated/interfaces";
import { IShellScript } from "./models/interfaces/IShellScript";

@Injectable()
export class ShellService {
    private scripts: IShellScript[] = [];
    private scripts$: Subject<IShellScript[]> = new BehaviorSubject([]);

    private adminRequest$: Subject<IApiAdminRequestDto> = new BehaviorSubject(null);

    invRegisterScript(script: IShellScript) {
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


    invAdminRequest(value: IApiAdminRequestDto) {
        this.adminRequest$.next(value);
    }

    get onAdminRequest(): Observable<IApiAdminRequestDto> {
        return this.adminRequest$.asObservable();
    }

}