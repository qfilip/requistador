import { ElementRef, Renderer2 } from "@angular/core";
import { eShellColor } from "../terminal.models";

export abstract class ShellScriptBase {
    scriptName: string;
    protected stdout: ElementRef<HTMLDivElement>;
    private colorMap: { [key: number]: string }

    constructor(
        scriptName: string,
        stdout: ElementRef<HTMLDivElement>,
        protected renderer: Renderer2
    ) {
        this.scriptName = scriptName;
        this.stdout = stdout;

        this.colorMap = {};
        this.colorMap[eShellColor.Regular] = 'shell-regular';
        this.colorMap[eShellColor.Warning] = 'shell-warning';
        this.colorMap[eShellColor.Error] = 'shell-error';
        this.colorMap[eShellColor.User] = 'shell-user';
    }

    protected print(message: string, color: eShellColor = eShellColor.Regular, fingerbang = false) {
        let outputRow = this.renderer.createElement('div');
            
        const text = this.renderer.createText(message);
        const outputColorClass = this.colorMap[color];
        
        this.renderer.addClass(outputRow, outputColorClass);
        this.renderer.appendChild(outputRow, text);
        
        if(fingerbang) {
            let span = this.renderer.createElement('span');
            span.innerHTML = '&#128405;';
            this.renderer.appendChild(outputRow, span);
        }

        this.renderer.appendChild(this.stdout.nativeElement, outputRow);
    }
    abstract execute(option?: string, arg?: string);
    protected abstract validate(option: string, arg: string): boolean;
    protected abstract getDocumentation(): string[];
}