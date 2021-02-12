import { ElementRef, Renderer2 } from "@angular/core";
import { ShellScriptBase } from "./base.shellscript";

export class HelpScript extends ShellScriptBase {
    private scriptNames: string[];

    constructor(stdout: ElementRef<HTMLDivElement>, renderer: Renderer2, scriptNames: string[]) {
        super('help', stdout, renderer);
        this.scriptNames = scriptNames;
        this.scriptNames.push('help');
    }

    execute(option?: string, arg?: string) {
        let printArray = [ 'Available commands are:'];
        this.scriptNames.forEach(x => `[${x}]`);
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