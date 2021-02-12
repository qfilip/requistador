import { ElementRef, Renderer2 } from "@angular/core";
import { takeUntil } from "rxjs/operators";
import { ShellService } from "../../shell.service";
import { eShellColor } from "../enums";
import { IShellScript } from "../interfaces/IShellScript";
import { ShellScriptBase } from "./base.shellscript";

export class ManScript extends ShellScriptBase {
    private scripts: IShellScript[];

    constructor(
        stdout: ElementRef<HTMLDivElement>,
        renderer: Renderer2,
        shellService: ShellService)
    {
        super('man', stdout, renderer, shellService);
        this.subscribeToShell();
    }


    private subscribeToShell() {
        this.shellService.registeredScripts
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe((xs: IShellScript[]) => {
            if(!!xs && xs.length > 0) {
                this.scripts = xs;
            }
        });
    }


    execute(option?: string, arg?: string) {
        throw new Error("Method not implemented.");
    }
    

    protected validate(option: string, arg: string): boolean {
        const valid = !option || option.length === 0;
        if(!valid) {
            const errors = [
                'Insufficient arguments for [man] command',
                'Try [man] [command] to get command details',
                'Try [help] to see available commands'
            ];

            errors.forEach(x => this.print(x, eShellColor.Error));
        }

        return valid;
    }


    protected getDocumentation(): string[] {
        return [
            '[man]',
            '## If you came this far, you know how to use it'
        ];
    }

}