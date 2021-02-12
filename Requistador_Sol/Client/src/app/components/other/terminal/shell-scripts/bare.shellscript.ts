import { ElementRef, Renderer2 } from "@angular/core";
import { eShellColor } from "../terminal.models";
import { ShellScriptBase } from "./base.shellscript";

export class BareShell extends ShellScriptBase {
    private clear: () => void;
    
    constructor(stdout: ElementRef<HTMLDivElement>, renderer: Renderer2, clearFn: () => void) {
        super('bareshell', stdout, renderer);
        this.clear = clearFn;
    }

    printToShell(message: string, color: eShellColor = eShellColor.Regular, fingerbang = false) {
        this.print(message, color, fingerbang);
        this.clear();
    }

    scrollToElement(): void {
        document
            .querySelector('#terminalBottom')
            .scrollIntoView({behavior: "smooth", block: "start", inline: "nearest"});
    }

    execute(option?: string, arg?: string) {
        throw new Error("Method not implemented.");
    }
    protected validate(option: string, arg: string): boolean {
        throw new Error("Method not implemented.");
    }
    protected getDocumentation(): string[] {
        throw new Error("Method not implemented.");
    }

}