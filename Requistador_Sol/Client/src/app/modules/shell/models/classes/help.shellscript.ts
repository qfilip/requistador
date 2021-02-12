import { ElementRef, Renderer2 } from "@angular/core";
import { takeUntil } from "rxjs/operators";
import { ShellService } from "../../shell.service";
import { IShellScript } from "../interfaces/IShellScript";
import { ShellScriptBase } from "./base.shellscript";

export class HelpScript extends ShellScriptBase {
    private scriptNames: string[];

    constructor(
        stdout: ElementRef<HTMLDivElement>,
        renderer: Renderer2,
        shellService: ShellService
    )    
    {
        super('help', stdout, renderer, shellService);
        this.subscribeToShell();
    }


    private subscribeToShell() {
        this.shellService.registeredScripts
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe((xs: IShellScript[]) => {
            if(!!xs && xs.length > 0) {
                this.scriptNames = xs.map(x => x.name);
            }
        });
    }


    execute(option?: string, arg?: string) {
        let printArray = [ 'Available commands are:'];
        this.scriptNames.forEach(x => printArray.push(`[${x}]`));
        printArray.push('To obtain more info on command, type:');
        printArray.push('man [command]');

        printArray.forEach(x => this.print(x));
    }


    protected validate(option: string, arg: string): boolean {
        throw new Error("Method not implemented.");
    }


    protected getDocumentation(): string[] {
         return [
            '[help]',
            '## Prints out available commands',
            '## No additional options available'
        ];
    }

}