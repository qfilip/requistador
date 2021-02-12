import { ElementRef, Renderer2 } from "@angular/core";
import { ShellScriptBase } from "./base.shellscript";

export class ClearScript extends ShellScriptBase {
    private clearUserInput: () => void;
    constructor(stdout: ElementRef<HTMLDivElement>, renderer: Renderer2, clearFn: () => void) {
        super('clear', stdout, renderer);
        this.clearUserInput = clearFn;
    }

    execute(option?: string, arg?: string) {
        const childElements = Array.from(this.stdout.nativeElement.childNodes);
        for (let child of childElements) {
            this.renderer.removeChild(this.stdout.nativeElement, child);
        }
        
        this.clearUserInput();
    }

    protected validate(option: string, arg: string): boolean {
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