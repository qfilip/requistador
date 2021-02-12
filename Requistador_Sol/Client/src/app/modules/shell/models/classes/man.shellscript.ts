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
        const valid = this.validate(option, arg);
        if(!valid) {
            return;
        }

        const command = this.scripts.find(x => x.name === option);
        command.manual.forEach(x => this.print(x));
    }
    

    protected validate(option: string, arg: string): boolean {
        let errors = [];
        const availableScripts = this.scripts.map(x => x.name);

        if(!option) {
            errors = [
                'Bad invocation of [man] command',
                'Try [help] to see available commands'
            ];

            errors.forEach(x => this.print(x, eShellColor.Error));
        }
        else if(option.length === 0) {
            errors = [
                'Insufficient arguments for [man] command',
                'Try [man] [command] to get command details',
                'Try [help] to see available commands'
            ];

            errors.forEach(x => this.print(x, eShellColor.Error));
        }
        else if(!availableScripts.includes(option)) {
            errors = [
                `The command [${option}] is not registered within shell`,
                'Try [help] to see available commands'
            ];

            errors.forEach(x => this.print(x, eShellColor.Error));
        }

        return errors.length === 0;
    }


    protected getDocumentation(): string[] {
        return [
            '[man]',
            '## If you came this far, you know how to use it'
        ];
    }

}