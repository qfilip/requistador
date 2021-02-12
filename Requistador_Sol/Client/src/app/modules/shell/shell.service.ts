import { Injectable } from "@angular/core";
import { BehaviorSubject, Subject } from "rxjs";
import { IShellScript } from "./models/interfaces/IShellScript";

@Injectable()
export class ShellService {
    private scripts: IShellScript[] = [];
    private scripts$: Subject<IShellScript[]> = new BehaviorSubject([]);

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


    get registeredScripts() {
        return this.scripts$.asObservable();
    }

}