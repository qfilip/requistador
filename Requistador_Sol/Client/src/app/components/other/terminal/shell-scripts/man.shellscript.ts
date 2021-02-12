import { ElementRef, Renderer2 } from "@angular/core";
import { eShellColor } from "../terminal.models";
import { ShellScriptBase } from "./base.shellscript";

export class ManScript extends ShellScriptBase {
    constructor(stdout: ElementRef<HTMLDivElement>, renderer: Renderer2) {
        super('appcfg', stdout, renderer);
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
        throw new Error("Method not implemented.");
    }

}