import { ElementRef, Renderer2 } from "@angular/core";
import { ShellService } from "../..";
import { eShellColor } from "../enums";
import { ShellScriptBase } from "./base.shellscript";

export class BareShell extends ShellScriptBase {
    private clear: () => void;
    
    constructor(
        stdout: ElementRef<HTMLDivElement>,
        renderer: Renderer2,
        shellService: ShellService,
        clearFn: () => void)
    {
        const registerInShell = false;
        super('bareshell', stdout, renderer, shellService, registerInShell);
        this.clear = clearFn;
    }

    printToShell(message: string, color: eShellColor = eShellColor.Regular, fingerbang = false) {
        this.print(message, color, fingerbang);
        this.clear();
    }

    scrollToBottom() {
        this.scrollToElement();
    }

    execute(option?: string, args?: string[]) {
        throw new Error("Method not implemented.");
    }
    protected validate(option: string, args: string[]): boolean {
        throw new Error("Method not implemented.");
    }
    protected getDocumentation(): string[] {
        return [];
    }

}