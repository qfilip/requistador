import { ElementRef, Renderer2 } from "@angular/core";
import { ShellService } from "../../shell.service";
import { ShellScriptBase } from "./base.shellscript";

export class ClearScript extends ShellScriptBase {
    private clearUserInput: () => void;
    constructor(
        stdout: ElementRef<HTMLDivElement>,
        renderer: Renderer2,
        shellService: ShellService,
        clearFn: () => void) {
        super('clear', stdout, renderer, shellService);
        this.clearUserInput = clearFn;
    }

    execute(option?: string, args?: string[]) {
        const childElements = Array.from(this.stdout.nativeElement.childNodes);
        for (let child of childElements) {
            this.renderer.removeChild(this.stdout.nativeElement, child);
        }
        
        this.clearUserInput();
    }

    protected validate(option: string, args: string[]): boolean {
        throw new Error("Method not implemented.");
    }
    protected getDocumentation(): string[] {
        return [
            '[clear]',
            '## Clears previous outputs from the console window',
            '## No additional options available'
        ];
    }
}